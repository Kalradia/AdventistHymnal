using System;
using System.IO;
using AdventistHymnal.Components;
using AdventistHymnal.Data;
using Newtonsoft.Json;

namespace AdventistHymnal.Importer
{
	public class TextImporter
	{
		public void ImportTextFiles(string path)
		{
			string[] files = Directory.GetFiles(path);

			if (files.Length == 0)
			{
				Logger.LogMessage("No hymns found in the input folder!");
				return;
			}

			foreach (string fP in files)
			{
				Hymn hymn = new();
				_activeStanza = StanzaType.Verse;
				using (StreamReader reader = new(fP))
				{
					while (!reader.EndOfStream)
					{
						ProcessLine(reader.ReadLine(), ref hymn);
					}
				}

				if (hymn.StanzaOrder.Count == 0)
				{
					foreach (Stanza s in hymn.Stanzas)
					{
						hymn.StanzaOrder.Add(s.ToString());
					}
				}

				string json = JsonConvert.SerializeObject(hymn, Formatting.Indented);
				
				if (!Directory.Exists(Program.DatabaseDirectory))
					Directory.CreateDirectory(Program.DatabaseDirectory);
				
				File.WriteAllText($"{Program.DatabaseDirectory}{hymn.Number:000} - {hymn.Title.MakeValidFileName()}.json", json);
				
				Console.WriteLine($"Imported #{hymn.Number} - {hymn.Title}");
			}
			
			Logger.LogMessage("Hymn database generation complete!");
		}

		private StanzaType _activeStanza;
		private int _activeStanzaNumber;

		private void ProcessLine(string line, ref Hymn hymn)
		{
			if (string.IsNullOrEmpty(line))
			{
				// Nothing
			}
			else if (line.StartsWith("Title:"))
			{
				hymn.Title = line.Replace("Title:", "");
			}
			else if (line.StartsWith("Author:"))
			{
				// TODO: Author information needs to be filled in properly and then this can be enabled.
				//hymn.Author = line.Replace("Author:", "");
			}
			else if (line.StartsWith("Copyright:"))
			{
				string copy = line.Replace("Copyright:", "");
				if (copy == "Public Domain")
				{
					hymn.Copyright = CopyrightType.PublicDomain;
				}
				else if (copy == "Projection Permitted Under" || copy == "Projection Not Permitted")
				{
					hymn.Copyright = CopyrightType.Copyrighted;
				}
				else
				{
					Console.WriteLine($"Unhandled copyright information: {copy}");
				}
			}
			else if (line.StartsWith("CCLI:"))
			{
				string value = line.Replace("CCLI:", "");
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				hymn.CcliNumber = uint.Parse(value);
			}
			else if (line.StartsWith("Hymnal:"))
			{
				hymn.Number = int.Parse(line.Replace("Hymnal:Hymn ", ""));
			}
			else if (line.StartsWith("PlayOrder:"))
			{
				string[] stanzas = line.Replace("PlayOrder:", "").Split(char.Parse(","));

				foreach (string stanza in stanzas)
				{
					(StanzaType, int) s = GetStanzaType(stanza);
					hymn.StanzaOrder.Add(new StanzaReference(s.Item1, s.Item2).ToString());
				}
			}
			else if (line.StartsWith("[Intro]"))
			{
				_activeStanza = StanzaType.Intro;
				_activeStanzaNumber = 0;
				CreateStanza(ref hymn, StanzaType.Intro, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 1]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 1;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 2]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 2;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 3]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 3;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 4]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 4;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 5]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 5;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 6]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 6;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 7]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 7;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Verse 8]"))
			{
				_activeStanza = StanzaType.Verse;
				_activeStanzaNumber = 8;
				CreateStanza(ref hymn, StanzaType.Verse, _activeStanzaNumber);
			}
			else if (line.StartsWith("[Chorus]"))
			{
				_activeStanza = StanzaType.Chorus;
				_activeStanzaNumber = 0;
				CreateStanza(ref hymn, StanzaType.Chorus, _activeStanzaNumber);
			}
			else
			{
				Stanza stanza = hymn.GetStanza(_activeStanza, _activeStanzaNumber);
				stanza.Lines.Add(line);
			}
		}

		private (StanzaType, int) GetStanzaType(string s)
		{
			StanzaType type;
			int number = 0;
			
			switch (s)
			{
				case "Intro":
					type = StanzaType.Intro;
					break;
				case "Verse 1":
					type = StanzaType.Verse;
					number = 1;
					break;
				case "Verse 2":
					type = StanzaType.Verse;
					number = 2;
					break;
				case "Verse 3":
					type = StanzaType.Verse;
					number = 3;
					break;
				case "Verse 4":
					type = StanzaType.Verse;
					number = 4;
					break;
				case "Verse 5":
					type = StanzaType.Verse;
					number = 5;
					break;
				case "Verse 6":
					type = StanzaType.Verse;
					number = 6;
					break;
				case "Verse 7":
					type = StanzaType.Verse;
					number = 7;
					break;
				case "Verse 8":
					type = StanzaType.Verse;
					number = 8;
					break;
				case "Chorus":
					type = StanzaType.Chorus;
					break;
				default:
					throw new SystemException($"Not implemented stanza type: {s}");
			}
			
			return new ValueTuple<StanzaType, int>(type, number);
		}

		private void CreateStanza(ref Hymn hymn, StanzaType type, int number)
		{
			Stanza stanza = hymn.GetStanza(type, number);

			if (stanza != null)
			{
				return;
			}
			
			hymn.Stanzas.Add(new Stanza(type, number));
		}
	}
}