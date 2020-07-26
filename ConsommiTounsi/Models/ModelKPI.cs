using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsi.Models
{
    public class ModelKPI
    {
		[JsonProperty("totalNumberOfSuppliers")]
		public int totalNumberOfSuppliers { get; set; }
		[JsonProperty("totalNumberOfCustumers")]
		public int totalNumberOfCustumers { get; set; }
		[JsonProperty("totalNumberOfOrders")]
		public int totalNumberOfOrders { get; set; }
		[JsonProperty("totalNumberOfComplaints")]
		public int totalNumberOfComplaints { get; set; }
		[JsonProperty("totalNumberOfDeliveries")]
		public int totalNumberOfDeliveries { get; set; }
		[JsonProperty("totalNumberOfEvents")]
		public int totalNumberOfEvents { get; set; }
		[JsonProperty("topSelledPoducts")]
		public List<ModelProductSales> topSelledPoducts { get; set; }
	}
}