using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
	public abstract class BaseEntity
	{
		public BaseEntity()
		{
			this.Id = Guid.NewGuid().ToString();
			this.CreateAt = DateTime.Now;
		}

		public string Id { get; set; }
		public DateTimeOffset CreateAt { get; set; }

	}
}
