using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AdventistHymnal.Data
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CopyrightType
	{
		PublicDomain = 0,
		Copyrighted = 1,
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum StanzaType
	{
		Intro,
		Verse,
		Chorus,
		Bridge,
	}
}