using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Enums;
using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class EvaluationFormRepository : IEvaluationFormRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EvaluationForm> CreateAsync(EvaluationForm form)
        {
            await _context.EvaluationForms.AddAsync(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<EvaluationForm?> GetByIdAsync(int id)
        {
            return await _context.EvaluationForms.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<EvaluationForm?> GetByIdWithFieldsAsync(int id)
        {
            return await _context.EvaluationForms
                 .Include(f => f.Fields)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<EvaluationForm> UpdateAsync(EvaluationForm form)
        {
            _context.EvaluationForms.Update(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<List<EvaluationForm>> GetAllFormsAsync()
        {
            return await _context.EvaluationForms
                .Include(f => f.Fields)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<EvaluationForm>> GetPublishedFormsAsync()
        {
            return await _context.EvaluationForms
                .Where(f => f.Status == FormStatus.Published)
                .Include(f => f.Fields)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var form = await _context.EvaluationForms
                .FirstOrDefaultAsync(f => f.Id == id);

            if (form == null) return false;

            _context.EvaluationForms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EvaluationForm>> GetPublishedFormsByRoleAsync(string role)
        {
            return await _context.EvaluationForms
                .Where(f =>  f.Status == FormStatus.Published && f.AssignTo == role)
                .Include(f => f.Fields)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.EvaluationForms.CountAsync();
        }

        public async Task<int> CountPublishedAsync()
        {
            return await _context.EvaluationForms
                .CountAsync(f => f.Status == FormStatus.Published);
        }

        public async Task<List<EvaluationForm>> GetFormsBySemesterAsync(Guid semesterId)
        {
            return await _context.EvaluationForms
                .Where(f => f.SemesterId == semesterId)
                .Include(f => f.Fields)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<EvaluationForm>> GetPublishedFormsBySemesterAsync(Guid semesterId)
        {
            return await _context.EvaluationForms
                .Where(f => f.SemesterId == semesterId && f.Status == FormStatus.Published)
                .Include(f => f.Fields)
                .ToListAsync();
        }

        public async Task<List<EvaluationForm>> GetPublishedFormsByRoleAndSemesterAsync(string role, Guid semesterId)
        {
            return await _context.EvaluationForms
                .Where(f =>
                    f.SemesterId == semesterId &&
                    f.Status == FormStatus.Published &&
                    f.AssignTo == role)
                .Include(f => f.Fields)
                .ToListAsync();
        }
    }
}