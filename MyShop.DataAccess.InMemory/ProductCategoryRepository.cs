using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using System.Runtime.Caching;

namespace MyShop.DataAccess.InMemory
{
	public class ProductCategoryRepository
	{
		private ObjectCache cache = MemoryCache.Default;
		private List<ProductCategory> productCategories;

		public ProductCategoryRepository()
		{
			productCategories = cache["productCategories"] as List<ProductCategory>;
			if (productCategories == null)
				productCategories = new List<ProductCategory>();
		}

		public void Commit()
		{
			cache["productCategories"] = productCategories;
		}

		public void Insert(ProductCategory productCategory)
		{
			productCategories.Add(productCategory);
		}

		public void Update(ProductCategory productCategory)
		{
			var cate = productCategories.Where(p => p.Id == productCategory.Id).FirstOrDefault();
			if (cate != null)
			{
				cate = productCategory;
			}
			else
				throw new Exception("Product category not found.");
		}

		public void Delete(ProductCategory productCategory)
		{
			var cate = productCategories.Where(p => p.Id == productCategory.Id).FirstOrDefault();
			if (cate != null)
				productCategories.Remove(cate);
			else
				throw new Exception("Product category not found.");
		}

		public ProductCategory Find(string Id)
		{
			var cate = productCategories.Where(p => p.Id == Id).FirstOrDefault();
			if (cate != null)
				return cate;
			else
				throw new Exception("Product category not found.");
		}

		public IQueryable<ProductCategory> Collection()
		{
			return productCategories.AsQueryable();
		}
	}
}
