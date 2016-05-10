using System;
using CommandLine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
			var files = new DirectoryInfo(options.DirectoryPath)
				.GetFileSystemInfos()
				.Where(x => !options.ExcludeRegexes.Any(regex => Regex.IsMatch(x.Name, regex)))
				.OrderBy(x => x.LastWriteTime)
				.Select(x => new
			{
				Info = x,
				Size = FileSizeCalculator.CalculateFileSize(x)
			}).ToList();

			var totalDirectorySize = files.Sum(x => x.Size);

			Console.WriteLine($"Cleaning {options.DirectoryPath} ...");
			Console.WriteLine($"\tMax allowed size: {UserFriendlySizeFormatter.Format(options.MaxDirectorySize)}");
			Console.WriteLine($"\tCurrent size: {UserFriendlySizeFormatter.Format(totalDirectorySize)}");

			while(totalDirectorySize > options.MaxDirectorySize && files.Any())
			{
				var entry = files.First();
				files = files.Skip(1).ToList();

				Console.WriteLine($"\t\tRemoving {entry.Info.Name} ...");

				if(entry.Info.Attributes.HasFlag(FileAttributes.Directory))
				{
					Directory.Delete(entry.Info.FullName, recursive: true);
				}
				else
				{
					File.Delete(entry.Info.FullName);
				}

				totalDirectorySize -= entry.Size;

				Console.WriteLine($"\t\tThe directory is now {totalDirectorySize} bytes.");
			}

			Console.WriteLine($"Finished");

			return 0;
		}
	}
}
