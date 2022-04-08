using MongoDB.Driver;

namespace LabNOSQL.Repositories
{
    public class MongoDBContext
    {
        public MongoClient client; //seria el provider el nexo con nuestra aplicación 
        public IMongoDatabase db;
        public MongoDBContext()
        {
            client = new MongoClient("mongodb+srv://krmapun:5rB1BSVS0h0xjtBV@cluster0.ioxde.mongodb.net/Cluster0?retryWrites=true&w=majority");
            db = client.GetDatabase("ClusterPartial");
        }
    }
}
