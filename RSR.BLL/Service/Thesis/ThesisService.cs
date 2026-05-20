using Mapster;
using RSR.BLL.Service.Files;
using RSR.DAL.DTOs.Request.ThesisReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.ThesisRes;
using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ThesisRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Thesis
{
    public  class ThesisService : IThesisService
    {
        private readonly IThesisRepository _thesisRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IFileService _fileService;

        public ThesisService(IThesisRepository thesisRepository , IGroupRepository groupRepository , IFileService fileService)
        {
            _thesisRepository = thesisRepository;
            _groupRepository = groupRepository;
            _fileService = fileService;
        }

        public async System.Threading.Tasks.Task<BaseResponse> CreateThesis(ThesisRequest request , string supervisorId , Guid GroupId)
        {
            var group = await _groupRepository.GroupByIdRepo(GroupId);
            if (group == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Group Not found"
                };
            }
            if (group.SupervisorId != supervisorId) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "you can't add Thesis to this group"
                };
            }
            if (group.Thesis != null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This group already has a thesis"
                };
            }
            var ThesisDB = new DAL.Models.ThesisModel.Thesis
            {
                CreatedAt = DateTime.UtcNow,
                DeadLine = request.DeadLine, 
                GroupId = GroupId,
            };
            if (request.ThesisFile != null) 
            {
                try
                {
                    var ThesisFileName = await _fileService.UploadThesisFile(request.ThesisFile);
                    ThesisDB.ThesisFile = ThesisFileName;
                }
                catch (Exception ex) 
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };
                }
            }
            await _thesisRepository.CreateThesis(ThesisDB);
            return new BaseResponse
            {
                Success = true,
                Message = "Thesis Created Successfully"
            };

        }

        public async System.Threading.Tasks.Task<BaseResponse> UpdateThesis(ThesisRequest request, string supervisorId , Guid ThesisId)
        {
            var thesisDB = await _thesisRepository.GetThesisById(ThesisId);
            if (thesisDB == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Thesis Not Found"
                };
            }

            var group = await _groupRepository.GroupByIdRepo(thesisDB.GroupId);
            if (group == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Group Not found"
                };
            }
            if (group.SupervisorId != supervisorId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "you can't Updated Thesis to this group"
                };
            }
       

            if (request.ThesisFile != null)
            {
                try
                {
                    var ThesisFileName = await _fileService.UploadThesisFile(request.ThesisFile);
                    thesisDB.ThesisFile = ThesisFileName;
                }
                catch (Exception ex)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };
                }
            }
            thesisDB.DeadLine = request.DeadLine;
            await _thesisRepository.UpdateThesis(thesisDB);
            return new BaseResponse
            {
                Success = true,
                Message = "Thesis Updated Successfully"
            };

        }

        public async Task <ThesisResponse> GetThesisByGroupId(Guid GroupId , string userId , string Role)
        {
            var thesis = await _thesisRepository.GetThesisByGroupId(GroupId);
            if(thesis == null)
            {
                return new ThesisResponse
                {
                    Success = false,
                    Message = "This group has not uploaded the thesis file yet."
                };
            }
            if (Role == "Student")
            {
                if (!thesis.Group.Students.Any(s => s.UserId == userId))
                {
                    return new ThesisResponse
                    {
                        Success = false,
                        Message = "This student does not belong to this group."
                    };
                }
            }else if (Role == "Supervisor")
            {
                if (thesis.Group.SupervisorId != userId)
                {
                    return new ThesisResponse
                    {
                        Success = false,
                        Message = "You are not the supervisor of this group."
                    };
                }
            }
            var thesisResponse = thesis.Adapt<ThesisResponse>();
            thesisResponse.Success = true;
            thesisResponse.Message = "Success";
            return thesisResponse;

        }

    }
}
