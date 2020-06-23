using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPoinNew_CoreGateway.Models
{


	public class ClsGettingMemberDataResp_membership
	{
		public int status { get; set; }
		public int stamps { get; set; }
		public int balance { get; set; }
		public string start_date { get; set; }
		public string created { get; set; }
	}

	public class clsGettingMemberDataResp_user
	{
		public string[] member_ids { get; set; }
		public bool is_active { get; set; }
		public string phone  { get; set; }
		public bool protected_redemption { get; set; }
		public string birthday { get; set; }
		public string address  { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public string gender  { get; set; }
		public string picture_url { get; set; }
		public string email  { get; set; }
	}

	public class clsGettingMemberDataResp
	{
		/*
		 * {	"membership":{"tags":[],"status":100,"status_text":"Blue","stamps":102337,"balance":0,"is_blocked":false,
		 *				"referral_code":"82K66","start_date":"2017-10-23","created":"2017-10-23","remaining_monthly_stamps_quota":19911},
		 *		"user":{"id":"4396","name":"Ahmad Hermanto","gender":"male","address":"DSN SUKAMELANG RT 054 RW 014 DS SUKAMELANG KEC SUBANG KAB SUBANG",
		 *				"is_active":true,"email":"ahmadhermanto043@gmail.com","picture_url":null,"birthday":"1991-04-28",
		 *				"phone":"+6281334769997","protected_redemption":true,"has_incorrect_email":false,"marital_status":null,
		 *				"member_ids":["101368648826"]}}
		 * 
		 */
		public ClsGettingMemberDataResp_membership membership = new ClsGettingMemberDataResp_membership();
		public clsGettingMemberDataResp_user user = new clsGettingMemberDataResp_user();

	}


	public class coreReq_checkPoin
	{
		public string card_num;
		public string id_bucket;
		public int id_merchant;
	}

	public class coreResp_checkPoin
	{
		public cekPoin_membership membership = new cekPoin_membership();
		public cekPoin_user user = new cekPoin_user();
	}

	public class cekPoin_membership
	{
		public int point;
		public bool is_blocked;
		public string addtime;
	}
	public class cekPoin_user
	{
		public int id;
		public string name;
		public string gender;
		public string alamat;
		public string phone;
		public string email;
		public string card_num;
		public string birthday;
		public bool is_active;
		public bool protected_redemption;
		public string start_date;

	}
}
