using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_score", "_scoreCoins", "_scoreGems", "_scoreStars")]
	public class ES3UserType_ScoreManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ScoreManager() : base(typeof(Other.ScoreManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Other.ScoreManager)obj;
			
			writer.WritePrivateField("_score", instance);
			writer.WritePrivateField("_scoreCoins", instance);
			writer.WritePrivateField("_scoreGems", instance);
			writer.WritePrivateField("_scoreStars", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Other.ScoreManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_score":
					instance = (Other.ScoreManager)reader.SetPrivateField("_score", reader.Read<System.Int32>(), instance);
					break;
					case "_scoreCoins":
					instance = (Other.ScoreManager)reader.SetPrivateField("_scoreCoins", reader.Read<System.Int32>(), instance);
					break;
					case "_scoreGems":
					instance = (Other.ScoreManager)reader.SetPrivateField("_scoreGems", reader.Read<System.Int32>(), instance);
					break;
					case "_scoreStars":
					instance = (Other.ScoreManager)reader.SetPrivateField("_scoreStars", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ScoreManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ScoreManagerArray() : base(typeof(Other.ScoreManager[]), ES3UserType_ScoreManager.Instance)
		{
			Instance = this;
		}
	}
}