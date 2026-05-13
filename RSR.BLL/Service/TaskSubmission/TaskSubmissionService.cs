using Microsoft.AspNetCore.Hosting;
using RSR.BLL.Service.Files;
using RSR.DAL.DTOs.Request.TaskReq;
using RSR.DAL.DTOs.Response;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Repository.GroupRepo;
using RSR.DAL.Repository.StudentRepo;
using RSR.DAL.Repository.SubmissionCommentRepo;
using RSR.DAL.Repository.TaskRepo;
using RSR.DAL.Repository.TaskSubmissionRepo;

namespace RSR.BLL.Service.TaskSubmission
{
    public  class TaskSubmissionService  : ITaskSubmissionService
    {
        private readonly ITaskSubmissionRepository _taskSubmissionRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _env;
        private readonly ISubmissionCommentRepository _commentRepository;

        public TaskSubmissionService(ITaskSubmissionRepository taskSubmissionRepository, ITaskRepository taskRepository 
            , IStudentRepository studentRepository , IGroupRepository groupRepository, IFileService fileService , IWebHostEnvironment env ,ISubmissionCommentRepository commentRepository)
        {
            _taskSubmissionRepository = taskSubmissionRepository;
            _taskRepository = taskRepository;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _fileService = fileService;
            _env = env;
            _commentRepository = commentRepository;
        }

        public async Task<BaseResponse> AddTaskSubmission(TaskSubmissionRequest TaskSubmission, string StudentId , Guid TaskId )
        {
            var Task = await _taskRepository.GetTaskById(TaskId);
            if (Task == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This Task is not found",
                };
            }
           

            var group = await _groupRepository.GroupByIdRepo(Task.GroupId);
            if(group is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This Group is not found",
                };
            }

