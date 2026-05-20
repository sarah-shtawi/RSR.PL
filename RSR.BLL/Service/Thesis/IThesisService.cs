using RSR.DAL.DTOs.Request.ThesisReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.ThesisRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Thesis
{
    public  interface IThesisService
    {
        System.Threading.Tasks.Task<BaseResponse> CreateThesis(ThesisRequest request, string supervisorId, Guid GroupId);

        System.Threading.Tasks.Task<BaseResponse> UpdateThesis(ThesisRequest request, string supervisorId, Guid ThesisId);

        Task<ThesisResponse> GetThesisByGroupId(Guid GroupId, string userId, string Role);
            }

}
