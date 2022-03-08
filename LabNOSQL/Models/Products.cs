using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LabNOSQL.Models
{
    public class Products
    {
        [BsonId]

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public DateTime ExpirateDate { get; set; }
        public string Category { get; set; }

    }
}
