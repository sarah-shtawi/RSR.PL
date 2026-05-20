using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class EvaluationFieldRepository : IEvaluationFieldRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationFieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }

         // CREATE FIELD
         public async Task<EvaluationField> CreateAsync(EvaluationField field)
        {
            await _context.EvaluationFields.AddAsync(field);
            await _context.SaveChangesAsync();

            return field;
        }

         // GET FIELD BY ID
         public async Task<EvaluationField?> GetByIdAsync(int id)
        {
            return await _context.EvaluationFields
                .FirstOrDefaultAsync(f => f.Id == id);
        }

         // DELETE FIELD
         public async Task<bool> DeleteAsync(int id)
        {
            var field = await _context.EvaluationFields
                .FirstOrDefaultAsync(f => f.Id == id);

            if (field == null)
                return false;

            _context.EvaluationFields.Remove(field);

            await _context.SaveChangesAsync();

            return true;
        }

         // UPDATE FIELD
         public async Task<EvaluationField?> UpdateAsync(EvaluationField field)
        {
            var existingField = await _context.EvaluationFields
                .FirstOrDefaultAsync(f => f.Id == field.Id);

            if (existingField == null)
                return null;

            existingField.FieldName = field.FieldName;
            existingField.MinValue = field.MinValue;
            existingField.MaxValue = field.MaxValue;
            existingField.IsRequired = field.IsRequired;

            await _context.SaveChangesAsync();

            return existingField;
        }
    }
}