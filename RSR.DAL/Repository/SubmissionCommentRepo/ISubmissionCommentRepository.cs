using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.SubmissionCommentRepo
{
    public interface ISubmissionCommentRepository
    {
        System.Threading.Tasks.Task CreateComment(TaskSubmissionComment comment);
        Task<TaskSubmissionComment?> GetParentComment(Guid parentcomment);
        Task<TaskSubmissionComment> GetCommentById(Guid commentId);
        System.Threading.Tasks.Task UpdateComment(TaskSubmissionComment comment);
        System.Threading.Tasks.Task DeleteComment(TaskSubmissionComment comment);
     }
}

