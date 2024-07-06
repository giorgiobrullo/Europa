using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("groundCheckSize", "groundCheck", "whatIsGround", "_isGrounded", "_jumpCount", "_jumpCooldown", "_lastJumpTime", "_parentAnimator", "m_CancellationTokenSource")]
	public class ES3UserType_PlayerControllerUp : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerControllerUp() : base(typeof(Player.PlayerControllerUp)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Player.PlayerControllerUp)obj;
			
			writer.WritePrivateField("groundCheckSize", instance);
			writer.WritePrivateFieldByRef("groundCheck", instance);
			writer.WritePrivateField("whatIsGround", instance);
			writer.WritePrivateField("_isGrounded", instance);
			writer.WritePrivateField("_jumpCount", instance);
			writer.WritePrivateField("_jumpCooldown", instance);
			writer.WritePrivateField("_lastJumpTime", instance);
			writer.WritePrivateFieldByRef("_parentAnimator", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Player.PlayerControllerUp)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "groundCheckSize":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("groundCheckSize", reader.Read<UnityEngine.Vector2>(), instance);
					break;
					case "groundCheck":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("groundCheck", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "whatIsGround":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("whatIsGround", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "_isGrounded":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("_isGrounded", reader.Read<System.Boolean>(), instance);
					break;
					case "_jumpCount":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("_jumpCount", reader.Read<System.Int32>(), instance);
					break;
					case "_jumpCooldown":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("_jumpCooldown", reader.Read<System.Single>(), instance);
					break;
					case "_lastJumpTime":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("_lastJumpTime", reader.Read<System.Single>(), instance);
					break;
					case "_parentAnimator":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("_parentAnimator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (Player.PlayerControllerUp)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerControllerUpArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerControllerUpArray() : base(typeof(Player.PlayerControllerUp[]), ES3UserType_PlayerControllerUp.Instance)
		{
			Instance = this;
		}
	}
}