using CarWebApi.Controllers;
using CarWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace CarWebApiTests
{
    public class CarControllerTest
    {
        CarController Controller { get; }
        IRepository Service { get; }
        public CarControllerTest()
        {
            Service = new CarRepositoryMock();
            Controller = new CarController(Service);
        }
        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var okResult = Controller.GetAll();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsAllItems()
        {
            var result = Controller.GetAll().Result as OkObjectResult;
            var items = Assert.IsType<List<Car>>(result.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = Controller.Get(-1);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void Get_ExistingIdPassed_ReturnsOkResult()
        {
            var testId = 1;
            var okResult = Controller.Get(testId);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_ExistingIdPassed_ReturnsRightItem()
        {
            var testId = 1;
            var result = Controller.Get(testId).Result as OkObjectResult;
            Assert.IsType<Car>(result.Value);
            Assert.Equal(testId, (result.Value as Car).Id);
        }

        [Fact]
        public void Put_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var testId = -1;
            string json = @"{
              name: 'updatedName',
              description: 'updatedDesc'
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as NotFoundResult;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_ExistingIdPassed_ReturnsOkResult()
        {
            var testId = 1;
            string json = @"{
              name: 'updatedName',
              description: 'updatedDesc'
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Put_ExistingIdPassed_FullChanged()
        {
            var testId = 1;
            var oldCar = (Controller.Get(testId)).Value;
            string json = @"{
              name: 'updatedName',
              description: 'updatedDesc'
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            var updatedCar = result.Value as Car;
            Assert.Equal("updatedName", updatedCar.Name);
            Assert.Equal("updatedDesc", updatedCar.Description);
        }
        [Fact]
        public void Put_ExistingIdPassed_NameChanged()
        {
            var testId = 1;
            var oldCarDesc = Service.Get(testId).Description;
            string json = @"{
              name: 'updatedName',
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            var updatedCar = result.Value as Car;
            Assert.Equal("updatedName", updatedCar.Name);
            Assert.Equal(oldCarDesc, updatedCar.Description);
        }

        [Fact]
        public void Put_ExistingIdPassed_DescriptionChanged()
        {
            var testId = 1;
            var oldCarName = Service.Get(testId).Name;
            string json = @"{
              description: 'updatedDesc'
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            var updatedCar = result.Value as Car;
            Assert.Equal("updatedDesc", updatedCar.Description);
            Assert.Equal(oldCarName, updatedCar.Name);
        }
        [Fact]
        public void Put_ExistingIdPassed_NameChangedToNull()
        {
            var testId = 1;
            var oldCarDesc = Service.Get(testId).Description;
            string json = @"{
              name: null
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            var updatedCar = result.Value as Car;
            Assert.Null(updatedCar.Name);
            Assert.Equal(oldCarDesc, updatedCar.Description);
        }
        [Fact]
        public void Put_ExistingIdPassed_DescriptionChangedToNull()
        {
            var testId = 1;
            var oldCarName = Service.Get(testId).Name;
            string json = @"{
              description: null
            }";

            JObject o = JObject.Parse(json);
            var result = Controller.Put(testId, o) as OkObjectResult;
            var updatedCar = result.Value as Car;
            Assert.Null(updatedCar.Description);
            Assert.Equal(oldCarName, updatedCar.Name);
        }
    }
}
