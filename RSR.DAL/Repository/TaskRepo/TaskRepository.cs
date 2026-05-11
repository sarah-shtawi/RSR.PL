using Microsoft.EntityFrameworkCore;
using RSR.DAL.Data;
using RSR.DAL.Models.TaskModel;

namespace RSR.DAL.Repository.TaskRepo
{
    public  class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task <Models.TaskModel.Task> CreateTask(Models.TaskModel.Task task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<Models.TaskModel.Task> UpdateTask(Models.TaskModel.Task task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<Models.TaskModel.Task?> GetTaskById(Guid TaskId)
        {
          var Task = await _context.Tasks
                .Include(t=>t.Supervisor).ThenInclude(s=>s.User)
                .Include(t=>t.Group).ThenInclude(g=>g.Students)
                .Include(t=>t.TaskSubmissions).ThenInclude(s=>s.TaskSubmissionComments.OrderBy(c=>c.CreatedAt)).ThenInclude(c=>c.User)
                .Include(t => t.TaskSubmissions.OrderBy(s => s.SubmittedAt)).ThenInclude(ts => ts.Student)
                .FirstOrDefaultAsync(t=>t.TaskId == TaskId);
            return Task;
        }

        public async Task<List<Models.TaskModel.Task>> GetTasksGroup(Guid GroupId)
        {
            var Tasks = await _context.Tasks
                .Include(t=>t.Supervisor).ThenInclude(s=>s.User)
                .Include(t=>t.Group).ThenInclude(g=>g.Students)
               // .Include(t=>t.TaskSubmissions.OrderBy(s=>s.SubmittedAt)).ThenInclude(ts=>ts.Student)
              //  .Include(t=>t.TaskSubmissions).ThenInclude(ts=>ts.TaskSubmissionComments.OrderBy(c=>c.CreatedAt)).ThenInclude(c=>c.User)
                .Where(t=>t.GroupId == GroupId).ToListAsync();
            return Tasks;
        }

    }
}
