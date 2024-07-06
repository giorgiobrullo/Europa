using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("type", "pickupSound", "Activated", "enabled", "name")]
	public class ES3UserType_ScoreItem : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ScoreItem() : base(typeof(Items.ScoreItem)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Items.ScoreItem)obj;
			
			writer.WritePrivateField("type", instance);
			writer.WritePrivateFieldByRef("pickupSound", instance);
			writer.WritePrivateField("Activated", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Items.ScoreItem)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "type":
					instance = (Items.ScoreItem)reader.SetPrivateField("type", reader.Read<Other.ScoreManager.ScoreItems>(), instance);
					break;
					case "pickupSound":
					instance = (Items.ScoreItem)reader.SetPrivateField("pickupSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "Activated":
					instance = (Items.ScoreItem)reader.SetPrivateField("Activated", reader.Read<System.Boolean>(), instance);
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


	public class ES3UserType_ScoreItemArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ScoreItemArray() : base(typeof(Items.ScoreItem[]), ES3UserType_ScoreItem.Instance)
		{
			Instance = this;
		}
	}
}