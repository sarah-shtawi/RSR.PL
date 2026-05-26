using Microsoft.AspNetCore.Http;
using RSR.DAL.DTOs.Request.EvaluationSubmissionRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;
using RSR.DAL.DTOs.Response.EvaluationSubmissionResponse;
using RSR.DAL.Enums;
using RSR.DAL.Models.Evaluation;
using RSR.DAL.Repository.EvaluationRepository;
using System.Security.Claims;

namespace RSR.BLL.Service.EvaluationService
{
    public class EvaluationSubmissionService
        : IEvaluationSubmissionService
    {
        private readonly IEvaluationSubmissionRepository
            _submissionRepository;

        private readonly IEvaluationFormRepository
            _formRepository;

        private readonly IFinalEvaluationResultRepository
            _finalResultRepository;

        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public EvaluationSubmissionService(
            IEvaluationSubmissionRepository submissionRepository,
            IEvaluationFormRepository formRepository,
            IFinalEvaluationResultRepository finalResultRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _submissionRepository =
                submissionRepository;

            _formRepository =
                formRepository;

            _finalResultRepository =
                finalResultRepository;

            _httpContextAccessor =
                httpContextAccessor;
        }

        // =========================
        // SUBMIT EVALUATION
        // =========================
        public async Task<SubmitEvaluationResponse?>
            SubmitAsync(
            SubmitEvaluationRequest request)
        {
            // =========================
            // REQUEST VALIDATION
            // =========================
            if (request == null)
            {
                throw new Exception(
                    "Request cannot be null");
            }

            if (request.EvaluationFormId <= 0)
            {
                throw new Exception(
                    "Invalid evaluation form id");
            }

            // =========================
            // GET USER ID
            // =========================
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(
                    "User not found");
            }

            // =========================
            // EMPTY ANSWERS VALIDATION
            // =========================
            if (request.Answers == null
                || !request.Answers.Any())
            {
                throw new Exception(
                    "At least one answer is required");
            }

            // =========================
            // CHECK FORM
            // =========================
            var form = await _formRepository
                .GetByIdWithFieldsAsync(
                    request.EvaluationFormId);

            if (form == null)
            {
                throw new Exception(
                    "Evaluation form not found");
            }

            // =========================
            // LOCK SUBMISSION
            // =========================
            var publishedResult =
                await _finalResultRepository
                    .GetPublishedByGroupIdAsync(
                        request.GroupId);

            if (publishedResult != null)
            {
                throw new Exception(
                    "Final result is published. Submissions are locked.");
            }

            // =========================
            // FORM MUST CONTAIN FIELDS
            // =========================
            if (form.Fields == null
                || !form.Fields.Any())
            {
                throw new Exception(
                    "This evaluation form has no fields");
            }

            // =========================
            // ONLY PUBLISHED FORMS
            // =========================
            if (form.Status != FormStatus.Published)
            {
                throw new Exception(
                    "Only published forms can receive submissions");
            }

            // =========================
            // ROLE VALIDATION
            // =========================
            var userRole = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.Role)?
                .Value;

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception(
                    "User role not found");
            }

            if (form.AssignTo.ToString()
                != userRole)
            {
                throw new Exception(
                    $"This form is assigned to {form.AssignTo} only");
            }

            // =========================
            // PREVENT DUPLICATE SUBMISSION
            // =========================
            var alreadySubmitted =
                await _submissionRepository
                .HasUserSubmittedAsync(
                    request.EvaluationFormId,
                    userId);

            if (alreadySubmitted)
            {
                throw new Exception(
                    "You already submitted this form");
            }

            // =========================
            // DUPLICATE FIELD VALIDATION
            // =========================
            var duplicateFields =
                request.Answers
                .GroupBy(a => a.EvaluationFieldId)
                .Any(g => g.Count() > 1);

            if (duplicateFields)
            {
                throw new Exception(
                    "Duplicate evaluation fields are not allowed");
            }

            // =========================
            // REQUIRED FIELDS VALIDATION
            // =========================
            foreach (var field in form.Fields)
            {
                if (field.IsRequired)
                {
                    var hasAnswer =
                        request.Answers
                        .Any(a =>
                            a.EvaluationFieldId
                            == field.Id);

                    if (!hasAnswer)
                    {
                        throw new Exception(
                            $"{field.FieldName} is required");
                    }
                }
            }

            // =========================
            // ANSWERS VALIDATION
            // =========================
            foreach (var answer in request.Answers)
            {
                if (answer.EvaluationFieldId <= 0)
                {
                    throw new Exception(
                        "Invalid evaluation field id");
                }

                var field = form.Fields
                    .FirstOrDefault(f =>
                        f.Id
                        == answer.EvaluationFieldId);

                if (field == null)
                {
                    throw new Exception(
                        $"Field with ID {answer.EvaluationFieldId} not found in this form");
                }

                if (answer.Value < 0)
                {
                    throw new Exception(
                        $"{field.FieldName} value cannot be negative");
                }

                if (answer.Value < field.MinValue)
                {
                    throw new Exception(
                        $"{field.FieldName} value cannot be less than {field.MinValue}");
                }

                if (answer.Value > field.MaxValue)
                {
                    throw new Exception(
                        $"{field.FieldName} value cannot be greater than {field.MaxValue}");
                }
            }

            // =========================
            // CALCULATE TOTAL SCORE
            // =========================
            var totalScore = request.Answers
                .Sum(a => a.Value);

            // =========================
            // CREATE SUBMISSION
            // =========================
            var submission =
                new EvaluationSubmission
                {
                    EvaluationFormId =
                        request.EvaluationFormId,

                    GroupId =
                        request.GroupId,

                    SubmittedByUserId =
                        userId,

                    SubmittedAt =
                        DateTime.UtcNow,

                    TotalScore =
                        totalScore,

                    Answers = request.Answers
                        .Select(a =>
                            new EvaluationSubmissionAnswer
                            {
                                EvaluationFieldId =
                                    a.EvaluationFieldId,

                                Value =
                                    a.Value
                            }).ToList()
                };

            // =========================
            // SAVE
            // =========================
            var createdSubmission =
                await _submissionRepository
                .CreateAsync(submission);

            // =========================
            // RESPONSE
            // =========================
            return new SubmitEvaluationResponse
            {
                SubmissionId =
                    createdSubmission.Id,

                EvaluationFormId =
                    createdSubmission.EvaluationFormId,

                SubmittedAt =
                    createdSubmission.SubmittedAt,

                Answers =
                    createdSubmission.Answers
                    .Select(a =>
                        new SubmitEvaluationAnswerResponse
                        {
                            EvaluationFieldId =
                                a.EvaluationFieldId,

                            Value =
                                a.Value
                        }).ToList()
            };
        }

        // =========================
        // GET MY FORMS
        // =========================
        public async Task<List<CreateEvaluationFormResponse>>
            GetMyFormsAsync()
        {
            var userRole = _httpContextAccessor
                .HttpContext?
                .User
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)
                ?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception("User role not found");
            }

            var forms = await _formRepository
                .GetPublishedFormsByRoleAsync(userRole);

            return forms.Select(form =>
                new CreateEvaluationFormResponse
                {
                    Id = form.Id,
                    Title = form.Title,
                    AssignTo = form.AssignTo,
                    Description = form.Description,
                    Status = form.Status,
                    CreatedAt = form.CreatedAt,

                    Fields = form.Fields.Select(f =>
                        new CreateEvaluationFieldResponse
                        {
                            Id = f.Id,
                            FieldName = f.FieldName,
                            MinValue = f.MinValue,
                            MaxValue = f.MaxValue,
                            IsRequired = f.IsRequired
                        }).ToList()
                }).ToList();
        }

        // =========================
        // GET FINAL GRADE
        // =========================
        public async Task<FinalGradeResponse>
            GetFinalGradeAsync(Guid groupId)
        {
            var submissions =
                await _submissionRepository
                    .GetGroupSubmissionsAsync(groupId);

            if (!submissions.Any())
            {
                throw new Exception(
                    "No evaluations found for this group");
            }

            double supervisorGrade = submissions
                .Where(s =>
                    s.EvaluationForm.AssignTo.ToString()
                    == "Supervisor")
                .Sum(s => s.TotalScore);

            double examinerGrade = submissions
                .Where(s =>
                    s.EvaluationForm.AssignTo.ToString()
                    == "Examiner")
                .Sum(s => s.TotalScore);

            double finalGrade =
                supervisorGrade + examinerGrade;

            return new FinalGradeResponse
            {
                GroupId = groupId,
                SupervisorGrade = supervisorGrade,
                ExaminerGrade = examinerGrade,
                FinalGrade = finalGrade
            };
        }
    }
}