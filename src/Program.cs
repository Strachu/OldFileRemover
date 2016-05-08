using System;
using CommandLine;
using CommandLine.Text;

namespace OldFileRemover
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var parsingResult = new Parser(x => x.HelpWriter = null).ParseArguments<CommandLineOptions>(args);

			return parsingResult.MapResult(Run, errors =>
			{
				Console.WriteLine(CommandLineOptions.GetHelpText(parsingResult));
				return 1;
			});
		}

		private static int Run(CommandLineOptions options)
		{
			Console.WriteLine($"Directory: \"{options.DirectoryPath}\"");
			Console.WriteLine($"MaxDirectorySize: \"{options.MaxDirectorySize}\"");
			Console.WriteLine($"ExcludeRegexes: \"{String.Join(", ", options.ExcludeRegexes)}\"");

			return 0;
		}
	}
}
