# AdventistHymnal
This is a management and export tool for the Seventh-day Adventist hymnal into various presentation software, such as ProPresenter, Proclaim, or MediaShout.

The hymn files and idea came from <a>https://github.com/ariseforgod/adventist_hymnal</a>. That repository is no longer maintained, so I wrote this new program to manage the hymns and assist in the importing process into ProPresenter. Other software can be supported, but contributions will be required to get them working. The framework is here for it, though. It just needs a bit of work.

Additionally, this tool can allow you to easily modify the layout of the files in bulk. This makes it easy to adjust spacing and re-import in just a few minutes.

## How to use
The hymns for ProPresenter are already prepared in the 'Outputs/ProPresenter' folder. If you would like to further customize the aesthetic, manual tweaking of the generation system will be required. But they work if you just mass import them as .txt files.

If you build the project, you must copy the Inputs/Database folders into the root directory with the executable.

- Inputs: Raw text files of all hymns.
- Database: Imported hymn files that are parsed and turned into json.
- Outputs: Final output hymns, ready to be imported.

## To do
- Improve abstraction to allow other exporters that may not have a similar syntax.
- Review copyright information
- Add author credits
- Review text for typos or other issues
- Phase out 'Inputs' folder in favor of the json database

## Closing notes
If you have questions or would like to contribute, please contact me, my information is below.

Discord: kalradia

*I sincerely hope this helps you and your church up your presentation game and makes your life a bit easier.*
