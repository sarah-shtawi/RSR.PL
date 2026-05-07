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
    }
}
