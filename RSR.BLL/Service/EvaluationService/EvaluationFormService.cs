using Microsoft.AspNetCore.Http;
using RSR.DAL.DTOs.Request.EvaluationRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;
using RSR.DAL.Models.Evaluation;
using RSR.DAL.Enums;
using RSR.DAL.Repository.EvaluationRepository;
using System.Security.Claims;

namespace RSR.BLL.Services.EvaluationService
{
    public class EvaluationFormService : IEvaluationFormService
    {
        private readonly IEvaluationFormRepository
            _repository;

        private readonly IEvaluationSubmissionRepository
            _submissionRepository;

        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public EvaluationFormService(
            IEvaluationFormRepository repository,
            IEvaluationSubmissionRepository submissionRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;

            _submissionRepository =
                submissionRepository;

            _httpContextAccessor =
                httpContextAccessor;
        }

         // CREATE FORM
         public async Task<CreateEvaluationFormResponse> CreateAsync( CreateEvaluationFormRequest request)
        {
            try
            {
                var form = new EvaluationForm
                {
                    Title = request.Title,
                    AssignTo = request.AssignTo,
                    Description = request.Description,
                    Status = FormStatus.Draft,
                    CreatedAt = DateTime.UtcNow,
                    Fields = new List<EvaluationField>()
                };

                if (request.Fields != null && request.Fields.Any())
                {
                    foreach (var field in request.Fields)
                    {
                        form.Fields.Add(new EvaluationField
                        {
                            FieldName = field.FieldName,

                            MinValue = field.MinValue,

                            MaxValue = field.MaxValue,

                            IsRequired = field.IsRequired
                        });
                    }
                }

                var createdForm = await _repository.CreateAsync(form);

                return new CreateEvaluationFormResponse
                {
                    Id = createdForm.Id,
                    Title = createdForm.Title,
                    AssignTo = createdForm.AssignTo,
                    Description = createdForm.Description,
                    Status = createdForm.Status,
                    CreatedAt = createdForm.CreatedAt,
                    Fields = createdForm.Fields
                        .Select(f =>
                            new CreateEvaluationFieldResponse
                            {
                                Id = f.Id,
                                FieldName = f.FieldName,
                                MinValue = f.MinValue,
                                MaxValue = f.MaxValue,
                                IsRequired = f.IsRequired
                            }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new CreateEvaluationFormResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

         // GET FORM WITH FIELDS
         public async Task<CreateEvaluationFormResponse> GetByIdAsync(int id)
           {
            try
            {
            var form = await _repository.GetByIdWithFieldsAsync(id);

            if (form == null)
            {
                throw new Exception("Evaluation Form not found");
            }

            return new CreateEvaluationFormResponse
            {
                Id = form.Id,
                Title = form.Title,
                AssignTo = form.AssignTo,
                Description = form.Description,
                Status = form.Status,
                CreatedAt = form.CreatedAt,

                Fields = form.Fields
                    .Select(f =>
                        new CreateEvaluationFieldResponse
                        {
                            Id = f.Id,
                            FieldName = f.FieldName,
                            MinValue = f.MinValue,
                            MaxValue = f.MaxValue,
                            IsRequired = f.IsRequired
                        }).ToList()
            };
            }
            catch (Exception ex)
            {
                return new CreateEvaluationFormResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

         // PUBLISH FORM
         public async Task<bool> PublishAsync(int id)
        {
            var form = await _repository.GetByIdAsync(id);

            if (form == null)
            {
                return false;
            }

            if (form.Status == FormStatus.Archived)
            {
                return false;
            }

            form.Status = FormStatus.Published;

            await _repository.UpdateAsync(form);

            return true;
        }

         // SET DRAFT
         public async Task<bool> SetDraftAsync(int id)
        {
            var form =
                await _repository
                    .GetByIdAsync(id);

            if (form == null)
            {
                return false;
            }

            if (form.Status
                == FormStatus.Archived)
            {
                return false;
            }

            form.Status =
                FormStatus.Draft;

            await _repository
                .UpdateAsync(form);

            return true;
        }

         // ARCHIVE FORM
        
        public async Task<bool> ArchiveAsync(int id)
        {
            var form =
                await _repository
                    .GetByIdAsync(id);

            if (form == null)
            {
                return false;
            }

            if (form.Status
                == FormStatus.Archived)
            {
                return false;
            }

            form.Status =
                FormStatus.Archived;

            await _repository
                .UpdateAsync(form);

            return true;
        }


        // =========================
        // GET ALL FORMS
        // =========================
        public async Task<List<CreateEvaluationFormResponse>>  GetAllFormsAsync()
        {
            var forms = await _repository.GetAllFormsAsync();

            return forms.Select(form =>
                new CreateEvaluationFormResponse
                {
                    Id = form.Id,
                    Title = form.Title,
                    AssignTo = form.AssignTo,
                    Description = form.Description,
                    Status = form.Status,
                    CreatedAt = form.CreatedAt,

                    Fields = form.Fields
                        .Select(f =>
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
        // UPDATE FORM
        // =========================
        public async Task<UpdateEvaluationFormResponse?> UpdateAsync(
            int id,
            UpdateEvaluationFormRequest request)
        {
            try
            {
            var form = await _repository.GetByIdAsync(id);

            if (form == null)
            {
                return null;
            }

            if (form.Status == FormStatus.Archived)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace( request.Title))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace( request.AssignTo))
            {
                return null;
            }

            form.Title = request.Title;

            form.AssignTo = request.AssignTo;

            form.Description =
                request.Description;

            var updatedForm =
                await _repository
                    .UpdateAsync(form);

            return new UpdateEvaluationFormResponse
            {
                Id = updatedForm.Id,
                Title = updatedForm.Title,
                AssignTo = updatedForm.AssignTo,
                Description = updatedForm.Description,
                Status = updatedForm.Status
            };
            }
            catch (Exception ex)
            {
                return new UpdateEvaluationFormResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        // =========================
        // GET ALL PUBLISHED FORMS
        // =========================
        public async Task<List<CreateEvaluationFormResponse>>GetPublishedFormsAsync()
        {
            var forms = await _repository.GetPublishedFormsAsync();

            return forms.Select(form =>
                new CreateEvaluationFormResponse
                {
                    Id = form.Id,
                    Title = form.Title,
                    AssignTo = form.AssignTo,
                    Description = form.Description,
                    Status = form.Status,
                    CreatedAt = form.CreatedAt
                }).ToList();
        }



        // =========================
        // GET ALL FORMS
        // =========================
        public async Task<List<CreateEvaluationFormResponse>>
            GetAllFormsAsync()
        {
            var forms =
                await _repository
                    .GetAllFormsAsync();

            return forms.Select(form =>
                new CreateEvaluationFormResponse
                {
                    Id = form.Id,
                    Title = form.Title,
                    AssignTo = form.AssignTo,
                    Description = form.Description,
                    Status = form.Status,
                    CreatedAt = form.CreatedAt,

                    Fields = form.Fields
                        .Select(f =>
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
        // GET MY FORMS
        public async Task<List<CreateEvaluationFormResponse>>  GetMyFormsAsync()
        {
            var userRole = _httpContextAccessor.HttpContext?.User
                .Claims
                .FirstOrDefault(c =>
                    c.Type == ClaimTypes.Role)
                ?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception(
                    "User role not found");
            }

            var forms =
                await _repository
                    .GetPublishedFormsByRoleAsync(
                        userRole);

            return forms.Select(form =>
                new CreateEvaluationFormResponse
                {
                    Id = form.Id,
                    Title = form.Title,
                    AssignTo = form.AssignTo,
                    Description = form.Description,
                    Status = form.Status,
                    CreatedAt = form.CreatedAt,

                    Fields = form.Fields
                        .Select(f =>
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

         // DELETE FORM
         public async Task<bool>DeleteAsync(int id)
        {
            var form =
                await _repository
                    .GetByIdAsync(id);

            if (form == null)
            {
                return false;
            }

            if (form.Status
                != FormStatus.Draft)
            {
                return false;
            }

            return await _repository
                .DeleteAsync(id);
        }

         // DASHBOARD STATISTICS
         public async Task<DashboardStatisticsResponse> GetDashboardStatisticsAsync()
        {
            var totalForms =
                await _repository.CountAsync();

            var publishedForms =
                await _repository.CountPublishedAsync();

            var totalEvaluations =
                await _submissionRepository.CountAsync();

            return new DashboardStatisticsResponse
            {
                TotalForms =
                    totalForms,

                PublishedForms =
                    publishedForms,

                TotalEvaluations =
                    totalEvaluations
            };
        }
    }
}