            var student = await _studentRepository.GetStudentById(StudentId);
            if (student is null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Student Not Found",
                };
            }
            if (student.GroupId  != Task.GroupId) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "The student does not belong to this group",
                };
            }
            string? submissionTaskName = null;
            if (TaskSubmission.TaskSubmission is not null)
            {
                try
                {
                    var submissionFileName = await _fileService.UploadTaskFile(TaskSubmission.TaskSubmission);
                    submissionTaskName = submissionFileName;
                }
                catch (Exception ex) 
                {
                    return new BaseResponse
                    {
                        Success= false,
                        Message= ex.Message,
                    };
                }
            }
            int newVersion = 1;
            var LastSubmissiommn = await _taskSubmissionRepository.GetLastSubmission(TaskId);
            if (LastSubmissiommn != null)
            {
                LastSubmissiommn.IsLatest = false;
                await _taskSubmissionRepository.UpdateTaskSubmission(LastSubmissiommn);

                newVersion = LastSubmissiommn.VersionNumber + 1;
            }
            var Submission = new RSR.DAL.Models.TaskModel.TaskSubmission
            {
                TaskId = TaskId,
                StudentId = StudentId,
                SubmissionTaskFileURL = submissionTaskName,
                VersionNumber = newVersion,
                IsLatest = true,
                Status = SubmissionStatus.Submitted,
                GroupId = Task.GroupId,
                StudentNotes = TaskSubmission.StudentNotes,
                SubmittedAt = DateTime.UtcNow
            };
            await _taskSubmissionRepository.AddSubmission(Submission);
            return new BaseResponse
            {
                Success = true,
                Message = $"Submission Uploaded Successfully version {newVersion}"
            };

        }
        public async Task <BaseResponse> UpdateTaskSubmission(TaskSubmissionRequest Request , string StudentId , Guid SubmissionTaskId)
        {
            var Submission = await _taskSubmissionRepository.GetSubmissionById(SubmissionTaskId);
            if (Submission is null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Submission Not Found"
                };
            }
            var Student = await _studentRepository.GetStudentById(StudentId);
            if (Student == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Student not found."
                };
            }
            if ( Student.GroupId != Submission.GroupId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You can't update this submission."
                };
            }
            if (Submission.Status != SubmissionStatus.Submitted) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Only submitted submissions can be updated."
                };
            }
            if (Request.TaskSubmission?.Length > 0) 
            {
                try
                {
                    var oldFileName = Submission.SubmissionTaskFileURL;
                    var newFileName = await _fileService.UploadTaskFile(Request.TaskSubmission);
                    Submission.SubmissionTaskFileURL = newFileName;
                    if (!string.IsNullOrEmpty(oldFileName))
                    {
                        var oldFilePath = Path.Combine( _env.WebRootPath,"files","Tasks",oldFileName);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };
                }
            }
            Submission.StudentNotes = Request.StudentNotes;
            Submission.SubmittedAt = DateTime.UtcNow;

            await _taskSubmissionRepository.UpdateTaskSubmission(Submission);
            return new BaseResponse
            {
                Success = true,
                Message = "Task Submission Updated Successfully"
            };
        }
        public async Task <BaseResponse> ReviewForSubmission(Guid submissionId , string supervisorId , ReviewTaskSubmission request)
        {
            // get submission
            var submission = await _taskSubmissionRepository.GetSubmissionById(submissionId);
            if (submission == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Submission Not Found"
                };
            }
            // get task 
            var task = await _taskRepository.GetTaskById(submission.TaskId);
            if (task == null) 
            {
                return new BaseResponse
                {
                    Success = false , 
                    Message = "Task Not Found"
                };
            }
            // Auth cheack
            if(task.SupervisorId != supervisorId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You Can't Review For this submission"
                };
            }
            // can't review again 
            if (submission.Status != SubmissionStatus.Submitted)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "This submission has already been reviewed"
                };
            }
            // validate status 
            if(request.status != SubmissionStatus.Rejected &&  request.status != SubmissionStatus.Approved)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "InValied Status"
                };
            }
            // comment is not optional when status is reject 
            if(request.status == SubmissionStatus.Rejected && string.IsNullOrWhiteSpace(request.Comment))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You must provide a reason for rejection"
                };
            }
            // update submission status 
             submission.Status = request.status;
             submission.ReviewedAt = DateTime.UtcNow;
             await _taskSubmissionRepository.UpdateTaskSubmission(submission);

            // add comment  
            if (!string.IsNullOrWhiteSpace(request.Comment)) 
            {
                var comment = new TaskSubmissionComment
                {
                    Content = request.Comment,
                    CreatedAt = DateTime.UtcNow,
                    TaskSubmissionId = submission.TaskSubmissionId,
                    UserId = supervisorId,
                    ParentCommentId = null,
                };
                await _commentRepository.CreateComment(comment);
            }
            // return response 
            return new BaseResponse
            {
                Success = true ,
                Message = request.status == SubmissionStatus.Approved ? "Submission Approved Successfully" : "Submission Rejected With Feedback"
            };

        }

        public async Task<BaseResponse> ReplyToComment(string userId , Guid parentCommentId , ReplyToCommentRequest Request  )
        {
            var parent = await _commentRepository.GetParentComment(parentCommentId);
            if (parent == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Parent Comment Is Not Found"
                };
            }
            var submission = await _taskSubmissionRepository.GetSubmissionById(parent.TaskSubmissionId);
            if (submission == null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Submission Is Not Found"
                };
            }
            if(userId != submission.StudentId && userId != submission.Task.SupervisorId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You Can't reply to this comment."
                };
            }
            var reply = new TaskSubmissionComment
            {
                Content = Request.Content,
                CreatedAt = DateTime.UtcNow,
                TaskSubmissionId = submission.TaskSubmissionId,
                UserId = userId,
                ParentCommentId = parentCommentId,
            };
            await _commentRepository.CreateComment( reply );
            return new BaseResponse
            {
                Success = true,
                Message = "Comment Added Successfully"
            };
        }

        public async Task<BaseResponse> UpdateComment(Guid commentId , string userId , ReplyToCommentRequest request )
        {
            var comment = await _commentRepository.GetCommentById(commentId);
            if(comment == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "comment not found "
                };
            }
            if (userId != comment.UserId) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You are not allowed to edit this comment"
                };
            }
            if (comment.Replies != null && comment.Replies.Count > 0) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You cannot edit a comment that has replies"
                };
            }
            comment.Content = request.Content;
            await _commentRepository.UpdateComment( comment );
            return new BaseResponse
            {
                Success = true,
                Message = "Comment updated successfully"
            };
        }

        public async Task<BaseResponse> DeleteComment(Guid commentId, string userId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);
            if( comment == null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Comment Not Found"
                };
            }
            if (userId != comment.UserId) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You Can't Delete this comment"
                };
            }
            if(comment.Replies != null && comment.Replies.Count > 0)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You can't delete comment that has replies"
                };
            }
            await _commentRepository.DeleteComment(comment);
            return new BaseResponse
            {
                Success = true,
                Message = "comment deleted successfully"
            };


        }


        public async Task <BaseResponse> DeleteSubmission(Guid submissionId , string studentId)
        {
            var submission = await _taskSubmissionRepository.GetSubmissionById(submissionId);
            if (submission is null) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "submission is not found"
                };
            }
            if(submission.StudentId != studentId)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "you can't Delete this submission"
                };
            }
            if(submission.Status != SubmissionStatus.Submitted)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't Delete a reviewed submission."
                };
            }
            await _taskSubmissionRepository.DeleteSubmission(submission);
            return new BaseResponse()
            {
                Success = true , 
                Message = "Submission Removed Successfully"
            };
        }



    }
}
