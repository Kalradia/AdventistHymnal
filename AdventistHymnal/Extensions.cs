namespace AdventistHymnal
{
	public static class Extensions
	{
		public static string MakeValidFileName(this string name)
		{
			string invalidChars =
				System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
			string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

			return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "");
		}
	}
}