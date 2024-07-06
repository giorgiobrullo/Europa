using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("maxHealth", "attackDamage", "defense", "healthBar", "powerBar", "damageText", "defenseText", "gameOverCamera", "statsPanel", "deathSound", "damageSound", "_health", "power", "_animation", "enabled", "name")]
	public class ES3UserType_Stats : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Stats() : base(typeof(Player.Stats)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Player.Stats)obj;
			
			writer.WritePrivateField("maxHealth", instance);
			writer.WritePrivateField("attackDamage", instance);
			writer.WritePrivateField("defense", instance);
			writer.WritePrivateFieldByRef("healthBar", instance);
			writer.WritePrivateFieldByRef("powerBar", instance);
			writer.WritePrivateFieldByRef("damageText", instance);
			writer.WritePrivateFieldByRef("defenseText", instance);
			writer.WritePrivateFieldByRef("gameOverCamera", instance);
			writer.WritePrivateFieldByRef("statsPanel", instance);
			writer.WritePrivateFieldByRef("deathSound", instance);
			writer.WritePrivateFieldByRef("damageSound", instance);
			writer.WritePrivateField("_health", instance);
			writer.WriteProperty("power", instance.power, ES3Type_int.Instance);
			writer.WritePrivateFieldByRef("_animation", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Player.Stats)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "maxHealth":
					instance = (Player.Stats)reader.SetPrivateField("maxHealth", reader.Read<System.Int32>(), instance);
					break;
					case "attackDamage":
					instance = (Player.Stats)reader.SetPrivateField("attackDamage", reader.Read<System.Int32>(), instance);
					break;
					case "defense":
					instance = (Player.Stats)reader.SetPrivateField("defense", reader.Read<System.Int32>(), instance);
					break;
					case "healthBar":
					instance = (Player.Stats)reader.SetPrivateField("healthBar", reader.Read<ProgressBarPro>(), instance);
					break;
					case "powerBar":
					instance = (Player.Stats)reader.SetPrivateField("powerBar", reader.Read<ProgressBarPro>(), instance);
					break;
					case "damageText":
					instance = (Player.Stats)reader.SetPrivateField("damageText", reader.Read<TMPro.TextMeshProUGUI>(), instance);
					break;
					case "defenseText":
					instance = (Player.Stats)reader.SetPrivateField("defenseText", reader.Read<TMPro.TextMeshProUGUI>(), instance);
					break;
					case "gameOverCamera":
					instance = (Player.Stats)reader.SetPrivateField("gameOverCamera", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "statsPanel":
					instance = (Player.Stats)reader.SetPrivateField("statsPanel", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "deathSound":
					instance = (Player.Stats)reader.SetPrivateField("deathSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "damageSound":
					instance = (Player.Stats)reader.SetPrivateField("damageSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_health":
					instance = (Player.Stats)reader.SetPrivateField("_health", reader.Read<System.Int32>(), instance);
					break;
					case "power":
						instance.power = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "_animation":
					instance = (Player.Stats)reader.SetPrivateField("_animation", reader.Read<UnityEngine.Animator>(), instance);
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


	public class ES3UserType_StatsArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_StatsArray() : base(typeof(Player.Stats[]), ES3UserType_Stats.Instance)
		{
			Instance = this;
		}
	}
}