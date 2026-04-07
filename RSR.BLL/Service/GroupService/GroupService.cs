using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ProjectRepo;
using RSR.DAL.Repository.SemesterRepo;
using RSR.DAL.Repository.StudentRepo;


namespace RSR.BLL.Service.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ApplicationDbContext _context;

        public GroupService(IGroupRepository groupRepository , IStudentRepository studentRepository , ISemesterRepository semesterRepository , IProjectRepository projectRepository , ApplicationDbContext _context)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _semesterRepository = semesterRepository;
            _projectRepository = projectRepository;
            this._context = _context;
        }

        public async Task<BaseResponse> CreateGroup(GroupRequest request , string SupervisorId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var Students = await _studentRepository.GetStudentsByIds(request.StudentIds);
                if (Students == null || Students.Count == 0)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "No students found"
                    };
                }
                var semester = await _semesterRepository.GetActiveSemester();
                if(semester is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "No Active Semester Found"
                    };
                }
                var group = new Group
                {
                    GroupName = request.GroupName,
                    SemesterId = semester.SemesterId,
                    SupervisorId = SupervisorId
                };
                await _groupRepository.CreateGroup(group);

                var project = new Project
                {
                    ProjectIdea = request.ProjectIdea,
                    ProjectName = request.ProjectName,
                    Description = request.Description,
                    ProjectStatus = Status.InProgress,
                    GroupId = group.GroupId
                };
                if (Students.Count > 0)
                {
                    foreach (var student in Students)
                    {
                        student.GroupId = group.GroupId;
                    }
                }
                await _projectRepository.CreateProject(project);
                await transaction.CommitAsync();
                return new BaseResponse
                {
                    Success = true,
                    Message = "Group Added Succssfully"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't Added Group ",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<BaseResponse> UpdateGroup(GroupRequest request, string SupervisorId , Guid groupId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var groupWithProject = await _groupRepository.FindById(groupId); // Include
                if (groupWithProject == null)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "group not found"
                    };

                }
                if (groupWithProject.SupervisorId != SupervisorId)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "You are not allowed to update this group"
                    };
                }
                var Students = await _studentRepository.GetStudentsByIds(request.StudentIds);
                if (Students == null || Students.Count == 0)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "No students found"
                    };
                }
                var currentStudents = await _studentRepository.GetCurrentStudentByGroupId(groupWithProject.GroupId);
                foreach (var student in currentStudents)
                {
                    student.GroupId = null;
                }
                foreach (var student in Students)
                {
                    student.GroupId = groupWithProject.GroupId;
                }
                var project = groupWithProject.Project;
                if (project is null)
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = "project Not found"
                    };
                }
                project.ProjectIdea = request.ProjectIdea;
                project.ProjectName = request.ProjectName;
                project.Description = request.Description;
                groupWithProject.GroupName = request.GroupName;

                await _groupRepository.UpdateGroup(groupWithProject);
                await _projectRepository.UpdateProject(project);
                await transaction.CommitAsync();
                return new BaseResponse()
                {
                    Success = true,
                    Message = "group updated successfully"
                };
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't updated Group ",
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
