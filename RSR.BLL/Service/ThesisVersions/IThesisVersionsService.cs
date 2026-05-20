using RSR.DAL.DTOs.Request.ThesisReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.ThesisRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.ThesisVersions
{
    public  interface IThesisVersionsService
    {
        Task<BaseResponse> AddThesisVersion(ThesisVersionRequest request, string studentId, Guid ThesisId);
        Task<BaseResponse> UpdateThesisVersion(ThesisVersionRequest request, string studentId, Guid ThesisVersionId);
        Task<BaseResponse> ReviewThesisVersion(string supervisorId, Guid VersionId, ReviewThesisRequest request);

        Task<BaseResponse> PublishThesisVersion(Guid versionId);
        Task<List<ThesisArchiveHomePageResponse>> GetThesisHomePage();
    }
}
