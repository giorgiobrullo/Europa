using System;
using Enemies.Generic;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("gravityScale", "hurtSound", "idleSound", "_collider2D", "_velocity", "_enemyAttack", "_enemyMovement", "_enemyHealth", "_idleSound", "_playerController", "m_CancellationTokenSource", "Animator", "Rigidbody", "Target", "IsFacingRight", "isDead", "enabled", "name")]
	public class ES3UserType_GenericEnemy : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_GenericEnemy() : base(typeof(GenericEnemy)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (GenericEnemy)obj;
			
			writer.WritePrivateField("gravityScale", instance);
			writer.WritePropertyByRef("hurtSound", instance.hurtSound);
			writer.WritePropertyByRef("idleSound", instance.idleSound);
			writer.WritePrivateFieldByRef("_collider2D", instance);
			writer.WritePrivateField("_velocity", instance);
			writer.WritePrivateFieldByRef("_enemyAttack", instance);
			writer.WritePrivateFieldByRef("_enemyMovement", instance);
			writer.WritePrivateFieldByRef("_enemyHealth", instance);
			writer.WritePrivateFieldByRef("_idleSound", instance);
			writer.WritePrivateFieldByRef("_playerController", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
			writer.WritePrivatePropertyByRef("Animator", instance);
			writer.WritePrivatePropertyByRef("Rigidbody", instance);
			writer.WritePrivatePropertyByRef("Target", instance);
			writer.WriteProperty("IsFacingRight", instance.IsFacingRight, ES3Type_bool.Instance);
			writer.WriteProperty("isDead", instance.isDead, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (GenericEnemy)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "gravityScale":
					instance = (GenericEnemy)reader.SetPrivateField("gravityScale", reader.Read<System.Single>(), instance);
					break;
					case "hurtSound":
						instance.hurtSound = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "idleSound":
						instance.idleSound = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "_collider2D":
					instance = (GenericEnemy)reader.SetPrivateField("_collider2D", reader.Read<UnityEngine.BoxCollider2D>(), instance);
					break;
					case "_velocity":
					instance = (GenericEnemy)reader.SetPrivateField("_velocity", reader.Read<UnityEngine.Vector2>(), instance);
					break;
					case "_enemyAttack":
					instance = (GenericEnemy)reader.SetPrivateField("_enemyAttack", reader.Read<EnemyAttack>(), instance);
					break;
					case "_enemyMovement":
					instance = (GenericEnemy)reader.SetPrivateField("_enemyMovement", reader.Read<EnemyMovement>(), instance);
					break;
					case "_enemyHealth":
					instance = (GenericEnemy)reader.SetPrivateField("_enemyHealth", reader.Read<EnemyHealth>(), instance);
					break;
					case "_idleSound":
					instance = (GenericEnemy)reader.SetPrivateField("_idleSound", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "_playerController":
					instance = (GenericEnemy)reader.SetPrivateField("_playerController", reader.Read<Player.PlayerController>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (GenericEnemy)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					case "Animator":
					instance = (GenericEnemy)reader.SetPrivateProperty("Animator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "Rigidbody":
					instance = (GenericEnemy)reader.SetPrivateProperty("Rigidbody", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "Target":
					instance = (GenericEnemy)reader.SetPrivateProperty("Target", reader.Read<UnityEngine.GameObject>(), instance);
					break;
					case "IsFacingRight":
						instance.IsFacingRight = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isDead":
						instance.isDead = reader.Read<System.Boolean>(ES3Type_bool.Instance);
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


	public class ES3UserType_GenericEnemyArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GenericEnemyArray() : base(typeof(GenericEnemy[]), ES3UserType_GenericEnemy.Instance)
		{
			Instance = this;
		}
	}
}