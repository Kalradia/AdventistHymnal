using System;

namespace AdventistHymnal
{
	public static class Logger
	{
		public static void LogMessage(string message)
		{
			Console.WriteLine($"> {message}");
		}
	}
}