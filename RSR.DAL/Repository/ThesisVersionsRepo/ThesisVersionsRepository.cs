using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisVersionsRepo
{
    public  class ThesisVersionsRepository : IThesisVersionsRepository
    {
        private readonly ApplicationDbContext _context;

        public ThesisVersionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddThesisVersion(ThesisVersions thesis)
        {
            var thesisDB = await _context.ThesisVersions.AddAsync(thesis);
            await _context.SaveChangesAsync();
        }
        public async Task<ThesisVersions?> GetLastVersion(Guid ThesisId)
        {
            var LastVersion = await _context.ThesisVersions.Where(v => v.ThesisId == ThesisId && v.IsLatest).FirstOrDefaultAsync();
            return LastVersion;
        }
        public async Task UpdateThesisVersion(ThesisVersions version)
        {
             _context.Update(version);
            await _context.SaveChangesAsync();
        } 
        public async Task<bool> HasFrozenThesis(Guid ThesisId)
        {
            return await _context.ThesisVersions.AnyAsync(v=>v.ThesisId == ThesisId && v.IsFrozen);
        }
        public async Task <ThesisVersions?> GetVersionById(Guid VersionId)
        {
            var version = await _context.ThesisVersions.FindAsync(VersionId);
            return version;
        }
        public async Task<ThesisVersions?> GetVersionByIdWithSupervisor(Guid VersionId)
        {
            var version = await _context.ThesisVersions
                .Include(v=>v.Thesis)
                .ThenInclude(th=>th.Group)
                .ThenInclude(g=>g.Supervisor).ThenInclude(s=>s.User)
                .FirstOrDefaultAsync(v=>v.VersionId == VersionId);
            return version;
        }

        public async Task <List<ThesisVersions>> GetPublishedThesis()
        {
            var publishedVersions = await _context.ThesisVersions.Where(v => v.IsPublished)
            .Include(v => v.Thesis).ThenInclude(t => t.Group).ThenInclude(g => g.Project)
             .OrderByDescending(v => v.PublishedAt).ToListAsync();
              return publishedVersions;
        }


    }
}
