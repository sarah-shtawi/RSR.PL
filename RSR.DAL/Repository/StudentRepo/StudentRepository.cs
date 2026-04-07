using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.StudentRepo
{
    public  class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task <List<StudentProfile>> GetStudentsByIds(List<string> studentsIds)
        {
            var students = await _context.Students
                .Where(s=> studentsIds.Contains(s.UserId))
                .ToListAsync();
            return students;
        }
        public async Task <List<StudentProfile>>? GetCurrentStudentByGroupId(Guid groupId)
        {
            var CurrentStudents = await _context.Students.Where(s =>s.GroupId == groupId).ToListAsync();
            if(CurrentStudents is null)
            {
                return null;
            }
            return CurrentStudents;
        }
    }
}
