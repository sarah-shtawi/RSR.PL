using RSR.DAL.Models.SemesterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.SemesterRepo
{
    public  interface ISemesterRepository
    {
        Task<Semester> CreateSemester(Semester semester);
        Task<List<Semester>> GetAllSemesters();
        Task<Semester> GetActiveSemester();
        Task<Semester> GetById(Guid semesterId);
        Task<Semester> UpdateSemester(Semester semester);
        Task<Semester> Delete(Semester semester);
        Task<List<Semester>> GetSemesterWithProjectsForArchive();

    }
}
