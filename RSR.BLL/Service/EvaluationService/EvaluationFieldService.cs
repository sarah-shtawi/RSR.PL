using RSR.DAL.DTOs.Request.EvaluationRequest;
using RSR.DAL.DTOs.Response.EvaluationResponse;
using RSR.DAL.Models.Evaluation;
using RSR.DAL.Enums;
using RSR.DAL.Repository.EvaluationRepository;
using RSR.DAL.DTOs.Response;

namespace RSR.BLL.Services.EvaluationService
{
    public class EvaluationFieldService : IEvaluationFieldService
    {
        private readonly IEvaluationFieldRepository _repository;
        private readonly IEvaluationFormRepository _formRepository;

        public EvaluationFieldService(
            IEvaluationFieldRepository repository,
            IEvaluationFormRepository formRepository)
        {
            _repository = repository;
            _formRepository = formRepository;
        }

        // HELPER
        private bool IsEditable(FormStatus status)
        {
            return status == FormStatus.Draft;
        }

       
        // CREATE FIELD
        public async Task<CreateEvaluationFieldResponse?> CreateAsync(CreateEvaluationFieldRequest request, int evaluationFormId)
        {
            try
            {
                var form = await _formRepository.GetByIdAsync(evaluationFormId);
                if (form == null)
                    return null;

                // Only Draft allowed
                if (!IsEditable(form.Status))
                    return null;

                // VALIDATION
                if (string.IsNullOrWhiteSpace(request.FieldName))
                    return null;

                if (request.MinValue > request.MaxValue)
                    return null;

                var field = new EvaluationField
                {
                    FieldName = request.FieldName,
                    MinValue = request.MinValue,
                    MaxValue = request.MaxValue,
                    IsRequired = request.IsRequired,
                    EvaluationFormId = evaluationFormId
                };

                var createdField = await _repository.CreateAsync(field);

                return new CreateEvaluationFieldResponse
                {
                    Id = createdField.Id,
                    FieldName = createdField.FieldName,
                    MinValue = createdField.MinValue,
                    MaxValue = createdField.MaxValue,
                    IsRequired = createdField.IsRequired
                };
            }
            catch (Exception ex)
            {
                return new CreateEvaluationFieldResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // DELETE FIELD
        public async Task<bool> DeleteAsync(int id)
        {
            var field = await _repository.GetByIdAsync(id);

            if (field == null)
                return false;

            var form = await _formRepository.GetByIdAsync(field.EvaluationFormId);

            if (form == null)
                return false;

            // ❌--- Only Draft allowed
            if (!IsEditable(form.Status))
                return false;

            return await _repository.DeleteAsync(id);
        }

        // UPDATE FIELD
        public async Task<UpdateEvaluationFieldResponse?> UpdateAsync( int id,UpdateEvaluationFieldRequest request)
        {
            try
            {
            var existingField = await _repository.GetByIdAsync(id);

            if (existingField == null)
                return null;

            var form = await _formRepository.GetByIdAsync(existingField.EvaluationFormId);

            if (form == null)
                return null;

            // Only Draft allowed
            if (!IsEditable(form.Status))
                return null;

            // VALIDATION
            if (string.IsNullOrWhiteSpace(request.FieldName))
                return null;

            if (request.MinValue > request.MaxValue)
                return null;

            // UPDATE
            existingField.FieldName = request.FieldName;
            existingField.MinValue = request.MinValue;
            existingField.MaxValue = request.MaxValue;
            existingField.IsRequired = request.IsRequired;

            var updatedField = await _repository.UpdateAsync(existingField);

            if (updatedField == null)
                return null;

            return new UpdateEvaluationFieldResponse
            {
                Id = updatedField.Id,
                FieldName = updatedField.FieldName,
                MinValue = updatedField.MinValue,
                MaxValue = updatedField.MaxValue,
                IsRequired = updatedField.IsRequired
            };
            }
            catch (Exception ex)
            {
                return new UpdateEvaluationFieldResponse
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}