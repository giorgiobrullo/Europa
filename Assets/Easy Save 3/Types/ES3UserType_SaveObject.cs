using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("pFlagTop", "greenFlagSprite", "respawn", "stats", "pickupSound", "Activated")]
	public class ES3UserType_SaveObject : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_SaveObject() : base(typeof(Items.SaveObject)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Items.SaveObject)obj;
			
			writer.WritePropertyByRef("pFlagTop", instance.pFlagTop);
			writer.WritePropertyByRef("greenFlagSprite", instance.greenFlagSprite);
			writer.WritePropertyByRef("respawn", instance.respawn);
			writer.WritePropertyByRef("stats", instance.stats);
			writer.WritePrivateFieldByRef("pickupSound", instance);
			writer.WritePrivateField("Activated", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Items.SaveObject)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "pFlagTop":
						instance.pFlagTop = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "greenFlagSprite":
						instance.greenFlagSprite = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "respawn":
						instance.respawn = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "stats":
						instance.stats = reader.Read<Player.Stats>();
						break;
					case "pickupSound":
					instance = (Items.SaveObject)reader.SetPrivateField("pickupSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "Activated":
					instance = (Items.SaveObject)reader.SetPrivateField("Activated", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_SaveObjectArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SaveObjectArray() : base(typeof(Items.SaveObject[]), ES3UserType_SaveObject.Instance)
		{
			Instance = this;
		}
	}
}