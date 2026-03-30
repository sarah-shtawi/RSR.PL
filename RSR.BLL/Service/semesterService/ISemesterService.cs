using RSR.DAL.DTOs.Request.semester;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.SemesterRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Semester
{
    public  interface ISemesterService
    {
        Task<BaseResponse> CreateSemester(CreateSemesterRequest request);
        Task<SemesterResponse> GetActiveSemester();
        Task<List<SemesterResponse>> GetAllSemesters();
        Task<BaseResponse> UpdateSemester(Guid Id, CreateSemesterRequest request);
    }
}
