using System;
using System.IO;
using AdventistHymnal.Components;
using AdventistHymnal.Exporter;
using AdventistHymnal.Importer;

namespace AdventistHymnal
{
	class Program
	{
		public const string DatabaseDirectory = @".\Database\";
		public const string OutputDirectory = @".\Outputs\";
		public const string InputDirectory = @".\Inputs\";

		public static HymnDatabase DB
		{
			get
			{
				if (_db == null)
				{
					_db = new HymnDatabase(DatabaseDirectory);
				}

				return _db;
			}
		}
		private static HymnDatabase _db;

		static void Main(string[] args)
		{
			MainMenu();
		}

		public static void MainMenu()
		{
			Console.Clear();
			Console.WriteLine("Input the number to execute the command.");
			Console.WriteLine("1) Export Hymns");
			Console.WriteLine("2) Import Hymns");
			Console.WriteLine();
			Console.Write("> ");

			if (!int.TryParse(Console.ReadLine(), out int commandId))
			{
				MainMenu();
				return;
			}

			if (commandId == 1)
			{
				ExportHymns();
			}
			else if (commandId == 2)
			{
				ImportHymns();
			}
			else
			{
				MainMenu();
			}
		}

		private static void ExportHymns()
		{
			Console.WriteLine("What format would you like to export?");
			Console.WriteLine("1) ProPresenter");
			Console.WriteLine();
			Console.Write("> ");

			if (!int.TryParse(Console.ReadLine(), out int commandId))
			{
				MainMenu();
				return;
			}

			if (commandId == 1)
			{
				new ProPresenterExporter().ExportAll();
			}
			else
			{
				MainMenu();
				return;
			}
			
			Logger.LogMessage("Press any key to return to the main menu");
			Console.ReadKey();
			
			MainMenu();
		}

		private static void ImportHymns()
		{
			if (!Directory.Exists(InputDirectory))
			{
				Directory.CreateDirectory(InputDirectory);
			}

			TextImporter importer = new();
			importer.ImportTextFiles(InputDirectory);

			// Clear the old database from memory so it can be refreshed
			_db = null;
			
			Logger.LogMessage("Press any key to return to the main menu");
			Console.ReadKey();
			
			MainMenu();
		}
	}
}