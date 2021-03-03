using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.SQL;
using MyShop.Core.Models;
using MyShop.Core.Contracts;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
	public class HomeController : Controller
	{
		IRepository<ProductCategory> productCategoires;
		IRepository<Product> context;

		public HomeController(IRepository<Product> context, IRepository<ProductCategory> category)
		{
			this.context = context;
			this.productCategoires = category;
		}

		public ActionResult Index(string Category = null)
		{
			List<Product> products;
			List<ProductCategory> categories = productCategoires.Collection().ToList();

			if (Category == null)
				products = context.Collection().ToList();
			else
				products = context.Collection().Where(p => p.Category == Category).ToList();

			ProductListViewModel model = new ProductListViewModel();
			model.product = products;
			model.productCategories = categories;


			return View(model);
		}

		public ActionResult Detail(string Id)
		{
			var product = context.Find(Id);
			if (product == null)
				return HttpNotFound();
			return View(product);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}