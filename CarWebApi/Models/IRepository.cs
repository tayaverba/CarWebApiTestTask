using System.Collections.Generic;

namespace CarWebApi.Models
{
    public interface IRepository
    {
        List<Car> GetAll();

        Car Get(int id);

        Car Create(Car car);

        void Update(int id, Car car);

        void Remove(Car car);

        void Remove(int id);
    }
}