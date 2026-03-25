using Mapster;
using Microsoft.Extensions.Configuration;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Response.User;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.mapsterConfigration
{
    public class mapsterConfig
    {
        private readonly  IConfiguration _configration;

        public mapsterConfig(IConfiguration configration)
        {
            _configration = configration;
        }
        public  void MapterConfigRegiter()
        {
            //TypeAdapterConfig<StudentProfile, AssignUserRequest>.NewConfig()
            //    .Map(dest => dest.MainImage, src => src.PictureProfileURL);

            TypeAdapterConfig<StudentProfile, StudentGetResponse>.NewConfig()
           .Map(dest => dest.Id, src => src.User.Id)
           .Map(dest => dest.FullName, src => src.User.FullName)
           .Map(dest => dest.UserName, src => src.User.UserName)
           .Map(dest => dest.Email, src => src.User.Email)
           .Map(dest => dest.PictureProfileURL,  src => string.IsNullOrEmpty(src.PictureProfileURL)
                                    ? null : $"{_configration["URL:BaseUrl"]}/images/{src.PictureProfileURL}");

            TypeAdapterConfig<SupervisorProfile, SupervisorGetResponse>.NewConfig()
            .Map(dest => dest.Id, src => src.User.Id)
            .Map(dest => dest.FullName, src => src.User.FullName)
            .Map(dest => dest.UserName, src => src.User.UserName)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.PictureProfileURL, src => string.IsNullOrEmpty(src.PictureProfileURL)
                                  ? null : $"{_configration["URL:BaseUrl"]}/images/{src.PictureProfileURL}");

            TypeAdapterConfig<CoordinatorProfile, CoordinatorGetResponse>.NewConfig()
           .Map(dest => dest.Id, src => src.User.Id)
           .Map(dest => dest.FullName, src => src.User.FullName)
           .Map(dest => dest.UserName, src => src.User.UserName)
           .Map(dest => dest.Email, src => src.User.Email)
           .Map(dest => dest.PictureProfileURL, src => string.IsNullOrEmpty(src.PictureProfileURL)
                             ? null : $"{_configration["URL:BaseUrl"]}/images/{src.PictureProfileURL}");

            TypeAdapterConfig<ExaminerProfile, ExaminerGetResponse>.NewConfig()
           .Map(dest => dest.Id, src => src.User.Id)
           .Map(dest => dest.FullName, src => src.User.FullName)
           .Map(dest => dest.UserName, src => src.User.UserName)
           .Map(dest => dest.Email, src => src.User.Email)
           .Map(dest => dest.PictureProfileURL, src => string.IsNullOrEmpty(src.PictureProfileURL)
                          ? null : $"{_configration["URL:BaseUrl"]}/images/{src.PictureProfileURL}");



            TypeAdapterConfig<SupervisorProfile, AssignCoordinaterRequest>.NewConfig()
                .Map(dest => dest.CoordinatorNumber , src => src.SupervisorNumber);
      
        }

    }
}
