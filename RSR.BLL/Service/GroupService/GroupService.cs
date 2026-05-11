using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.DTOs.Request.GroupRequest;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.GroupRes;
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.User;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.ProjectRepo;
using RSR.DAL.Repository.SemesterRepo;
using RSR.DAL.Repository.StudentRepo;
using System.Collections.Generic;


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

        public async Task<List<GetAllSupervisorsWithGroups>> GetCoordinatersGroups()
        {
            var supervisorsWithGroup = await _groupRepository.GetAllSupervisorsWithGroups();
            var AllGroups = supervisorsWithGroup.Select(s =>
            new GetAllSupervisorsWithGroups
            {
                SupervisorName = s.User.FullName,
                Groups = s.Groups.Select(g => new GroupResponse
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    ProjectName = g.Project.ProjectName,
                    ProjectIdea = g.Project.ProjectIdea,
                    ProjectStatus = g.Project.ProjectStatus,
                    Description = g.Project.Description,
                    Students = g.Students.Select(s => new StudentResponse
                    {
                        StudentNumber = s.StudentNumber,
                        FullName = s.User.FullName,
                        GroupId = s.GroupId ?? Guid.Empty
                    }).ToList()
                }).ToList()
            }).ToList();
            return AllGroups;
        }
        public async Task<List<GroupResponse>> GetSupervisorGroups(string supervisorId)
        {
            var Groups = await _groupRepository.GetSupervisorGroup(supervisorId);
            var supervisorGroups = Groups.Select(g =>
                new GroupResponse
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    ProjectName = g.Project.ProjectName,
                    ProjectIdea = g.Project.ProjectIdea,
                    ProjectStatus = g.Project.ProjectStatus,
                    Description = g.Project.Description,
                    Students = g.Students.Select(s => new StudentResponse
                    {
                        StudentNumber = s.StudentNumber,
                        FullName = s.User.FullName,
                        GroupId = s.GroupId ?? Guid.Empty
                    }).ToList()
                }).ToList();
            return supervisorGroups;
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
                foreach(var student in Students)
                {
                    if (student.GroupId is not null)
                    {
                        return new BaseResponse
                        {
                            Success = false,
                            Message = "Student already assigned"
                        };
                    }
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
                var groupWithProject = await _groupRepository.GroupByIdRepo(groupId); // Include
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
                foreach (var student in Students)
                {
                    if (student.GroupId is not null && student.GroupId != groupWithProject.GroupId)
                    {
                        return new BaseResponse
                        {
                            Success = false,
                            Message = "Student already assigned"
                        };
                    }
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

        public async Task<GroupResponse> GetGroupById(Guid groupId , string userId , string role)
        {
            var group = await _groupRepository.GroupByIdRepo(groupId);
            if (group == null)
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "group not found"
                };
            }

            if (group.Project == null)
            {
                return new GroupResponse
                {
                    Success = false,
                    Message = "Project not found"
                };
            }
            if(role == "Student")
            {
                bool isMember = group.Students.Any(s => s.UserId == userId);

                if (!isMember)
                    return new GroupResponse
                    {
                        Success = false,
                        Message = "You are not a member of this group"
                    };
            }
            if (role == "Supervisor")
            {
                if (group.SupervisorId != userId)
                    return new GroupResponse
                    {
                        Success = false,
                        Message = "You are not the supervisor of this group"
                    };
            }

            var GroupResponse = new GroupResponse
            {
                Success = true,
                Message = "success",
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                ProjectIdea = group.Project.ProjectIdea,
                ProjectName = group.Project.ProjectName,
                ProjectStatus = group.Project.ProjectStatus,
                Description = group.Project.Description,
                Students = group.Students.Select(s => new StudentResponse
                {
                    StudentNumber = s.StudentNumber,
                    FullName = s.User.FullName,
                   GroupId = s.GroupId ?? Guid.Empty
                }).ToList()
            };
            return GroupResponse;
        }


    }
}
