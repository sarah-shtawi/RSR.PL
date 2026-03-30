using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RSR.BLL.Service.Semester;
using RSR.DAL.DTOs.Request.semester;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.SemesterRes;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Repository.SemesterRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.semesterService
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterService(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }
        public async Task<BaseResponse> CreateSemester(CreateSemesterRequest request)
        {
            var semesterDB = request.Adapt<RSR.DAL.Models.SemesterModel.Semester>();
            var allSemesters = await _semesterRepository.GetAllSemesters();
            if (allSemesters.Count != 0)
            {
                foreach (var semester in allSemesters)
                {
                    if (semester.IsActive == true)
                    {
                        semester.IsActive = false;
                        await _semesterRepository.UpdateSemester(semester);
                    }
                }
            }
            semesterDB.IsActive = true;
            if (semesterDB.StartDate >= semesterDB.EndDate)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "The Date's was a problem"
                };
            }
            var semesterAdd = await _semesterRepository.CreateSemester(semesterDB);
            return new BaseResponse
            {
                Success = true,
                Message = "Semester Added Successfully"
            };
        }
        public async Task<SemesterResponse> GetActiveSemester()
        {
            var semesterDB = await _semesterRepository.GetActiveSemester();
            if(semesterDB is null)
            {
                return null;
            }
            var semesterRes = semesterDB.Adapt<SemesterResponse>();
            return semesterRes;
        }
        public async Task<List<SemesterResponse>> GetAllSemesters()
        {
            var semestersDB = await _semesterRepository.GetAllSemesters();
            if(semestersDB is null)
            {
                return null;
            }
            var semestersRes = semestersDB.Adapt<List<SemesterResponse>>();
            return semestersRes;
        }


        public async Task<BaseResponse> UpdateSemester(Guid Id,CreateSemesterRequest request)
        {
            var semester = await _semesterRepository.GetById(Id);
            if(semester is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "semester not found"
                };
            }
            if (request.StartDate >= request.EndDate)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "The Date's was a problem"
                };
            }
            semester.Name = request.Name;
            semester.StartDate = request.StartDate;
            semester.EndDate = request.EndDate;
            await _semesterRepository.UpdateSemester(semester);
            return new BaseResponse
            {
                Success = true,
                Message = "Semester Updated Successfully"
            };
        }
           
    }
}
