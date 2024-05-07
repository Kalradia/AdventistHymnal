using System.Collections.Generic;
using AdventistHymnal.Data;
using Newtonsoft.Json;

namespace AdventistHymnal.Components
{
	[JsonObject]
	public class Stanza
	{
		/// <summary>
		/// Identifier of the stanza.
		/// </summary>
		[JsonProperty]
		public StanzaReference Reference { get; private set; }

		/// <summary>
		/// Lines of the stanza.
		/// </summary>
		[JsonProperty]
		public List<string> Lines = new();

		public Stanza() {}

		public Stanza(StanzaType type, int number)
		{
			Reference = new StanzaReference(type, number);
		}

		public Stanza(StanzaReference reference)
		{
			Reference = reference;
		}

		public override string ToString()
		{
			return Reference.ToString();
		}
	}
}