using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
	public class ProductRepository
	{
		ObjectCache cache = MemoryCache.Default;
		List<Product> products;

		public ProductRepository()
		{
			products = cache["products"] as List<Product>;
			if (products == null)
				products = new List<Product>();
		}

		public void Commit()
		{
			cache["prodcuts"] = products;
		}

		public void Insert(Product product)
		{
			products.Add(product);
		}

		public void Update(Product product)
		{
			var pro = products.Where(p => p.Id == product.Id).FirstOrDefault();
			if (pro != null)
				pro = product;
			else
				throw new Exception("Product not found.");
		}

		public void Delete(string Id)
		{
			var product = products.Where(p => p.Id == Id).FirstOrDefault();
			if (product != null)
				products.Remove(product);
			else
				throw new Exception("Product not found.");
		}

		public Product Find(string Id)
		{
			var product = products.Where(p => p.Id == Id).FirstOrDefault();
			if (product != null)
				return product;
			else
				throw new Exception("Product not found.");
		}

		public IQueryable<Product> Collection()
		{
			return products.AsQueryable();
		}
	}
}
