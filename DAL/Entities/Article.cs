using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Article
    {
		public long Id { get; set; }
		public string Code { get; set; }
		public string ColorCode { get; set; }
		public string Description { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
