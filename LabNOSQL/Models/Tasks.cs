using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LabNOSQL.Models
{
    public class Tasks
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExecutedDate { get; set; }
    }
}
