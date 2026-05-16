using Mapster;
using Mapster.Adapters;
using Microsoft.Extensions.Configuration;
using RSR.DAL.DTOs.Request.UserRequest;
using RSR.DAL.DTOs.Request.UserRequest.update;
using RSR.DAL.DTOs.Response.SemesterRes;
using RSR.DAL.DTOs.Response.TaskRes;
using RSR.DAL.DTOs.Response.ThesisRes;
using RSR.DAL.DTOs.Response.User;
using RSR.DAL.Models;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.ThesisModel;
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
            // Task Mapping
            TypeAdapterConfig<DAL.Models.TaskModel.Task, TaskResponse>.NewConfig()
              .Map(dest => dest.TaskFileURL, src => string.IsNullOrEmpty(src.TaskFileURL) ? null : $"{_configration["URL:BaseUrl"]}/files/Tasks/{src.TaskFileURL}")
              .Map(dest => dest.SupervisorName, src => src.Supervisor.User.FullName);

            TypeAdapterConfig<TaskSubmission, TaskSubmissionResponse>.NewConfig()
             .Map(dest => dest.TaskSubmissionURL, src => string.IsNullOrEmpty(src.SubmissionTaskFileURL) ? null : $"{_configration["URL:BaseUrl"]}/files/Tasks/{src.SubmissionTaskFileURL}")
                          .Map(dest => dest.StudentName, src => src.Student.User.FullName);

             TypeAdapterConfig<TaskSubmissionComment, TaskSubmissionCommentResponse>.NewConfig()
                       .Map(dest => dest.UserName, src => src.User.FullName);
 

            TypeAdapterConfig<DAL.Models.TaskModel.Task, TaskDetailsResponse>.NewConfig()
          .Map(dest => dest.SupervisorName, src => src.Supervisor.User.FullName);

            // Thesis mapping
            TypeAdapterConfig<Thesis, ThesisResponse>.NewConfig()
        .Map(dest => dest.ThesisFile, src => string.IsNullOrEmpty(src.ThesisFile) ? null : $"{_configration["URL:BaseUrl"]}/files/Thesis/{src.ThesisFile}")
            .Map(dest => dest.SupervisorName, src => src.Group.Supervisor.User.FullName);

            TypeAdapterConfig<ThesisVersions, ThesisVersionResponse>.NewConfig()
                .Map(dest => dest.FileURL, src => string.IsNullOrEmpty(src.FileURL) ? null : $"{_configration["URL:BaseUrl"]}/files/Thesis/{src.FileURL}")
                .Map(dest => dest.studentName, src => src.student.User.FullName);

            TypeAdapterConfig<ThesisFeedback, ThesisFeedbackResponse>.NewConfig()
              .Map(dest => dest.ReviwerName, src => src.Reviwer.FullName);

            TypeAdapterConfig<ThesisVersions, ThesisArchiveHomePageResponse>.NewConfig()
             .Map(dest => dest.ThesisFile, src => string.IsNullOrEmpty(src.FileURL) ? null : $"{_configration["URL:BaseUrl"]}/files/Thesis/{src.FileURL}")
             .Map(dest => dest.ProjectIdea, src => src.Thesis.Group.Project.ProjectIdea)
             .Map(dest => dest.ProjectName, src => src.Thesis.Group.Project.ProjectName)
             .Map(dest => dest.ThesisVersionId,src => src.VersionId)
             .Map(dest=>dest.PublishedAt , src=>src.PublishedAt);

            TypeAdapterConfig<Semester, SemesterArchive>.NewConfig()
            .Map(dest => dest.Projects, src => src.Groups.Where(g => g.Thesis != null && g.Thesis.ThesisVersions.Any(v => v.IsFrozen))
            .SelectMany(g => g.Thesis.ThesisVersions.Where(v => v.IsFrozen)).ToList());

        }

    }
}
