using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productID)
        {
            return View(_repository.Products.FirstOrDefault(p => p.ProductID == productID));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                //TODO: add that something wrong with data
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        public ActionResult Delete(int productId)
        {
            var product = _repository.DeleteProduct(productId);

            if (product != null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}
