using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
		InMemoryRepository<ProductCategory> context;
		public ProductCategoryController()
		{
			context = new InMemoryRepository<ProductCategory>();
		}

        // GET: ProductCategory
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

		#region Add Product Category
		public ActionResult Create()
		{
			ProductCategory productCategory = new ProductCategory();
			return View(productCategory);
		}

		[HttpPost]
		public ActionResult Create(ProductCategory productCategory)
		{
			if (!ModelState.IsValid)
				return View(productCategory);
			else
			{
				context.Insert(productCategory);
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion

		#region Edit Product
		public ActionResult Edit(string Id)
		{
			// Fint product by ID
			var productCategory = context.Find(Id);
			if (productCategory == null)
				return HttpNotFound();
			else
				return View(productCategory);
		}

		[HttpPost]
		public ActionResult Edit(ProductCategory productCategory , string Id)
		{
			var category = context.Find(Id);
			if (category == null)
				return HttpNotFound();
			else
			{
				// Check if product is valid or not
				if (!ModelState.IsValid)
					return View(productCategory);

				//using category = productCategory will not working
				//Asign new values to current product
				category.Name = productCategory.Name;
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion

		#region Delete Product
		public ActionResult Delete(string Id)
		{
			var productCategory = context.Find(Id);
			if (productCategory == null)
				return HttpNotFound();
			else
			{
				return View(productCategory);
			}
		}

		[HttpPost]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(ProductCategory productCategory , string Id)
		{
			var category = context.Find(Id);
			if (category == null)
				return HttpNotFound();
			else
			{
				context.Delete(category);
				context.Commit();

				return RedirectToAction("Index");
			}
		}
		#endregion
	}
}