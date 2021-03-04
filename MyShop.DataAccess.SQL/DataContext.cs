using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.SQL
{
	public class DataContext : DbContext
	{
		public DataContext() : base("DefaultConnection")
		{
		}

		public DbSet<Product> products { get; set; }
		public DbSet<ProductCategory> productCategories { get; set; }
		public DbSet<Basket> baskets { get; set; }
		public DbSet<BasketItem> basketItems { get; set; }

	}
}
