using RSR.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSR.DAL.Models.SemesterModel;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Models.ThesisModel;

namespace RSR.DAL.Repository.SemesterRepo
{
    public  class SemesterRepository : ISemesterRepository
    {
        private readonly ApplicationDbContext _context;

        public SemesterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Semester> CreateSemester (Semester semester)
        {
           await _context.AddAsync(semester);
           await _context.SaveChangesAsync();
           return semester;
        }
        public async Task<List<Semester>> GetAllSemesters()
        {
            var semesters = await _context.Semesters.ToListAsync();
            if(semesters is null)
            {
                return null;
            }
            return semesters;
        }
        public async Task<Semester> GetActiveSemester()
        {
            var semester = await _context.Semesters.FirstOrDefaultAsync(s=>s.IsActive == true);
            if (semester == null) 
            {
                return null;
            }
            return semester;
        }
        public async Task<Semester> GetById (Guid semesterId)
        {
            var semester = await _context.Semesters.FirstOrDefaultAsync(s=>s.SemesterId == semesterId && s.IsActive);
            if (semester == null) 
            {
                return null;
            }
            return semester;
        }
        public async Task<Semester> UpdateSemester (Semester semester )
        {
            _context.Update(semester);
             await _context.SaveChangesAsync();
             return semester;
        }
        public async Task<Semester> Delete(Semester semester)
        {
            semester.IsActive = false;
            _context.Update(semester);
            await _context.SaveChangesAsync();
            return semester;
        }


        public async Task<List<Semester>> GetSemesterWithProjectsForArchive()
        {
            var AllThesis = await _context.Semesters
                .Include(s => s.Groups).ThenInclude(g => g.Project)
                .Include(s => s.Groups).ThenInclude(g => g.Thesis).ThenInclude(th => th.ThesisVersions.Where(v => v.IsFrozen)).ToListAsync();
            return AllThesis;
        }

    }
}
