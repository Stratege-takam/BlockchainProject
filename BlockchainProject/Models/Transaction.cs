using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainProject.Models
{
	public class Transaction
	{
		public string From { get; set; }
		public string To { get; set; }
		public double Amount { get; set; }
	}
}