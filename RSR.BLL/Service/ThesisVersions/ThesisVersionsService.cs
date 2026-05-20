using Mapster;
using RSR.BLL.Service.Files;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.ThesisReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.ThesisRes;
using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ThesisFeedBackRepo;
using RSR.DAL.Repository.ThesisRepo;
using RSR.DAL.Repository.ThesisVersionsRepo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.ThesisVersions
{
    public  class ThesisVersionsService : IThesisVersionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IThesisRepository _thesisRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IFileService _fileService;
        private readonly IThesisVersionsRepository _versionsRepository;
        private readonly IThesisFeedBackRepository _thesisFeedBack;


        public ThesisVersionsService(ApplicationDbContext context , IThesisRepository thesisRepository ,
            IGroupRepository groupRepository , IFileService fileService , IThesisVersionsRepository versionsRepository , IThesisFeedBackRepository thesisFeedBack)
        {
            _context = context;
            _thesisRepository = thesisRepository;
            _groupRepository = groupRepository;
            _fileService = fileService;
            _versionsRepository = versionsRepository;
            _thesisFeedBack = thesisFeedBack;
        }
        public async Task<BaseResponse> AddThesisVersion(ThesisVersionRequest request, string studentId, Guid ThesisId)
        {
            var Thesis = await _thesisRepository.GetThesisById(ThesisId);
            if (Thesis == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Thesis not found"
                };
            }
            var group = await _groupRepository.GroupByIdRepo(Thesis.GroupId);
            if (group == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Group Not Found"
                };
            }
            if (!group.Students.Any(s => s.User.Id == studentId))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Student Not member in this group"
                };
            }
            if (request.ThesisVersionFile == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "File is required"
                };
            }
            var HasFrozenThesis = await _versionsRepository.HasFrozenThesis(ThesisId);
            if (HasFrozenThesis) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't upload new version after final submission"
                };
            }
           
            var LastVersion = await _versionsRepository.GetLastVersion(Thesis.ThesisId);
            int newVersion = 1;
            if(LastVersion is not null)
            {
                var LastFeedBack = await _thesisFeedBack.GetLastFeedback(LastVersion.VersionId);
              // no feedback
                if (LastFeedBack is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Previous Version has not been reviewd yet "
                    };
                }
                // feedback is  Approved
                else if (LastFeedBack.Decision == Decision.Approved)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Cannot upload new version after acceptance"
                    };
                }
                LastVersion.IsLatest = false;
                await _versionsRepository.UpdateThesisVersion(LastVersion);
                newVersion = LastVersion.VersionNumber +  1;
            }
            string? ThesisFileName = null;
            if (request.ThesisVersionFile != null)
            {
                try
                {
                    var ThesisName = await _fileService.UploadThesisFile(request.ThesisVersionFile);
                    ThesisFileName = ThesisName;
                }
                catch (Exception ex)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ex.Message,
                    };
                }
            }
            var version = new RSR.DAL.Models.ThesisModel.ThesisVersions
            {
                FileURL = ThesisFileName,
                VersionNumber = newVersion,
                IsLatest = true ,
                UploadedAt = DateTime.UtcNow,
                studentId = studentId,
                ThesisId = Thesis.ThesisId,
            };
            await _versionsRepository.AddThesisVersion(version);
            return new BaseResponse
            {
                Success = true , 
                Message = "Thesis Uploaded Successfully"
            };
        }

        public async Task<BaseResponse> UpdateThesisVersion(ThesisVersionRequest request, string studentId, Guid ThesisVersionId)
        {
            var ThesisVersion = await _versionsRepository.GetVersionById(ThesisVersionId);
            if (ThesisVersion == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Thesis version not found"
                };
            }
          
            if (ThesisVersion.studentId != studentId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Student Can not Updated this version"
                };
            }
            if (request.ThesisVersionFile == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "File is required"
                };
            }
            var hasFeedBack = await _thesisFeedBack.HasFeedBack(ThesisVersionId);
            if (hasFeedBack)
            {
                return new BaseResponse
                {
                    Success= false ,
                    Message="Can't Update version has feedback"
                };
            }
            string? ThesisFileName = null;
            if (request.ThesisVersionFile != null)
            {
                try
                {
                    var ThesisName = await _fileService.UploadThesisFile(request.ThesisVersionFile);
                    ThesisFileName = ThesisName;
                }
                catch (Exception ex)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ex.Message,
                    };
                }
            }
            if (!string.IsNullOrEmpty(ThesisFileName))
            {
                ThesisVersion.FileURL = ThesisFileName;
            }
            await _versionsRepository.UpdateThesisVersion(ThesisVersion);
        
            return new BaseResponse
            {
                Success = true,
                Message = "Thesis Updated Successfully"
            };
        }

        public async Task<BaseResponse> ReviewThesisVersion(string supervisorId , Guid VersionId , ReviewThesisRequest request)
        {
            var version = await _versionsRepository.GetVersionByIdWithSupervisor(VersionId);
            if (version == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "version is not found "
                };
            }
            if (version.Thesis.Group.Supervisor.User.Id != supervisorId) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Not authorized to give feedback"
                };
            }
            var existingFeedback = await _thesisFeedBack.GetByVersionAndReviewer(VersionId, supervisorId);

            if (existingFeedback != null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Feedback already exists for this version"
                };
            }
            if(request.Decision == Decision.Rejected && string.IsNullOrEmpty(request.Feedback))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You must provide a reason for rejection"
                };
            }
            var feedback = new ThesisFeedback
            {
                Decision = request.Decision,
                Feedback = request.Feedback,
                CreateAt = DateTime.UtcNow,
                VersionId= version.VersionId,
                ReviwerId = supervisorId
            };
            await _thesisFeedBack.AddFeedback(feedback);
            return new BaseResponse
            {
                Success = true,
                Message = " feedBack added Successfully"
            }; 
        }

         public async Task<BaseResponse> PublishThesisVersion(Guid versionId)
         {
            var version = await _versionsRepository.GetVersionById(versionId);
            if(version == null)
            {
                return new BaseResponse
                {
                    Success = false , 
                    Message = "This Version Not Found"
                };
            }
            if (!version.IsFrozen)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Only frozen thesis can be published"
                };
            }
            if (version.IsPublished)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "thesis is published"
                };
            }
            version.IsPublished = true;
            version.PublishedAt = DateTime.UtcNow;
            await _versionsRepository.UpdateThesisVersion(version);
            return new BaseResponse
            {
                Success = true,
                Message = "Thesis published Successfully"
            };
        }

        public async Task <List<ThesisArchiveHomePageResponse>> GetThesisHomePage()
        {
            var publishedThesis = await _versionsRepository.GetPublishedThesis();
            var HomePageThesis = publishedThesis.Adapt<List<ThesisArchiveHomePageResponse>>();
            return HomePageThesis;
        }
    }
}
