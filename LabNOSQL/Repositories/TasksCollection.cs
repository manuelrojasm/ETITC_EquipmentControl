using LabNOSQL.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LabNOSQL.Repositories
{
    public class TasksCollection : ITasksCollection
    {
        internal MongoDBContext _repository = new MongoDBContext();
        private IMongoCollection<Tasks> Collection;
        public TasksCollection()
        {
            Collection = _repository.db.GetCollection<Tasks>("ToDoList");
        }
        public async Task CreateTask(Tasks Task)
        {
            await Collection.InsertOneAsync(Task);
        }
        public async Task DeleteTask(string id)
        {
            var filter = Builders<Tasks>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }
        public async Task<List<Tasks>> GetAllTasks()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<Tasks> GetOneTask(string id)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task<List<Tasks>> GetTodayTask()
        {
            var dt = DateTime.Now.Date;
            var filter = Builders<Tasks>.Filter.Eq(s => s.StartDate.ToString("dd/MM/yyyy"), dt.ToString("dd/MM/yyyy"));
            return await Collection.Find(filter).ToListAsync();
        }
        public async Task UpdateTask(Tasks task)
        {
            var filter = Builders<Tasks>
                .Filter
                .Eq(s => s.Id, task.Id);
            await Collection.ReplaceOneAsync(filter, task);
        }
        public async Task CompleteTask(string id)
        {
            var dt = DateTime.Now.Date;
            var filter = Builders<Tasks>
                .Filter
                .Eq(s => s.Id, new ObjectId(id));
            var result =  await Collection.FindAsync(filter).Result.FirstAsync();
            result.Done = true;
            result.ExecutedDate = dt;
            await Collection.ReplaceOneAsync(filter, result);
        }


    }
}
