using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.TaskSubmissionRepo
{
    public  class TaskSubmissionRepository : ITaskSubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskSubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task AddSubmission(TaskSubmission taskSubmission)
        {
            await _context.TaskSubmissions.AddAsync(taskSubmission);
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task <TaskSubmission?> GetLastSubmission(Guid TaskId )
        {
            var LastSubmission = await _context.TaskSubmissions.Where(ls =>ls.TaskId == TaskId  && ls.IsLatest).FirstOrDefaultAsync();
            return LastSubmission; 
        }
        public async Task<TaskSubmission> UpdateTaskSubmission(TaskSubmission taskSubmission)
        {
            _context.Update(taskSubmission);
            await _context.SaveChangesAsync();
            return taskSubmission;
        }
        public async Task<TaskSubmission?> GetSubmissionById(Guid TaskSubmissionId)
        {
            var taskSubmision = await _context.TaskSubmissions.Include(s=>s.Task).FirstOrDefaultAsync(s => s.TaskSubmissionId == TaskSubmissionId);
            return taskSubmision;
        }

        public async Task<TaskSubmission> DeleteSubmission(TaskSubmission taskSubmission)
        {
            _context.TaskSubmissions.Remove(taskSubmission);
            await _context.SaveChangesAsync();

            return taskSubmission;
        }

        public async System.Threading.Tasks.Task RemoveSubmissions( List<TaskSubmission> submissions )
        {
             _context.TaskSubmissions.RemoveRange(submissions);
            _context.SaveChanges();
        }

    }
}
