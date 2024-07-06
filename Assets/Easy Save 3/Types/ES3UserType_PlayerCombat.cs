using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_animator", "attackPoint", "enemyLayers", "attackSoundEffect", "attackCoolDown", "attackRange")]
	public class ES3UserType_PlayerCombat : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerCombat() : base(typeof(Player.PlayerCombat)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Player.PlayerCombat)obj;
			
			writer.WritePrivateFieldByRef("_animator", instance);
			writer.WritePropertyByRef("attackPoint", instance.attackPoint);
			writer.WriteProperty("enemyLayers", instance.enemyLayers, ES3Type_LayerMask.Instance);
			writer.WritePropertyByRef("attackSoundEffect", instance.attackSoundEffect);
			writer.WriteProperty("attackCoolDown", instance.attackCoolDown, ES3Type_float.Instance);
			writer.WriteProperty("attackRange", instance.attackRange, ES3Type_float.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Player.PlayerCombat)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_animator":
					instance = (Player.PlayerCombat)reader.SetPrivateField("_animator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "attackPoint":
						instance.attackPoint = reader.Read<UnityEngine.Transform>(ES3UserType_Transform.Instance);
						break;
					case "enemyLayers":
						instance.enemyLayers = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "attackSoundEffect":
						instance.attackSoundEffect = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "attackCoolDown":
						instance.attackCoolDown = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "attackRange":
						instance.attackRange = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerCombatArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerCombatArray() : base(typeof(Player.PlayerCombat[]), ES3UserType_PlayerCombat.Instance)
		{
			Instance = this;
		}
	}
}