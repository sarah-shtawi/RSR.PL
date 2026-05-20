using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.SubmissionCommentRepo
{
    public class SubmissionCommentRepository : ISubmissionCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public SubmissionCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task CreateComment(TaskSubmissionComment comment)
        {
            await _context.TaskSubmissionComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
        public async Task <TaskSubmissionComment?> GetParentComment(Guid  parentcomment)
        {
            var parentComment = await _context.TaskSubmissionComments.FindAsync(parentcomment);
            return parentComment;
        }
        public async Task <TaskSubmissionComment> GetCommentById(Guid commentId)
        {
            var comment = await _context.TaskSubmissionComments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c=>c.TaskSubmissionCommentId == commentId);
            return comment;
        }
        public async System.Threading.Tasks.Task UpdateComment(TaskSubmissionComment comment)
        {
            _context.TaskSubmissionComments.Update(comment);
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task DeleteComment(TaskSubmissionComment comment)
        {
           _context.TaskSubmissionComments.Remove(comment);
            await _context.SaveChangesAsync();
        }


        public async System.Threading.Tasks.Task RemoveComments(List<TaskSubmissionComment> comments)
        {
             _context.TaskSubmissionComments.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }
    }
}
