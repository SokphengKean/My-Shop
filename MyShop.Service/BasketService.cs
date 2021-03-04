using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Service
{
	public class BasketService
	{
		IRepository<Basket> basketContext;
		IRepository<Product> productContext;

		public const string BasketSessionName = "eCommerceBasket";

		public BasketService(IRepository<Basket> basketContext , IRepository<Product> productContext)
		{
			this.basketContext = basketContext;
			this.productContext = productContext;
		}

		private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
		{
			HttpCookie httpCookie = httpContext.Request.Cookies.Get(BasketSessionName);

			Basket basket = new Basket();

			if (httpCookie != null)
			{
				string basketId = httpCookie.Value;
				if (!string.IsNullOrEmpty(basketId))
				{
					basket = basketContext.Find(basketId);
				}
			}
			else
			{
				if (createIfNull)
					basket = CreateNetBasket(httpContext);
			}

			return basket;
		}

		private Basket CreateNetBasket(HttpContextBase httpContext)
		{
			Basket basket = new Basket();

			basketContext.Insert(basket);
			basketContext.Commit();

			HttpCookie httpCookie = new HttpCookie(BasketSessionName);
			httpCookie.Value = basket.Id;
			httpCookie.Expires = DateTime.Now.AddDays(1);
			httpContext.Response.Cookies.Add(httpCookie);

			return basket;
		}

		public void AddToBasket(HttpContextBase httpContext, string productId)
		{
			Basket basket = GetBasket(httpContext , true);
			BasketItem basketItem = basket.BasketItems.FirstOrDefault(b => b.Id == productId);

			if(basketItem == null)
			{
				basketItem = new BasketItem()
				{
					BasketId = basket.Id ,
					ProductId = productId ,
					Quantity = 1
				};
				basket.BasketItems.Add(basketItem);
			}
			else
			{
				basketItem.Quantity += 1;
			}

			basketContext.Commit();
		}

		public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
		{
			Basket basket = GetBasket(httpContext , true);
			BasketItem basketItem = basket.BasketItems.FirstOrDefault(b => b.Id == itemId);

			if(basketItem != null)
			{
				basket.BasketItems.Remove(basketItem);
				basketContext.Commit();
			}
		}
	}
}
