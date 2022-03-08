using MongoDB.Driver;

namespace LabNOSQL.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client; //seria el provider el nexo con nuestra aplicación 
        public IMongoDatabase db;
        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://Mariana2312:Mariana2312@etitc.hcmfj.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            db = client.GetDatabase("Inventory");
        }



    }
}
