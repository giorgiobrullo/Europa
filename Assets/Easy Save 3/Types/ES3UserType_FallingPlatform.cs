using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("fallDelay", "initialFallSpeed", "acceleration", "playerLayer", "_rb", "_isFalling", "enabled", "name")]
	public class ES3UserType_FallingPlatform : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_FallingPlatform() : base(typeof(Traps.FallingPlatform)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Traps.FallingPlatform)obj;
			
			writer.WriteProperty("fallDelay", instance.fallDelay, ES3Type_float.Instance);
			writer.WriteProperty("initialFallSpeed", instance.initialFallSpeed, ES3Type_float.Instance);
			writer.WriteProperty("acceleration", instance.acceleration, ES3Type_float.Instance);
			writer.WriteProperty("playerLayer", instance.playerLayer, ES3Type_LayerMask.Instance);
			writer.WritePrivateFieldByRef("_rb", instance);
			writer.WritePrivateField("_isFalling", instance);
			writer.WriteProperty("enabled", instance.enabled, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Traps.FallingPlatform)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "fallDelay":
						instance.fallDelay = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "initialFallSpeed":
						instance.initialFallSpeed = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "acceleration":
						instance.acceleration = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "playerLayer":
						instance.playerLayer = reader.Read<UnityEngine.LayerMask>(ES3Type_LayerMask.Instance);
						break;
					case "_rb":
					instance = (Traps.FallingPlatform)reader.SetPrivateField("_rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "_isFalling":
					instance = (Traps.FallingPlatform)reader.SetPrivateField("_isFalling", reader.Read<System.Boolean>(), instance);
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


	public class ES3UserType_FallingPlatformArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_FallingPlatformArray() : base(typeof(Traps.FallingPlatform[]), ES3UserType_FallingPlatform.Instance)
		{
			Instance = this;
		}
	}
}