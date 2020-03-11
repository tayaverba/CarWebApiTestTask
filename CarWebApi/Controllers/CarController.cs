using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IRepository _carService;
        public CarController(IRepository carService)
        {
            _carService = carService;
        }
        // GET api/car
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll()
        {
            return Ok(_carService.GetAll());
        }

        // GET api/car/5
        [HttpGet("{id}", Name = nameof(Get))]
        public ActionResult<Car> Get(int id)
        {
            var car = _carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST api/car
        [HttpPost]
        public ActionResult<Car> Post(Car car)
        {
            _carService.Create(car);

            return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
        }

        // PUT api/car/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]JObject data)
        {
            var car = _carService.Get(id);
            if (car != null)
            {
                var carName = car.Name;
                var carDesc = car.Description;
                if (data.ContainsKey("name"))
                {
                    carName = data["name"].Type is JTokenType.Null ? null : data["name"].ToString();
                }
                if (data.ContainsKey("description"))
                {
                    carDesc = data["description"].Type is JTokenType.Null ? null : data["description"].ToString();
                }
                var updatedCar = new Car()
                {
                    Id = car.Id,
                    Name = carName,
                    Description = carDesc
                };
                _carService.Update(id, updatedCar);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return new OkObjectResult(updatedCar);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/car/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Remove(car.Id);

            return NoContent();
        }
    }
}
