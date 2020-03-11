using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWebApi.Models
{
    public class CarRepositoryMock : IRepository
    {
        List<Car> cars = new List<Car>() {
                new Car{Id = 1, Name = "Audi"},
                new Car{Id = 2, Name = "BMW", Description = "good car"},
                new Car{Id = 3, Name = "Lada"},
            };
        public Car Create(Car car)
        {
            int id = Guid.NewGuid().GetHashCode();
            car.Id = id;
            cars.Add(car);
            return car;
        }

        public Car Get(int id)
        {
            return cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Car> GetAll()
        {
            return cars;
        }

        public void Remove(Car car)
        {
            cars.Remove(car);
        }

        public void Remove(int id)
        {
            cars.Remove(Get(id));
        }

        public void Update(int id, Car car)
        {
            Remove(id);
            cars.Add(car);
        }
    }
}
