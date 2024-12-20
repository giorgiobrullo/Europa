using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("loop", "prewarm", "startDelay", "startDelayMultiplier", "startLifetime", "startLifetimeMultiplier", "startSpeed", "startSpeedMultiplier", "startSize3D", "startSize", "startSizeMultiplier", "startSizeX", "startSizeXMultiplier", "startSizeY", "startSizeYMultiplier", "startSizeZ", "startSizeZMultiplier", "startRotation3D", "startRotation", "startRotationMultiplier", "startRotationX", "startRotationXMultiplier", "startRotationY", "startRotationYMultiplier", "startRotationZ", "startRotationZMultiplier", "startColor", "gravityModifier", "gravityModifierMultiplier", "simulationSpace", "customSimulationSpace", "simulationSpeed", "scalingMode", "playOnAwake", "maxParticles")]
	public class ES3UserType_MainModule : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3UserType_MainModule() : base(typeof(UnityEngine.ParticleSystem.MainModule)){ Instance = this; priority = 1;}


		public override void Write(object obj, ES3Writer writer)
		{
			var instance = (UnityEngine.ParticleSystem.MainModule)obj;
			
			writer.WriteProperty("loop", instance.loop, ES3Type_bool.Instance);
			writer.WriteProperty("prewarm", instance.prewarm, ES3Type_bool.Instance);
			writer.WriteProperty("startDelay", instance.startDelay, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startDelayMultiplier", instance.startDelayMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startLifetime", instance.startLifetime, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startLifetimeMultiplier", instance.startLifetimeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSpeed", instance.startSpeed, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSpeedMultiplier", instance.startSpeedMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSize3D", instance.startSize3D, ES3Type_bool.Instance);
			writer.WriteProperty("startSize", instance.startSize, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeMultiplier", instance.startSizeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeX", instance.startSizeX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeXMultiplier", instance.startSizeXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeY", instance.startSizeY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeYMultiplier", instance.startSizeYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startSizeZ", instance.startSizeZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startSizeZMultiplier", instance.startSizeZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotation3D", instance.startRotation3D, ES3Type_bool.Instance);
			writer.WriteProperty("startRotation", instance.startRotation, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationMultiplier", instance.startRotationMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationX", instance.startRotationX, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationXMultiplier", instance.startRotationXMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationY", instance.startRotationY, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationYMultiplier", instance.startRotationYMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startRotationZ", instance.startRotationZ, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("startRotationZMultiplier", instance.startRotationZMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("startColor", instance.startColor, ES3Type_MinMaxGradient.Instance);
			writer.WriteProperty("gravityModifier", instance.gravityModifier, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("gravityModifierMultiplier", instance.gravityModifierMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("simulationSpace", instance.simulationSpace, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.ParticleSystemSimulationSpace)));
			writer.WritePropertyByRef("customSimulationSpace", instance.customSimulationSpace);
			writer.WriteProperty("simulationSpeed", instance.simulationSpeed, ES3Type_float.Instance);
			writer.WriteProperty("scalingMode", instance.scalingMode, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.ParticleSystemScalingMode)));
			writer.WriteProperty("playOnAwake", instance.playOnAwake, ES3Type_bool.Instance);
			writer.WriteProperty("maxParticles", instance.maxParticles, ES3Type_int.Instance);
		}

		public override object Read<T>(ES3Reader reader)
		{
			var instance = new UnityEngine.ParticleSystem.MainModule();
			string propertyName;
			while((propertyName = reader.ReadPropertyName()) != null)
			{
				switch(propertyName)
				{
					
					case "loop":
						instance.loop = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "prewarm":
						instance.prewarm = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "startDelay":
						instance.startDelay = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startDelayMultiplier":
						instance.startDelayMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startLifetime":
						instance.startLifetime = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startLifetimeMultiplier":
						instance.startLifetimeMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startSpeed":
						instance.startSpeed = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startSpeedMultiplier":
						instance.startSpeedMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startSize3D":
						instance.startSize3D = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "startSize":
						instance.startSize = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startSizeMultiplier":
						instance.startSizeMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startSizeX":
						instance.startSizeX = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startSizeXMultiplier":
						instance.startSizeXMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startSizeY":
						instance.startSizeY = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startSizeYMultiplier":
						instance.startSizeYMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startSizeZ":
						instance.startSizeZ = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startSizeZMultiplier":
						instance.startSizeZMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startRotation3D":
						instance.startRotation3D = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "startRotation":
						instance.startRotation = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startRotationMultiplier":
						instance.startRotationMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startRotationX":
						instance.startRotationX = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startRotationXMultiplier":
						instance.startRotationXMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startRotationY":
						instance.startRotationY = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startRotationYMultiplier":
						instance.startRotationYMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startRotationZ":
						instance.startRotationZ = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "startRotationZMultiplier":
						instance.startRotationZMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "startColor":
						instance.startColor = reader.Read<UnityEngine.ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
						break;
					case "gravityModifier":
						instance.gravityModifier = reader.Read<UnityEngine.ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						break;
					case "gravityModifierMultiplier":
						instance.gravityModifierMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "simulationSpace":
						instance.simulationSpace = reader.Read<UnityEngine.ParticleSystemSimulationSpace>(ES3Type_enum.Instance);
						break;
					case "customSimulationSpace":
						instance.customSimulationSpace = reader.Read<UnityEngine.Transform>(ES3UserType_Transform.Instance);
						break;
					case "simulationSpeed":
						instance.simulationSpeed = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "scalingMode":
						instance.scalingMode = reader.Read<UnityEngine.ParticleSystemScalingMode>(ES3Type_enum.Instance);
						break;
					case "playOnAwake":
						instance.playOnAwake = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "maxParticles":
						instance.maxParticles = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
			return instance;
		}
	}


	public class ES3UserType_MainModuleArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_MainModuleArray() : base(typeof(UnityEngine.ParticleSystem.MainModule[]), ES3UserType_MainModule.Instance)
		{
			Instance = this;
		}
	}
}