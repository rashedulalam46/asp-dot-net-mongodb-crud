using System.Diagnostics;
using System.Linq;
using AspNetMongoDbCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace AspNetMongoDbCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMongoCollection<Customers> collection;
        public HomeController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("TestDb");
            this.collection = db.GetCollection<Customers>("Customers");
        }

        public IActionResult Index()
        {
            var model = collection.Find(FilterDefinition<Customers>.Empty).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customers model)
        {
            var res = collection.Find(e => e.CustomerId == model.CustomerId).FirstOrDefault();
            if (res != null)
            {
                ViewBag.Message = "Customer Id is duplicate";
                return View();
            }
            else
            {
                collection.InsertOne(model);
                ViewBag.Message = "Added successfully!";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Edit(string id)
        {
            ObjectId OldId = new ObjectId(id);
            Customers model = collection.Find(e => e.Id == OldId).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string id, Customers model)
        {
            model.Id = new ObjectId(id);
            var filter = Builders<Customers>.Filter.Eq("Id", model.Id);
            var updateDef = Builders<Customers>.Update
                .Set("FirstName", model.FirstName)
                .Set("LastName", model.LastName)
                .Set("Phone", model.Phone)
                .Set("Email", model.Email)
                .Set("Point", model.Point);

            var result = collection.UpdateOne(filter, updateDef);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            ObjectId OdId = new ObjectId(id);
            var res = collection.DeleteOne<Customers>(x => x.Id == OdId); ;

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
