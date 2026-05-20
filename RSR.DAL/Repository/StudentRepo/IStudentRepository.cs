using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.StudentRepo
{
    public  interface IStudentRepository
    {
        Task<List<StudentProfile>> GetStudentsByIds(List<string> studentsIds);
        Task<List<StudentProfile>>? GetCurrentStudentByGroupId(Guid groupId);
        Task<StudentProfile?> GetStudentById(string studentId);
    }
}
