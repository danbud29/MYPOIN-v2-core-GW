using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPoinNew_CoreGateway.Models
{
	public class ClsRedeemRequest
	{
		/*
		 * {"input_method":"scanned","token":"3af1004f8ce67df9a317d447ca2dd522e9e56bff","invoice_number":"TK5Z190926102000000052",
		 * "user":"101767767463","store":"TK5Z","reward":3,"stamps":6700}
		 */
		public string token { get; set; }
		public string user { get; set; }
		public string store { get; set; }
		public int reward { get; set; }
		public string invoice_number { get; set; }
		public int stamps { get; set; }
		public string input_method { get; set; }
		public cExtraData extra_data { get; set; }
	}


	
	public class ClsRedeemResponse
	{//buat dikembalikan ke host idm / IGR
		/*
		{  "redemption":{"id":3951929,"reward":"Flexible Reward","stamps_used":5300,"extra_data":null},
		   "membership":{"tags":[],"status":100,"status_text":"Blue","stamps":49,"balance":0,"is_blocked":false,"referral_code":"Z998KKX6","start_date":"2019-09-08","created":"2019-09-08"},
		   "reward":{"id":3,"name":"Flexible Reward","stamps_to_redeem":0,"extra_data":{},"code":"","type":"reward"}}
	*/

		public cRedemption redemption = new cRedemption();
		public cMembership membership = new cMembership();
		public cReward reward = new cReward();

		
	}

	public class cMembership
	{
		public int status { get; set; }
		public string[] tags { get; set; }
		public string created { get; set; }
		public int stamps { get; set; }
		public string referral_code { get; set; }
		public int balance { get; set; }
		public string start_date { get; set; }
		
	}


	public class cReward
	{
		public string code { get; set; }
		public string name { get; set; }
		public int stamps_to_redeem { get; set; }
		public object extra_data { get; set; }
		public string type { get; set; }
		public int id { get; set; }
		
	}

	public class cRedemption
	{
		public string extra_data { get; set; }
		public string reward { get; set; }
		public int id { get; set; }
		public int stamps_used { get; set; }
	}


	public class coreReq_redeemPoint
	{
		/*
		 * {"token":"204013119","id_bucket":"f81ee9dc91ec0f6a545131fb9ed493ae3d70bb7z","id_toko":"TPTP",
		 * "point":1000,"invoice_number":"TPTP200618101000000260","input_method":"typed","addtime":"2020-06-18 15:36:57","kodecabang":"G001","kodepromo":"HOKA03HK","id_merchant":3}
		 */

		public string token;
		public string id_bucket;
		public string id_toko;
		
		public int  point;
		public string invoice_number;
		public string input_method;
		public string addtime;
		public string kodecabang;
		public string kodepromo;
		public string id_merchant;
	}

	public class coreResp_redeemPoint
	{
		/*
		 * 	/*
			{"redemption_point":{"id":"100018279757","point_used":3400,"add_time":"2020-06-18 14:45:44","status":"SUKSES"},
		"membership":{"point":0,"is_blocked":false,"add_time":"2020-06-18 14:45:44"}}*/


		public coreResp_RedeemPoint_redemption redemption_point = new coreResp_RedeemPoint_redemption();
		public coreResp_redeemPoint_membership membership = new coreResp_redeemPoint_membership();

	}

	public class coreResp_RedeemPoint_redemption
	{
		public string id;
		public int point_used;
		public string add_time;
		public string status;
	}

	public class coreResp_redeemPoint_membership
	{
		public int point;
		public bool is_blocked;
		public string add_time;
		public string referral_code;
	}
}