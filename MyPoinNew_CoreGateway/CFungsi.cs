using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace MyPoinNew_CoreGateway
{
	public class CFungsi
	{
		public string tracelog(string msg)
		{
			string hasil = "";
			msg = msg.Replace("\'", "\'\'").Replace("\"", "\"\"");
			msg = msg.Replace("\'\'.", "\'.").Trim();
			if (msg.EndsWith("\\"))
			{
				msg = msg.Substring(0, (msg.Length - 1));
			}

			if ((msg.Length > 4000))
			{
				msg = msg.Substring(0, 4000);
			}

			while ((msg.EndsWith("\'")
						&& !msg.EndsWith("\'\'")))
			{
				msg = msg.Substring(0, (msg.Length - 1));
			}
			try
			{
				if (!System.IO.Directory.Exists("~/Tracelog/"))
				{
					System.IO.Directory.CreateDirectory("~/Tracelog/");
				}
			}
			catch (Exception ex)
			{ }
			
			try
			{
				string sFile = HttpContext.Current.Server.MapPath("~/Tracelog/TraceLog"
								+ DateTime.Now.ToString("yyMMddHH") + ".TXT");
				StreamWriter sw = new StreamWriter(sFile, true);
				sw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + (": " + msg));
				sw.Flush();
				sw.Close();
			}
			catch (Exception ex1)
			{
				hasil = ex1.Message;
			}

			// Finally
			//     MconLog.Close()
			// End Try
			return hasil;
		}

	}
}