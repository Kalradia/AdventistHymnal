using System;
using System.IO;
using System.Text;
using AdventistHymnal.Components;

namespace AdventistHymnal.Exporter
{
	public abstract class HymnExporter
	{
		// TODO: Move these to a config.ini file
		protected const int MaxLinesPerSlide = 2;
		protected const int CCLINumber = 999999;
		
		protected abstract string OutputFolderName { get; }
		protected abstract string TitleTag { get; }
		protected abstract string AuthorTag { get; }
		protected abstract string CCLITag { get; }
		protected abstract string[] InvalidCharacters { get; }

		public void ExportAll()
		{
			ExportRange(Program.DB.MinNumber, Program.DB.MaxNumber);
		}

		public void ExportRange(int min, int max)
		{
			Logger.LogMessage($"Beginning export for hymns #{min} - #{max}");
			for (int i = min; i <= max; i++)
			{
				ExportHymn(i);
			}
		}

		public void ExportHymn(int number)
		{
			Hymn hymn = Program.DB.GetHymn(number);

			StringBuilder builder = new();
			builder.AppendLine($"{TitleTag}#{hymn.Number} {hymn.Title}");
			builder.AppendLine($"{AuthorTag}{hymn.Author}");
			
			if (hymn.CcliNumber.HasValue)
			{
				builder.AppendLine($"{CCLITag}{hymn.CcliNumber.Value}");
			}
			
			builder.AppendLine();

			builder.AppendLine("Tag");
			builder.AppendLine($"SDA Hymnal #{hymn.Number}");
			builder.AppendLine($"{hymn.Title}");
			builder.AppendLine();

			foreach (string s in hymn.StanzaOrder)
			{
				AddStanza(hymn.GetStanza(StanzaReference.FromString(s)), ref builder);
				builder.AppendLine();
			}
			
			string path = @$"{AppDomain.CurrentDomain.BaseDirectory}Outputs\{OutputFolderName}\";
			Directory.CreateDirectory(path);
			File.WriteAllText($"{path}{hymn.Number:000}.txt", builder.ToString());
			Logger.LogMessage($"Hymn #{hymn.Number} successful!");
		}

		private void AddStanza(Stanza stanza, ref StringBuilder builder)
		{
			builder.AppendLine(stanza.Reference.ToString());
			int x = 0;

			foreach (string line in stanza.Lines)
			{
				if (x == MaxLinesPerSlide)
				{
					builder.AppendLine();
					x = 0;
				}

				string output = line;

				foreach (string c in InvalidCharacters)
				{
					output = output.Replace(c, string.Empty);
				}
				
				builder.AppendLine(output);
				x++;
			}
		}
	}
}