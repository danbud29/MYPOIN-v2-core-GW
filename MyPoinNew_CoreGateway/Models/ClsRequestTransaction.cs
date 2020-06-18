using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPoinNew_CoreGateway.Models
{
	public class cItem
	{
		public string product_name { get; set; }
		public double quantity { get; set; }
		public double price { get; set; }
	}

	public class cExtraData
	{
		public string promo { get; set; }
	}
	public class ClsRequestTransaction
	{
		/*
		 * {"token":"d0bdc5221da9ba6305a1e2c685c2f8bb0444d3db","created":"2019-09-26T15:11:11+07","user":"101476539298",
		 * "store":"TB77","number_of_people":1,"invoice_number":"NTDL00UT10068701TB77190926104000000015",
		 * "stamps":3,"total_value":60000.0,
		 * "items":[{"product_name":"20072105","quantity":5,"price":20000.0}],"extra_data":[{"promo":"NTDL00UT"}]}
		 * */
		public string token { get; set; }
		public string created { get; set; }
		public string user { get; set; }
		public string store { get; set; }
		public int number_of_people { get; set; }
		public string invoice_number { get; set; }
		public int stamps { get; set; }
		public double total_value { get; set; }
		public cItem[] items { get; set; }
		public cExtraData[] extra_data { get; set; }
	}

	public class ClsResponseTransactionCustomers
	{
		public string status { get; set; }
		public int balance { get; set; }
		public string mobile_phone { get; set; }
		public int id { get; set; }
		public int stamps_remaining { get; set; }
	}

	public class ClsResponseTransactionTransaction
	{
		public int stamps_earned { get; set; }
		public int id { get; set; }
		public int value { get; set; }
		public int number_of_people { get; set; }

	}


	public class ClsResponseTransaction
	{
		/*
		 * {	"transaction":{"id":95823965,"value":60000.0,"stamps_earned":3,"number_of_people":1},
		 *		"customer":{"id":5950648,"mobile_phone":"+6288218640889","stamps_remaining":12,"status":"Blue","balance":0}}
		 * */

		public ClsResponseTransactionCustomers customer = new ClsResponseTransactionCustomers();
		public ClsResponseTransactionTransaction transaction = new ClsResponseTransactionTransaction();
	}


	public class ReqCore_earning
	{
		public string id_bucket = "";
		public string id_toko= "";
		public string kode_cabang ="";
		public string id_merchant = "";
		public string card_num = "";
		public DateTime add_time;
		public string invoice_number = "";
		public string kode_promo = "";
		public int point;
		public int total_value;
		public string transaction_with1 = "";
		public string transaction_with2 = "";
		public string transaction_with3 = "";
		public int value_transaction1;
		public int value_transaction2;
		public int value_transaction3;
		public List<cItem> items = new List<cItem>();

	}

	public class ClsItem
	{
		public string product_name = "";
		public int qty;
		public int price;
		public string plu= "";
	}




	/* RESPONSE DARI CORE - TRANSAKSI EARNING
	 * 
	 * 
	 * [
  {
    "customer": {
      "status": true,
      "balance": 0,
      "mobile_phone": "+62890654321",
      "card_num": "100231231",
      "point_remaining": 2120
    },
    "transaction": {
      "point_earned": 60,
      "invoice_number": "123123123123",
      "total_value": 12000
    }
  },
  {
    "customer": {
      "status": true,
      "balance": 0,
      "mobile_phone": "+62890654321",
      "card_num": "100231231",
      "stamps_remaining": 74
    },
    "transaction": {
      "stamps_earned": 2,
      "invoice_number": "12312012311",
      "total_value": 6000
    }
  }
]

	 * */
	public class RespCore_earning
	{
		public RespCore_earning_customer customer = new RespCore_earning_customer();
		public RespCore_earning_transaction transaction = new RespCore_earning_transaction();
	}

	public class RespCore_earning_customer
	{
		public Boolean status;
		public int balance;
		public string mobile_phone;
		public string card_num;
		public int point_remaining;
		public int stamps_remaining;
	}

	public class RespCore_earning_transaction
	{
		public int point_earned;
		public int stamps_earned;
		public string invoice_number;
		public int total_value;
	}
}