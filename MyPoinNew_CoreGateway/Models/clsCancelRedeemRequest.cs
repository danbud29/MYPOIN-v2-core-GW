using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPoinNew_CoreGateway.Models
{
	public class clsCancelRedeemRequest
	{
		/*
		 * {"token":"3af1004f8ce67df9a317d447ca2dd522e9e56bff","invoice_number":"T7RV190926101000000110"}
		 */
		public string token { get; set; }
		public string invoice_number { get; set; }

	}

	public class clsCancelRedeemResponse
	{
		/*
		 * {	"redemption":{"id":3949770,"reward":"Flexible Reward","stamps_used":3500,"extra_data":null,"status":"Canceled"},
		 *		"membership":{"tags":[],"status":100,"status_text":"Blue","stamps":3582,"balance":0,"is_blocked":false,
		 *						"referral_code":"J26XX6K3","start_date":"2019-08-03","created":"2019-08-03"}}
		 */
		public cCancelRedeemResp_redemption redemption = new cCancelRedeemResp_redemption();
		public cCancelRedeemResp_membership membership = new cCancelRedeemResp_membership();

	}

	public class cCancelRedeemResp_redemption
	{
		public int id { get; set; }
		public string reward { get; set; }
		public int stamps_used { get; set; }
		public string extra_data { get; set; }
		public string status { get; set; }


	}

	public class cCancelRedeemResp_membership
	{
		public string[] tags { get; set; }
		public int status { get; set; }
		public int stamps { get; set; }
		public int balance { get; set; }
		public string referral_code { get; set; }
		public string start_date { get; set; }
		public string created { get; set; }

	}

	public class reqCore_cancelRedeem
	{
		//{"id_bucket":1,"invoice_number":"11111111"}
		public string id_bucket { get; set; }
		public string invoice_number { get; set; }

	}


	/*
	 * {
		  "redemption_point": {
			"id": "1",
			"point_used": 50,
			"add_time": "2019-07-31 09:14:59",
			"status": "CANCEL REDEEM"
		  },
		  "membership": {
			"point": 1820,
			"is_blocked": false,
			"add_time": "2019-07-24 14:44:13"
		  }
	}*/

	public class respCore_cancelRedeem
	{
		public respCore_cancelRedeem_redemption redemption_point { get; set; }
		public respCore_cancelRedeem_membership membership { get; set; }
	}

	public class respCore_cancelRedeem_redemption
	{
		public string id { get; set; }
		public int point_used { get; set; }
		public string add_time { get; set; }
		public string status { get; set; }
	}

	public class respCore_cancelRedeem_membership
	{
		public int point { get; set; }
		public bool is_blocked { get; set; }
		public string add_time { get; set; }

	}
}
 