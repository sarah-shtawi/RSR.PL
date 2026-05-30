using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.EvaluationResponse;
using RSR.DAL.Models.Evaluation;
using RSR.DAL.Repository.EvaluationRepository;

namespace RSR.BLL.Service.EvaluationService
{
    public class FinalEvaluationResultService
        : IFinalEvaluationResultService
    {
        private readonly IEvaluationSubmissionRepository
            _submissionRepository;

        private readonly IFinalEvaluationResultRepository
            _finalRepository;

        public FinalEvaluationResultService(
            IEvaluationSubmissionRepository submissionRepository,
            IFinalEvaluationResultRepository finalRepository)
        {
            _submissionRepository =
                submissionRepository;

            _finalRepository =
                finalRepository;
        }

        // GENERATE FINAL RESULT
        public async Task<FinalEvaluationResultResponse> GenerateAsync(Guid groupId)
        {
            try
            {
            var submissions = await _submissionRepository.GetGroupSubmissionsAsync(groupId);

            if (!submissions.Any())
            {
                throw new Exception("No evaluations found for this group");
            }

            double supervisorGrade =
                submissions
                .Where(s =>
                    s.EvaluationForm.AssignTo.ToString()
                    == "Supervisor")
                .Sum(s => s.TotalScore);

            double examinerGrade =
                submissions
                .Where(s =>
                    s.EvaluationForm.AssignTo.ToString()
                    == "Examiner")
                .Sum(s => s.TotalScore);

            double finalGrade =
                supervisorGrade + examinerGrade;

            var finalResult =
                new FinalEvaluationResult
                {
                    GroupId =
                        groupId,

                    SupervisorGrade =
                        supervisorGrade,

                    ExaminerGrade =
                        examinerGrade,

                    FinalGrade =
                        finalGrade,

                    Status =
                        "Draft",

                    CreatedAt =
                        DateTime.UtcNow
                };

            var created =
                await _finalRepository
                    .CreateAsync(finalResult);

            return new FinalEvaluationResultResponse
            {
                Id =
                    created.Id,

                GroupId =
                    created.GroupId,

                SupervisorGrade =
                    created.SupervisorGrade,

                ExaminerGrade =
                    created.ExaminerGrade,

                FinalGrade =
                    created.FinalGrade,

                Status =
                    created.Status
            };
            }
            catch (Exception ex)
            {
                return new FinalEvaluationResultResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // =========================
        // PUBLISH RESULT
        // =========================
        public async Task<bool>PublishAsync(int id)
        {
            var result =
                await _finalRepository
                    .GetByIdAsync(id);

            if (result == null)
            {
                return false;
            }

            result.Status =
                "Published";

            result.PublishedAt =
                DateTime.UtcNow;

            return await _finalRepository
                .SaveChangesAsync();
        }

        // =========================
        // SET DRAFT
        // =========================
        public async Task<bool> SetDraftAsync(int id)
        {
            var result =
                await _finalRepository
                    .GetByIdAsync(id);

            if (result == null)
            {
                return false;
            }

            result.Status =
                "Draft";

            result.PublishedAt =
                null;

            return await _finalRepository
                .SaveChangesAsync();
        }

        // =========================
        // GET PUBLISHED RESULT
        // =========================
        public async Task<FinalEvaluationResultResponse> GetPublishedResultAsync(Guid groupId)
        {
            var result =
                await _finalRepository
                    .GetPublishedByGroupIdAsync(groupId);

            if (result == null)
            {
                throw new Exception(
                    "No published result found");
            }

            return new FinalEvaluationResultResponse
            {
                Id =
                    result.Id,

                GroupId =
                    result.GroupId,

                SupervisorGrade =
                    result.SupervisorGrade,

                ExaminerGrade =
                    result.ExaminerGrade,

                FinalGrade =
                    result.FinalGrade,

                Status =
                    result.Status
            };
        }

        // =========================
        // GET STUDENT FINAL GRADE
        // =========================
        public async Task<StudentFinalGradeResponse> GetStudentFinalGradeAsync(Guid groupId)
        {
            var result =
                await _finalRepository
                    .GetPublishedByGroupIdAsync(groupId);

            if (result == null)
            {
                throw new Exception(
                    "No published result found");
            }

            return new StudentFinalGradeResponse
            {
                FinalScore =
                    result.FinalGrade
            };
        }

        // =========================
        // GET SUPERVISOR GROUPS
        // FINAL GRADES
        // =========================
        public async Task<List<SupervisorFinalGradeResponse>> GetSupervisorGroupsFinalGradesAsync( string supervisorId)
        {
            var results =
                await _finalRepository
                    .GetSupervisorResultsAsync(
                        supervisorId);

            return results
                .Select(r =>
                    new SupervisorFinalGradeResponse
                    {
                        GroupId =
                            r.GroupId,

                        GroupName =
                            r.Group.GroupName,

                        FinalGrade =
                            r.FinalGrade
                    })
                .ToList();
        }
        public async Task<List<CoordinatorFinalResultResponse>> GetAllFinalResultsAsync()
        {
            var results =
                await _finalRepository
                    .GetAllFinalResultsAsync();

            return results
                .Select(r =>
                    new CoordinatorFinalResultResponse
                    {
                        GroupId =
                            r.GroupId,

                        GroupName =
                            r.Group.GroupName,

                        SupervisorName =
                       r.Group.Supervisor.User.UserName,

                        SupervisorGrade =
                            r.SupervisorGrade,

                        ExaminerGrade =
                            r.ExaminerGrade,

                        FinalGrade =
                            r.FinalGrade,

                        Status =
                            r.Status
                    })
                .ToList();
        }
    }
}