using System;
using AdventistHymnal.Data;
using Newtonsoft.Json;

namespace AdventistHymnal.Components
{
	[JsonObject]
	public class StanzaReference
	{
		/// <summary>
		/// Type of stanza. (Verse, Chorus, Bridge, etc.)
		/// </summary>
		[JsonProperty]
		public StanzaType Type;
		
		/// <summary>
		/// Index within the stanza type. (Verse 1, 2, 3, etc.)
		/// </summary>
		[JsonProperty]
		public int Number;

		public StanzaReference() {}

		public StanzaReference(StanzaType type, int number)
		{
			Type = type;
			Number = number;
		}

		public static StanzaReference FromString(string s)
		{
			string[] subStrings = s.Split(' ');
			Enum.TryParse(subStrings[0], true, out StanzaType result);
			int number = 0;

			if (subStrings.Length > 1)
			{
				number = int.Parse(subStrings[1]);
			}
			
			return new StanzaReference(result, number);
		}

		public override string ToString()
		{
			if (Number <= 0)
			{
				return Type.ToString();
			}
			
			return $"{Type.ToString()} {Number.ToString()}";
		}
	}
}