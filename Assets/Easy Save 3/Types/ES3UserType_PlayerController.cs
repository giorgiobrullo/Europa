using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("moveSpeed", "jumpHeight", "attack", "row", "menu", "stats", "mobileController", "damageFromPatrols", "dust", "_canJump", "_canDoubleJump", "_isDead", "_nextAttackTime", "_rb", "_animator", "_playerControllerUp", "_horizontalInput", "_lastJumpInputTime", "_externalForce", "m_CancellationTokenSource")]
	public class ES3UserType_PlayerController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerController() : base(typeof(Player.PlayerController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Player.PlayerController)obj;
			
			writer.WritePrivateField("moveSpeed", instance);
			writer.WritePrivateField("jumpHeight", instance);
			writer.WritePrivateFieldByRef("attack", instance);
			writer.WritePrivateFieldByRef("row", instance);
			writer.WritePrivateFieldByRef("menu", instance);
			writer.WritePrivateFieldByRef("stats", instance);
			writer.WritePrivateFieldByRef("mobileController", instance);
			writer.WritePrivateField("damageFromPatrols", instance);
			writer.WritePrivateFieldByRef("dust", instance);
			writer.WritePrivateField("_canJump", instance);
			writer.WritePrivateField("_canDoubleJump", instance);
			writer.WritePrivateField("_isDead", instance);
			writer.WritePrivateField("_nextAttackTime", instance);
			writer.WritePrivateFieldByRef("_rb", instance);
			writer.WritePrivateFieldByRef("_animator", instance);
			writer.WritePrivateFieldByRef("_playerControllerUp", instance);
			writer.WritePrivateField("_horizontalInput", instance);
			writer.WritePrivateField("_lastJumpInputTime", instance);
			writer.WritePrivateField("_externalForce", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Player.PlayerController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "moveSpeed":
					instance = (Player.PlayerController)reader.SetPrivateField("moveSpeed", reader.Read<System.Single>(), instance);
					break;
					case "jumpHeight":
					instance = (Player.PlayerController)reader.SetPrivateField("jumpHeight", reader.Read<System.Single>(), instance);
					break;
					case "attack":
					instance = (Player.PlayerController)reader.SetPrivateField("attack", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "row":
					instance = (Player.PlayerController)reader.SetPrivateField("row", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "menu":
					instance = (Player.PlayerController)reader.SetPrivateField("menu", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "stats":
					instance = (Player.PlayerController)reader.SetPrivateField("stats", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "mobileController":
					instance = (Player.PlayerController)reader.SetPrivateField("mobileController", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "damageFromPatrols":
					instance = (Player.PlayerController)reader.SetPrivateField("damageFromPatrols", reader.Read<System.Int32>(), instance);
					break;
					case "dust":
					instance = (Player.PlayerController)reader.SetPrivateField("dust", reader.Read<UnityEngine.ParticleSystem>(), instance);
					break;
					case "_canJump":
					instance = (Player.PlayerController)reader.SetPrivateField("_canJump", reader.Read<System.Boolean>(), instance);
					break;
					case "_canDoubleJump":
					instance = (Player.PlayerController)reader.SetPrivateField("_canDoubleJump", reader.Read<System.Boolean>(), instance);
					break;
					case "_isDead":
					instance = (Player.PlayerController)reader.SetPrivateField("_isDead", reader.Read<System.Boolean>(), instance);
					break;
					case "_nextAttackTime":
					instance = (Player.PlayerController)reader.SetPrivateField("_nextAttackTime", reader.Read<System.Single>(), instance);
					break;
					case "_rb":
					instance = (Player.PlayerController)reader.SetPrivateField("_rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "_animator":
					instance = (Player.PlayerController)reader.SetPrivateField("_animator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "_playerControllerUp":
					instance = (Player.PlayerController)reader.SetPrivateField("_playerControllerUp", reader.Read<Player.PlayerControllerUp>(), instance);
					break;
					case "_horizontalInput":
					instance = (Player.PlayerController)reader.SetPrivateField("_horizontalInput", reader.Read<System.Single>(), instance);
					break;
					case "_lastJumpInputTime":
					instance = (Player.PlayerController)reader.SetPrivateField("_lastJumpInputTime", reader.Read<System.Single>(), instance);
					break;
					case "_externalForce":
					instance = (Player.PlayerController)reader.SetPrivateField("_externalForce", reader.Read<UnityEngine.Vector2>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (Player.PlayerController)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerControllerArray() : base(typeof(Player.PlayerController[]), ES3UserType_PlayerController.Instance)
		{
			Instance = this;
		}
	}
}