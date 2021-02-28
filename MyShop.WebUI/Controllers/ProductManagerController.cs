using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

		public ProductManagerController()
		{
            context = new ProductRepository();
   //         Product product = new Product();
			//product.Name = "Name pro";
			//product.Description = " Description Pro";
			//product.Price = 10;
			//product.Category = "cate";
			//context.Insert(product);
			//context.Commit();
		}


		// GET: ProductManager
		public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

		#region Add Product
		public ActionResult Create()
		{
            Product product = new Product();
            return View(product);
		}

		[HttpPost]
        public ActionResult Create(Product product)
		{
			if (!ModelState.IsValid)
				return View(product);
			else
			{
				context.Insert(product);
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion

		#region Edit Product
		public ActionResult Edit(string Id)
		{
			// Fint product by ID
			var product = context.Find(Id);
			if (product == null)
				return HttpNotFound();
			else
				return View(product);
		}

		[HttpPost]
		public ActionResult Edit(Product product, string Id)
		{
			var pro = context.Find(Id);
			if (pro == null)
				return HttpNotFound();
			else
			{
				// Check if product is valid or not
				if (!ModelState.IsValid)
					return View(product);
				
				//using pro = product will not working
				//Asign new values to current product
				pro.Name = product.Name;
				pro.Description = product.Description;
				pro.Category = product.Category;
				pro.Price = product.Price;
				pro.Image = product.Image;
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion

		#region Delete Product
		public ActionResult Delete(string Id)
		{
			var product = context.Find(Id);
			if (product == null)
				return HttpNotFound();
			else
			{
				return View(product);
			}
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(Product product, string Id)
		{
			var pro = context.Find(Id);
			if (pro == null)
				return HttpNotFound();
			else
			{
				context.Delete(product);
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion
	}
}