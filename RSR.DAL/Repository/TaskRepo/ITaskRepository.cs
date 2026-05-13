using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.TaskRepo
{
    public  interface ITaskRepository
    {
        Task<Models.TaskModel.Task> CreateTask(Models.TaskModel.Task task);
        Task<Models.TaskModel.Task> UpdateTask(Models.TaskModel.Task task);
        Task<Models.TaskModel.Task?> GetTaskById(Guid TaskId);
        Task<List<Models.TaskModel.Task>> GetTasksGroup(Guid GroupId);
        System.Threading.Tasks.Task DeleteTask(Models.TaskModel.Task task);
    }
}
