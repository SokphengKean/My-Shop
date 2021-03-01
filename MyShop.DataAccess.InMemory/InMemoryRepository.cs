using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
	public class InMemoryRepository<T> where T : BaseEntity
	{
		ObjectCache cache = MemoryCache.Default;
		List<T> items;
		string className;
		public InMemoryRepository()
		{
			className = typeof(T).Name;
			items = cache[className] as List<T>;
			if (items == null)
				items = new List<T>();
		}

		public void Commit()
		{
			cache[className] = items;
		}

		public void Insert(T t)
		{
			items.Add(t);
		}

		public void Update(T t)
		{
			var tUpdate = items.First(i => i.Id == t.Id);
			if (tUpdate != null)
				tUpdate = t;
			else
				throw new Exception(className + " not found.");
		}

		public void Delete(T t)
		{
			var tDelete = items.First(i => i.Id == t.Id);
			if (tDelete != null)
				items.Remove(tDelete);
			else
				throw new Exception(className + " not found.");
		}

		public T Find(string Id)
		{
			var tFind = items.First(i => i.Id == Id);
			if (tFind != null)
				return tFind;
			else
				throw new Exception(className + " not found.");
		}

		public IQueryable<T> Collection()
		{
			return items.AsQueryable();
		}
	}
}
