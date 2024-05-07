using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AdventistHymnal.Components
{
	public class HymnDatabase
	{
		public List<Hymn> Hymns { get; private set; } = new();
		public int MinNumber { get; private set; }
		public int MaxNumber { get; private set; }

		public HymnDatabase(){}

		public HymnDatabase(string path)
		{
			if (!Directory.Exists(path))
			{
				Logger.LogMessage("Hymn database is missing");
				return;
			}
			
			Logger.LogMessage($"Loading hymn database");
			string[] filePaths = Directory.GetFiles(path, "*.json");
			MinNumber = int.MaxValue;

			foreach (string filePath in filePaths)
			{
				Hymn hymn = JsonConvert.DeserializeObject<Hymn>(File.ReadAllText(filePath));
				Hymns.Add(hymn);
				
				if (MinNumber > hymn.Number)
				{
					MinNumber = hymn.Number;
				}

				if (MaxNumber < hymn.Number)
				{
					MaxNumber = hymn.Number;
				}
			}
			
			Logger.LogMessage("Hymn database initialized");
		}

		public Hymn GetHymn(int number)
		{
			foreach (Hymn hymn in Hymns)
			{
				if (hymn.Number == number)
				{
					return hymn;
				}
			}
			return null;
		}
	}
}