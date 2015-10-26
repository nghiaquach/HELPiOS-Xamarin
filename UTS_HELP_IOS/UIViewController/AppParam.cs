using System;
using UIKit;

namespace HELPiOS
{
	public class AppParam
	{
		public Student student { get; set;}
		public static string CreatorId = "123456";

		private static AppParam instance;

		private AppParam() {}

		public static AppParam Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new AppParam();
				}
				return instance;
			}
		}

		public void showAlertMessage(string title, string msg){
			UIAlertView alert = new UIAlertView () { 
				Title = title, Message = msg
			};
			alert.AddButton("OK");
			alert.Show ();
		}
	}
}

