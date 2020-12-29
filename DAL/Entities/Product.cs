using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
		public long Id { get; set; }
		public long ArticleId { get; set; }
		public int ColorId { get; set; }
		public string Key { get; set; }
		public decimal Price { get; set; }
		public decimal DiscountPrice { get; set; }
		public string DeliveredIn { get; set; }
		public string Q1 { get; set; }
		public string Size { get; set; }
		public DateTime CreatedOn { get; set; }
		public virtual Article Article { get; set; }
		public virtual Color Color { get; set; }
	}
}
