using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPoinNew_CoreGateway.Models;
using Newtonsoft.Json;


namespace MyPoinNew_CoreGateway.Controllers
{
	public class ValuesController : ApiController
	{
		[AllowAnonymous]
		[HttpGet]
		[Route("api/idm/memberships/details")]
		public IHttpActionResult gettingMemberData(string token, string user, string merchant)
		{//token=1&user=191347928341&merchant=1

			CFungsi fungsi = new CFungsi();

			fungsi.tracelog("GETTING MEMBER DATA MASUK : " + token + "//" + user + "//" + merchant);

			coreReq_checkPoin coreReq_cekpoin = new coreReq_checkPoin();
			coreReq_cekpoin.card_num = user;
			coreReq_cekpoin.id_merchant = int.Parse(merchant);
			coreReq_cekpoin.id_bucket = int.Parse(token);

			string jsonReq = JsonConvert.SerializeObject(coreReq_cekpoin);

			MyPoinCore.service2 service = new MyPoinCore.service2();
			fungsi.tracelog("GETTING MEMBER DATA REQUEST KE CORE " + service.Url + " " + jsonReq);
			string[] responseCore = service.CheckPoin(jsonReq);
			fungsi.tracelog("GETTING MEMBER DATA RESPONSE DARI CORE : " + responseCore[0] + "//" + responseCore[1] + "//" + responseCore[2]);

			if (responseCore[0] == "0")
			{
				//response core dimasukin ke class
				coreResp_checkPoin coreResp = JsonConvert.DeserializeObject<coreResp_checkPoin>(responseCore[1]);

				clsGettingMemberDataResp response = new clsGettingMemberDataResp();
				response.membership.stamps = coreResp.membership.point;
				response.user.name = coreResp.user.name;
				response.user.phone = coreResp.user.phone;
				response.user.id = coreResp.user.id.ToString();
				response.user.email = coreResp.user.email;
				response.user.address = coreResp.user.alamat;
				response.user.birthday = coreResp.user.birthday;
				response.user.gender = coreResp.user.gender;
				response.user.protected_redemption = coreResp.user.protected_redemption;
				response.user.is_active = coreResp.user.is_active;



				string JsonKeluar = JsonConvert.SerializeObject(response);
				fungsi.tracelog("GETTING MEMBER DATA RETURN  : " + JsonKeluar);


				return Json(response);
			}
			else
			{
				fungsi.tracelog("GETTING MEMBER DATA RETURN  : " + responseCore[1]);

				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, responseCore[1]));
			}



		}


		[AllowAnonymous]
		[HttpPost]
		[Route("api/idm/transactions/add")]
		public IHttpActionResult transactionAdd([FromBody]ClsRequestTransaction body)
		{
			CFungsi fungsi = new CFungsi();
			/*
			 * {"token":"d0bdc5221da9ba6305a1e2c685c2f8bb0444d3db","created":"2019-09-26T15:11:11+07","user":"101476539298",
			 * "store":"TB77","number_of_people":1,"invoice_number":"NTDL00UT10068701TB77190926104000000015",
			 * "stamps":3,"total_value":60000.0,
			 * "items":[{"product_name":"20072105","quantity":5,"price":20000.0}],"extra_data":[{"promo":"NTDL00UT"}]}
			 * */
			ClsResponseTransaction response = new ClsResponseTransaction();//response ke merchant
			string bodyJson = JsonConvert.SerializeObject(body);
			fungsi.tracelog("TRANSACTION ADD MASUK : " + bodyJson);



			List<ReqCore_earning> earningCoreList = new List<ReqCore_earning>();//request ke core

			// isi data earningCore
			ReqCore_earning earning = new ReqCore_earning();
			earning.add_time = DateTime.Now;
			earning.card_num = body.user;
			earning.invoice_number = body.invoice_number;
			earning.id_bucket = body.token; //nanti di host IDM, token diisi IDBUCKET + ID MERCHANT
			earning.id_merchant = body.token;//nanti di host idm, token diisi ID MERCHANT JUGA		
			earning.id_toko = body.store;
			earning.kode_cabang = body.store;
			earning.kode_promo = body.extra_data[0].promo;
			earning.point = body.stamps;
			earning.total_value = (int)body.total_value;

			for (int i = 0; i < body.items.Length; i++)
			{
				cItem item = new cItem();
				item.price = body.items[i].price;
				item.product_name = body.items[i].product_name;
				item.quantity = body.items[i].quantity;
				earning.items.Add(item);
			}

			earningCoreList.Add(earning);
			string jsonRequest = JsonConvert.SerializeObject(earningCoreList);

			MyPoinCore.service2 service = new MyPoinCore.service2();
			fungsi.tracelog("TRANSACTION ADD REQUEST CORE " + service.Url + " --> " + jsonRequest);
			string[] JsonResponse = service.earning_point(jsonRequest);
			fungsi.tracelog("TRANSACTION ADD RESPONSE CORE : " + JsonResponse[0] + "//" + JsonResponse[1] + "//" + JsonResponse[2]);

			if (JsonResponse[0] == "0")//balikan dari core sukses
			{
				//response dari core
				List<RespCore_earning> RespEarningCore = new List<RespCore_earning>();
				RespEarningCore = JsonConvert.DeserializeObject<List<RespCore_earning>>(JsonResponse[1]);
				response.customer.balance = RespEarningCore[0].customer.balance;
				response.customer.id = 1;// RespEarningCore[0].customer.card_num;
				response.customer.mobile_phone = RespEarningCore[0].customer.mobile_phone;
				if (RespEarningCore[0].customer.stamps_remaining == 0)
					response.customer.stamps_remaining = RespEarningCore[0].customer.point_remaining;
				else
					response.customer.stamps_remaining = RespEarningCore[0].customer.stamps_remaining;

				response.customer.status = RespEarningCore[0].customer.status.ToString();

				response.transaction.id = 1;//RespEarningCore[0].transaction.invoice_number;

				if (RespEarningCore[0].transaction.stamps_earned == 0)
					response.transaction.stamps_earned = RespEarningCore[0].transaction.point_earned;
				else
					response.transaction.stamps_earned = RespEarningCore[0].transaction.stamps_earned;
				response.transaction.value = RespEarningCore[0].transaction.total_value;


				string returnResponse = JsonConvert.SerializeObject(response);
				fungsi.tracelog("TRANSACTION ADD RETURN " + returnResponse);
				return Json(response);

			}
			else
			{
				fungsi.tracelog("TRANSACTION ADD RETURN " + JsonResponse[1]);

				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, JsonResponse[1]));
				//kalo balikan dari core error
			}

			
		}



		[AllowAnonymous]
		[HttpPost]
		[Route("api/idm/redemptions/redeem-reward")]
		public IHttpActionResult RedeemFlexible([FromBody] ClsRedeemRequest body)
		{
			/*
		 * {"input_method":"scanned","token":"3af1004f8ce67df9a317d447ca2dd522e9e56bff","invoice_number":"TK5Z190926102000000052",
		 * "user":"101767767463","store":"TK5Z","reward":3,"stamps":6700}
		 */
			string jsonMasuk = JsonConvert.SerializeObject(body);
			CFungsi fungsi = new CFungsi();
			fungsi.tracelog("REDEEM FLEXIBLE MASUK : " + jsonMasuk);

			coreReq_redeemPoint coreReq_redeem = new coreReq_redeemPoint();
			coreReq_redeem.addtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			coreReq_redeem.token = body.user;
			coreReq_redeem.id_bucket = body.token;
			coreReq_redeem.id_merchant = body.reward.ToString();

			coreReq_redeem.id_toko = body.store;	
			coreReq_redeem.invoice_number = body.invoice_number;
			coreReq_redeem.kodecabang = body.store;
			coreReq_redeem.input_method = body.input_method;
			try
			{
				coreReq_redeem.kodepromo = body.extra_data.promo;
			}
			catch (Exception ex)
			{ }
			coreReq_redeem.point = body.stamps;
	

			string JsonCoreReq = JsonConvert.SerializeObject(coreReq_redeem);
			MyPoinCore.service2 service = new MyPoinCore.service2();
			fungsi.tracelog("REDEEM POINT REQUEST CORE : " + service.Url + " " + JsonCoreReq);
			string[] respCore = service.redeem_point(JsonCoreReq);
			fungsi.tracelog("REDEEM POINT RESPONSE CORE : " + respCore[0] + "//" + respCore[1] + "//" + respCore[2]);


			//0
			//{""redemption_point"":{""id"":""100018279757"",""point_used"":1000,""add_time"":""2020-06-18 15:36:57"",""status"":""SUKSES""},
			//""membership"":{""point"":0,""is_blocked"":false,""add_time"":""2020-06-18 15:36:57""}}
			//TPTP200618101000000260


			if (respCore[0] == "0")
			{
				coreResp_redeemPoint cRespCore_redeem = JsonConvert.DeserializeObject<coreResp_redeemPoint>(respCore[1]);
				//{"redemption_point":{"id":"100018279757","point_used":3400,"add_time":"2020-06-18 14:45:44","status":"SUKSES"},"membership":{"point":0,"is_blocked":false,"add_time":"2020-06-18 14:45:44"}}

				ClsRedeemResponse response = new ClsRedeemResponse();

				response.membership.stamps = cRespCore_redeem.membership.point;
				response.membership.start_date = cRespCore_redeem.membership.add_time;

				response.redemption.stamps_used = cRespCore_redeem.redemption_point.point_used;
				response.redemption.reward = cRespCore_redeem.redemption_point.id.ToString();

				fungsi.tracelog("REDEEM FLEXIBLE RETURN : " + JsonConvert.SerializeObject(response));

				return Json(response);
				/*
					{  "redemption":{"id":3951929,"reward":"Flexible Reward","stamps_used":5300,"extra_data":null},
					   "membership":{"tags":[],"status":100,"status_text":"Blue","stamps":49,"balance":0,"is_blocked":false,"referral_code":"Z998KKX6","start_date":"2019-09-08","created":"2019-09-08"},
					   "reward":{"id":3,"name":"Flexible Reward","stamps_to_redeem":0,"extra_data":{},"code":"","type":"reward"}	}
				*/
			}
			else
			{
				fungsi.tracelog("REDEEM FLEXIBLE RETURN  : " + respCore[1]);

				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, respCore[1]));
			}

		}


		[AllowAnonymous]
		[HttpPost]
		[Route("api/idm/redemptions/cancel")]
		public IHttpActionResult cancelRedeem([FromBody]clsCancelRedeemRequest body)
		{
			string jsonMasuk = JsonConvert.SerializeObject(body);
			CFungsi fungsi = new CFungsi();
			fungsi.tracelog("CANCEL REDEEM MASUK : " + jsonMasuk);


			reqCore_cancelRedeem reqCore = new reqCore_cancelRedeem();
			reqCore.invoice_number = body.invoice_number;
			reqCore.id_bucket = body.token;

			string JsonCoreReq = JsonConvert.SerializeObject(reqCore);
			MyPoinCore.service2 service = new MyPoinCore.service2();
			fungsi.tracelog("CANCEL REDEEM POINT REQUEST CORE : " + service.Url + " " + JsonCoreReq);
			string[] respCore = service.cancel_redeem_point(JsonCoreReq);
			fungsi.tracelog("CANCEL REDEEM POINT RESPONSE CORE : " + respCore[0] + "//" + respCore[1] + "//" + respCore[2]);


			if (respCore[0] == "0")
			{
				respCore_cancelRedeem cRespCore = JsonConvert.DeserializeObject<respCore_cancelRedeem>(respCore[1]);


				clsCancelRedeemResponse response = new clsCancelRedeemResponse();

				//crespCore dimasukin ke response
				response.membership.balance = 0;
				response.membership.created = "";
				response.membership.referral_code = "";
				response.membership.stamps = cRespCore.membership.point;
				response.membership.start_date = "";
				response.membership.status = 1;
				//response.membership.tags

				//response.redemption.extra_data

				response.redemption.id =  int.Parse(cRespCore.redemption_point.id);
				//response.redemption.reward 
				response.redemption.stamps_used = cRespCore.redemption_point.point_used;
				response.redemption.status = cRespCore.redemption_point.status;


				return Json(response);
			}
			else
			{
				fungsi.tracelog("CANCEL REDEEM RETURN " + respCore[1]);

				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, respCore[1]));
			}


			
		}

		////belum ada di mypoin core bikin an supri
		//[AllowAnonymous]
		//[HttpPost]
		//[Route("api/idm/stamps-promotion/add")]//corporate promo
		//public IHttpActionResult CorporatePromo([FromBody]cCorporatePromo body)
		//{
		//	string jsonMasuk = JsonConvert.SerializeObject(body);
		//	CFungsi fungsi = new CFungsi();
		//	fungsi.tracelog("CORPORATE PROMO MASUK : " + jsonMasuk);



		//	cCorporatePromoResult response = new cCorporatePromoResult();
		//	return Json(response);
		//}


		

		
	}
}