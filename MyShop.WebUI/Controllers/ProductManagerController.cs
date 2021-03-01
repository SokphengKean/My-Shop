using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
		InMemoryRepository<Product> context;
		InMemoryRepository<ProductCategory> productCategory;

		public ProductManagerController()
		{
			context = new InMemoryRepository<Product>();
			productCategory = new InMemoryRepository<ProductCategory>();
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
			ProductManagerViewModel productManagerViewModel = new ProductManagerViewModel();
			productManagerViewModel.product = new Product();
			productManagerViewModel.productCategories = productCategory.Collection();
            return View(productManagerViewModel);
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
			{
				ProductManagerViewModel productManagerViewModel = new ProductManagerViewModel();
				productManagerViewModel.product = product;
				productManagerViewModel.productCategories = productCategory.Collection();
				return View(productManagerViewModel);
			}
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