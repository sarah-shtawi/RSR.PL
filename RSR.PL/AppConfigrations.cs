using Microsoft.AspNetCore.Identity;
using RSR.BLL.Service.Authentication;
using RSR.BLL.Service.EmailSender;
<<<<<<< HEAD
using RSR.BLL.Service.EvaluationService;
=======
>>>>>>> origin/master
using RSR.BLL.Service.Files;
using RSR.BLL.Service.GroupService;
using RSR.BLL.Service.Semester;
using RSR.BLL.Service.semesterService;
using RSR.BLL.Service.Task;
using RSR.BLL.Service.TaskSubmission;
<<<<<<< HEAD
using RSR.BLL.Service.Token;
using RSR.BLL.Service.Users;
using RSR.BLL.Services.EvaluationService;

using RSR.DAL.Repository.EvaluationRepository;
=======
using RSR.BLL.Service.Thesis;
using RSR.BLL.Service.ThesisVersions;
using RSR.BLL.Service.Token;
using RSR.BLL.Service.Users;
>>>>>>> origin/master
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ProjectRepo;
using RSR.DAL.Repository.SemesterRepo;
using RSR.DAL.Repository.StudentRepo;
using RSR.DAL.Repository.SubmissionCommentRepo;
using RSR.DAL.Repository.TaskRepo;
using RSR.DAL.Repository.TaskSubmissionRepo;
<<<<<<< HEAD
=======
using RSR.DAL.Repository.ThesisFeedBackRepo;
using RSR.DAL.Repository.ThesisRepo;
using RSR.DAL.Repository.ThesisVersionsRepo;
>>>>>>> origin/master
using RSR.DAL.Utils;

namespace RSR.PL
{
    public static class AppConfigrations
    {
        public static void Config(IServiceCollection Services)
        {
<<<<<<< HEAD
            // =========================
            // SEED DATA
            // =========================
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();


            // =========================
            // AUTHENTICATION
            // =========================
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ITokenService, TokenService>();


            // =========================
            // EMAIL
            // =========================
            Services.AddScoped<IEmailSenderService, EmailSenderService>();


            // =========================
            // USER
            // =========================
            Services.AddScoped<IUserService, UserService>();


            // =========================
            // FILES
            // =========================
            Services.AddScoped<IFileService, FileService>();


            // =========================
            // SEMESTER
            // =========================
            Services.AddScoped<ISemesterRepository, SemesterRepository>();
            Services.AddScoped<ISemesterService, SemesterService>();
            Services.AddHostedService<SemesterBackgroundService>();


            // =========================
            // GROUP
            // =========================
            Services.AddScoped<IGroupService, GroupService>();
            Services.AddScoped<IGroupRepository, GroupRepository>();


            // =========================
            // PROJECT + STUDENT
            // =========================
            Services.AddScoped<IProjectRepository, ProjectRepository>();
            Services.AddScoped<IStudentRepository, StudentRepository>();


            // =========================
            // TASK
            // =========================
            Services.AddScoped<ITaskRepository, TaskRepository>();
            Services.AddScoped<ITaskService, TaskService>();


            // =========================
            // TASK SUBMISSION
            // =========================
            Services.AddScoped<ITaskSubmissionRepository, TaskSubmissionRepository>();
            Services.AddScoped<ITaskSubmissionService, TaskSubmissionService>();
            Services.AddScoped<ISubmissionCommentRepository, SubmissionCommentRepository>();


            // =========================
            // EVALUATION MODULE
            // =========================

            // FORM
            Services.AddScoped<IEvaluationFormRepository, EvaluationFormRepository>();
            Services.AddScoped<IEvaluationFormService, EvaluationFormService>();

            // FIELD
            Services.AddScoped<IEvaluationFieldRepository, EvaluationFieldRepository>();
            Services.AddScoped<IEvaluationFieldService, EvaluationFieldService>();

            // SUBMISSION
            Services.AddScoped<IEvaluationSubmissionRepository, EvaluationSubmissionRepository>();
            Services.AddScoped<IEvaluationSubmissionService, EvaluationSubmissionService>();
        }
    }
}
=======
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();

            Services.AddScoped<ITokenService, TokenService>();

            Services.AddScoped<IEmailSenderService, EmailSenderService>();

            Services.AddScoped<IUserService , UserService>();

            Services.AddScoped<IFileService, FileService>();

            Services.AddScoped<ISemesterRepository, SemesterRepository>();
            Services.AddScoped<ISemesterService, SemesterService>();

            Services.AddScoped<IGroupService, GroupService>();
            Services.AddScoped<IGroupRepository, GroupRepository>();

            Services.AddScoped<IProjectRepository, ProjectRepository>();
            Services.AddScoped<IStudentRepository, StudentRepository>();

            Services.AddHostedService<SemesterBackgroundService>();

            Services.AddScoped<ITaskRepository, TaskRepository>();
            Services.AddScoped<ITaskService, TaskService>();

            Services.AddScoped<ITaskSubmissionRepository, TaskSubmissionRepository>();
            Services.AddScoped<ITaskSubmissionService, TaskSubmissionService>();

            Services.AddScoped<ISubmissionCommentRepository, SubmissionCommentRepository>();

            Services.AddScoped<IThesisService, ThesisService>();
            Services.AddScoped<IThesisRepository, ThesisRepository>();

            Services.AddScoped<IThesisVersionsRepository, ThesisVersionsRepository>();
            Services.AddScoped<IThesisVersionsService, ThesisVersionsService>();

            Services.AddScoped<IThesisFeedBackRepository, ThesisFeedBackRepository>();

        }
    }
}
>>>>>>> origin/master
