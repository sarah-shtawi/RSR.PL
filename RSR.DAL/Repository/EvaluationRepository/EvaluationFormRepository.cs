using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Enums;
using RSR.DAL.Models.Evaluation;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public class EvaluationFormRepository
        : IEvaluationFormRepository
    {
        private readonly ApplicationDbContext
            _context;

        public EvaluationFormRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE FORM
        // =========================
        public async Task<EvaluationForm>
            CreateAsync(EvaluationForm form)
        {
            await _context.EvaluationForms
                .AddAsync(form);

            await _context.SaveChangesAsync();

            return form;
        }

        // =========================
        // GET FORM BY ID
        // =========================
        public async Task<EvaluationForm?>
            GetByIdAsync(int id)
        {
            return await _context.EvaluationForms
                .FirstOrDefaultAsync(f =>
                    f.Id == id);
        }

        // =========================
        // GET FORM WITH FIELDS
        // =========================
        public async Task<EvaluationForm?>
            GetByIdWithFieldsAsync(int id)
        {
            return await _context.EvaluationForms
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(f =>
                    f.Id == id);
        }

        // =========================
        // UPDATE FORM
        // =========================
        public async Task<EvaluationForm>
            UpdateAsync(EvaluationForm form)
        {
            _context.EvaluationForms
                .Update(form);

            await _context.SaveChangesAsync();

            return form;
        }

        // =========================
        // GET ALL PUBLISHED FORMS
        // =========================
        public async Task<List<EvaluationForm>>
            GetPublishedFormsAsync()
        {
            return await _context.EvaluationForms
                .Where(f =>
                    f.Status == FormStatus.Published)
                .Include(f => f.Fields)
                .ToListAsync();
        }

        // =========================
        // GET ALL FORMS
        // =========================
        public async Task<List<EvaluationForm>>
            GetAllFormsAsync()
        {
            return await _context.EvaluationForms
                .Include(f => f.Fields)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        } 

        // =========================
        // DELETE DRAFT FORM
        // =========================
        public async Task<bool>
            DeleteAsync(int id)
        {
            var form = await _context.EvaluationForms
                .FirstOrDefaultAsync(f =>
                    f.Id == id);

            if (form == null)
            {
                return false;
            }

            _context.EvaluationForms
                .Remove(form);

            await _context.SaveChangesAsync();

            return true;
        }
        // =========================
        // GET ALL FORMS
        // =========================
        public async Task<List<EvaluationForm>>
            GetAllFormsAsync()
        {
            return await _context.EvaluationForms
                .Include(f => f.Fields)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        // =========================
        // GET FORMS BY ROLE
        // =========================
        public async Task<List<EvaluationForm>>GetPublishedFormsByRoleAsync(string role)
        {
            return await _context.EvaluationForms
                .Where(f =>
                    f.Status == FormStatus.Published &&
                    f.AssignTo.ToString() == role)
                .Include(f => f.Fields)
                .ToListAsync();
        }

        // =========================
        // COUNT ALL FORMS
        // =========================
        public async Task<int>CountAsync()
        {
            return await _context.EvaluationForms.CountAsync();
        }

        // =========================
        // COUNT PUBLISHED FORMS
        // =========================
        public async Task<int>CountPublishedAsync()
        {
            return await _context.EvaluationForms.CountAsync(f =>f.Status == FormStatus.Published);
        }
    }
}