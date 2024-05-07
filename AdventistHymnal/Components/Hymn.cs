using System.Collections.Generic;
using AdventistHymnal.Data;
using Newtonsoft.Json;

namespace AdventistHymnal.Components
{
	[JsonObject]
	public class Hymn
	{
		[JsonProperty]
		public int Number;
		[JsonProperty]
		public string Title;
		[JsonProperty]
		public string Author;
		[JsonProperty]
		public CopyrightType Copyright;
		[JsonProperty]
		public uint? CcliNumber;
		[JsonProperty]
		public List<Stanza> Stanzas = new();
		[JsonProperty]
		public List<string> StanzaOrder = new();

		public Stanza GetStanza(StanzaReference reference)
		{
			return GetStanza(reference.Type, reference.Number);
		}

		public Stanza GetStanza(StanzaType type, int number)
		{
			foreach (Stanza stanza in Stanzas)
			{
				if (stanza.Reference.Type == type && stanza.Reference.Number == number)
				{
					return stanza;
				}
			}

			return null;
		}
	}
}