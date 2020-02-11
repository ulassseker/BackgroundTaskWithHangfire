using BackgroundTaskWithHangfire.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BackgroundTaskWithHangfire.Services
{
    public class InformationMongoService
    {
        private readonly IMongoCollection<InformationMongo> _information;

        public InformationMongoService(IInformationMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _information = database.GetCollection<InformationMongo>(settings.InformationCollectionName);
        }

        public List<InformationMongo> Get() =>
            _information.Find(information => true).ToList();

        public InformationMongo Get(string id) =>
            _information.Find<InformationMongo>(information => information.Id == id).FirstOrDefault();

        public InformationMongo Create(InformationMongo information)
        {
            _information.InsertOne(information);
            return information;
        }

        public void Update(string id, InformationMongo informationIn) =>
            _information.ReplaceOne(information => information.Id == id, informationIn);

        public void Remove(InformationMongo informationIn) =>
            _information.DeleteOne(information => information.Id == informationIn.Id);

        public void Remove(string id) =>
            _information.DeleteOne(information => information.Id == id);
    }
}