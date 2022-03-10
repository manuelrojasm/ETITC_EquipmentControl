using LabNOSQL.Models;

namespace LabNOSQL.Repositories
{
    public interface ITasksCollection
    {
        Task CreateTask(Models.Tasks task);
        Task DeleteTask(string id);
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks> GetOneTask(string id);
        Task<List<Tasks>> GetTodayTask();
        Task UpdateTask(Tasks task);
        Task CompleteTask(string id);
    }
}
