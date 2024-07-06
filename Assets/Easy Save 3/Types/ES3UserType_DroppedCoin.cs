using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("pickupSound", "Activated", "m_CancellationTokenSource", "enabled", "name")]
	public class ES3UserType_DroppedCoin : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_DroppedCoin() : base(typeof(Items.DroppedCoin)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Items.DroppedCoin)obj;
			
			writer.WritePrivateFieldByRef("pickupSound", instance);
			writer.WritePrivateField("Activated", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Items.DroppedCoin)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "pickupSound":
					instance = (Items.DroppedCoin)reader.SetPrivateField("pickupSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "Activated":
					instance = (Items.DroppedCoin)reader.SetPrivateField("Activated", reader.Read<System.Boolean>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (Items.DroppedCoin)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					case "enabled":
						instance.enabled = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DroppedCoinArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DroppedCoinArray() : base(typeof(Items.DroppedCoin[]), ES3UserType_DroppedCoin.Instance)
		{
			Instance = this;
		}
	}
}