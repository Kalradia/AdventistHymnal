namespace AdventistHymnal.Exporter
{
	public class ProPresenterExporter : HymnExporter
	{
		protected override string OutputFolderName => "ProPresenter";
		protected override string TitleTag => "Title:";
		protected override string AuthorTag => "Author:";
		protected override string CCLITag => "CCLI:";
		protected override string[] InvalidCharacters => new[] { ":" };
	}
}