using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;

namespace CarWebApi.Models
{ 
    public class CarRepository : IRepository
    {
        IGridFSBucket gridFS;   // файловое хранилище
        IMongoCollection<Car> Cars; // коллекция в базе данных
        public CarRepository(IOptions<ConnectionSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            gridFS = new GridFSBucket(database);
            Cars = database.GetCollection<Car>(settings.Value.CollectionName);
        }

        public List<Car> GetAll()
        {
            return Cars.Find(new BsonDocument()).ToList();
        }

        public Car Get(int id)
        {
            return Cars.Find<Car>(car => car.Id == id).FirstOrDefault();
        }

        public Car Create(Car car)
        {
            int id = Guid.NewGuid().GetHashCode();
            car.Id = id;
            Cars.InsertOne(car);
            return car;
        }

        public void Update(int id, Car car)
        {
            Cars.ReplaceOne(c => c.Id == id, car);
        }

        public void Remove(Car car)
        {
            Cars.DeleteOne(c => c.Id == car.Id);
        }

        public void Remove(int id)
        {
            Cars.DeleteOne(car => car.Id == id);
        }
    }
}