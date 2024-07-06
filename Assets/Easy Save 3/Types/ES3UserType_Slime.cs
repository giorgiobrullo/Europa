using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("gravityScale", "hurtSound", "idleSound", "IsFacingRight", "isDead")]
	public class ES3UserType_Slime : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Slime() : base(typeof(Enemies.Slime)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Enemies.Slime)obj;
			
			writer.WritePrivateField("gravityScale", instance);
			writer.WritePropertyByRef("hurtSound", instance.hurtSound);
			writer.WritePropertyByRef("idleSound", instance.idleSound);
			writer.WriteProperty("IsFacingRight", instance.IsFacingRight, ES3Type_bool.Instance);
			writer.WriteProperty("isDead", instance.isDead, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Enemies.Slime)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "gravityScale":
					instance = (Enemies.Slime)reader.SetPrivateField("gravityScale", reader.Read<System.Single>(), instance);
					break;
					case "hurtSound":
						instance.hurtSound = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "idleSound":
						instance.idleSound = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "IsFacingRight":
						instance.IsFacingRight = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isDead":
						instance.isDead = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_SlimeArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SlimeArray() : base(typeof(Enemies.Slime[]), ES3UserType_Slime.Instance)
		{
			Instance = this;
		}
	}
}