using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.SQL;
using MyShop.Core.Models;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
	public class HomeController : Controller
	{
		IRepository<ProductCategory> category;
		IRepository<Product> context;

		public HomeController(IRepository<Product> context, IRepository<ProductCategory> category)
		{
			this.context = context;
			this.category = category;
		}

		public ActionResult Index()
		{
			List<Product> products = context.Collection().ToList();
			return View(products);
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