using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using RSR.BLL.Service.Files;
using RSR.DAL.DTOs.Request.TaskReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.DTOs.Response.TaskRes;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.TaskRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.BLL.Service.Task
{
    public  class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IFileService _fileService;
        private readonly IGroupRepository _groupRepository;
        private readonly IWebHostEnvironment _env;

        public TaskService(ITaskRepository taskRepository , IFileService fileService , IGroupRepository groupRepository , IWebHostEnvironment env)
        {
            _taskRepository = taskRepository;
            _fileService = fileService;
            _groupRepository = groupRepository;
            _env = env;
        }
        public async Task <BaseResponse> CreateTask(string SupervisorId , Guid GroupId, TaskRequest Request)
        {
            if(Request.DeadLine <= DateTime.UtcNow)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Deadline must be in the future"
                };
            }
            var group = await _groupRepository.GroupByIdRepo(GroupId);
            if(group == null)
            {
                return new BaseResponse()
                {
                    Success= false,
                    Message = "Group Not Found"
                };
            }
            if ( group.SupervisorId != SupervisorId)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "This Group does not belong to this supervisor"
                };
            }


            var TaskDB = new RSR.DAL.Models.TaskModel.Task()
            {
                Title = Request.Title,
                Description = Request.Description,
                DeadLine = Request.DeadLine,
                SupervisorId = SupervisorId,
                SupervisorNotes = Request.SupervisorNotes,
                GroupId = GroupId,
                CreatedAt = DateTime.UtcNow,
            };
            if(Request.TaskFileURL != null)
            {
                try
                {
                    var TaskFileName = await _fileService.UploadTaskFile(Request.TaskFileURL);
                    TaskDB.TaskFileURL = TaskFileName;
                }
                catch (Exception ex) 
                {
                    return new BaseResponse()
                    {
                        Success = false,
                        Message = ex.Message
                    };
                }
           
            }
            await _taskRepository.CreateTask(TaskDB);
            return new BaseResponse()
            {
                Success= true,
                Message = "Task Added To Group Successfully"
            };
        }
        public async Task<BaseResponse> UpdateTask(string SupervisorId, Guid GroupId, TaskRequest Request , Guid TaskId)
        {
            var TaskDB = await _taskRepository.GetTaskById(TaskId);
            if (TaskDB == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Task not found"
                };
            }
            if (Request.DeadLine <= DateTime.UtcNow)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Deadline must be in the future"
                };
            }
            var group = await _groupRepository.GroupByIdRepo(GroupId);
            if (group == null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Group Not Found"
                };
            }
            if (group.SupervisorId != SupervisorId)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "This Group does not updated to this supervisor"
                };
            }


            TaskDB.Title = Request.Title;
            TaskDB.Description = Request.Description;
            TaskDB.DeadLine = Request.DeadLine;
            TaskDB.SupervisorNotes = Request.SupervisorNotes;

            if (Request.TaskFileURL?.Length > 0)
            {
                try
                {
                    var oldFileName = TaskDB.TaskFileURL;
                    var newFileName = await _fileService.UploadTaskFile(Request.TaskFileURL);
                    TaskDB.TaskFileURL = newFileName;

                    if (!string.IsNullOrEmpty(oldFileName))
                    {
                        var oldFilePath = Path.Combine(_env.WebRootPath, "files", "Tasks", oldFileName);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    return new BaseResponse()
                    {
                        Success = true,
                        Message = ex.Message
                    };
                }
            }
            await _taskRepository.UpdateTask(TaskDB);
            return new BaseResponse()
            {
                Success = true,
                Message = "Task Updated Successfully"
            };
        }

        public async Task<List<TaskResponse>> GetTasksByGroup(Guid GroupId , string supervisorId)
        {
            var Tasks = await _taskRepository.GetTasksGroup(GroupId , supervisorId);
            var TasksResponse = Tasks.Adapt<List<TaskResponse>>();
            return TasksResponse;
        }



    }
}
