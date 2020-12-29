using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUSINESS_LOGIC.Dtos
{
    public class ImportDataViewModel
    {
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("articleCode")]
		public string ArticleCode { get; set; }

		[JsonProperty("colorCode")]
		public string ColorCode { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("price")]
		public decimal Price { get; set; }

		[JsonProperty("discountPrice")]
		public decimal DiscountPrice { get; set; }

		[JsonProperty("deliveredIn")]
		public string DeliveredIn { get; set; }

		[JsonProperty("q1")]
		public string Q1 { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }


	}
}
