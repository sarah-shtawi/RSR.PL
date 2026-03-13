using Mapster;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.mapsterConfigration
{
    public  class mapsterConfig
    {
        public static void MapterConfigRegiter()
        {
            TypeAdapterConfig<StudentProfile, AssignUserRequest>.NewConfig()
                .Map(dest => dest.MainImage, src => $"https://localhost:7137/images/{src.PictureProfileURL}");

        }

    }
}
