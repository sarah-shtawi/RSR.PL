using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisRepo
{
    public  class ThesisRepository : IThesisRepository
    {
        private readonly ApplicationDbContext _context;

        public ThesisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <Thesis> CreateThesis(Thesis thesis)
        {
            await _context.Thesis.AddAsync(thesis);
            await _context.SaveChangesAsync();
            return thesis;
        }
        public async Task<Thesis> UpdateThesis(Thesis thesis)
        {
             _context.Thesis.Update(thesis);
            await _context.SaveChangesAsync();
            return thesis;
        }
        public async Task <Thesis?> GetThesisById(Guid ThesisId)
        {
            var thesis = await _context.Thesis.Where(th=>th.ThesisId == ThesisId).FirstOrDefaultAsync();
            return thesis;
        }

        public async Task<Thesis?> GetThesisByGroupId(Guid GroupId)
        {
            var Thesis = await _context.Thesis
                .Include(th => th.ThesisVersions).ThenInclude(v=>v.student).ThenInclude(v=>v.User)
                .Include(th => th.ThesisVersions).ThenInclude(v => v.thesisFeedbacks).ThenInclude(f=>f.Reviwer)
                .Include(th=>th.Group).ThenInclude(g=>g.Students).ThenInclude(s=>s.User)
                .Include(th=>th.Group).ThenInclude(g=>g.Supervisor).ThenInclude(s=>s.User)
                .FirstOrDefaultAsync(th=>th.GroupId==GroupId);
            return Thesis;
        } 


      
    }
}
