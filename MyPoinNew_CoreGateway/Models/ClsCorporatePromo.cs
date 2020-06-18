using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPoinNew_CoreGateway.Models
{

	public class cCorporatePromo
	{
		public string token { get; set; }
		public int promotion_id { get; set; }
		public string user { get; set; }
		public int stamps { get; set; }
	}

	public class cAward
	{
		public int id { get; set; }
		public int stamps_earned { get; set; }
	}

	public class cCustomer
	{
		public int id { get; set; }
		public string mobile_phone { get; set; }
		public int stamps_remaining { get; set; }
		public string status { get; set; }
	}



	public class cCorporatePromoResult
	{
		public cAward award = new cAward();
		public cCustomer customer = new cCustomer();
	}

}