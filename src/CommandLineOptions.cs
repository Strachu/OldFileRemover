//
// Copyright (c) 2016 Patryk Strach
//
// This file is part of OldFileRemover.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using System.IO;

namespace OldFileRemover
{
	public class CommandLineOptions
	{
		[Value(0, MetaName = "directory-path", Required = true, HelpText = "Directory to remove files from.")]
		public string DirectoryPath { get; set; }

		[Option('m', "max-size", Required = true, HelpText = "Max size of directory in bytes. The old files will be removed after the size of directory exceeds this value.")]
		public long MaxDirectorySize { get; set; }
		
		[Option('e', "exclude", HelpText = "Regexes defining files to exclude.")]
		public IEnumerable<string> ExcludeRegexes { get; set; }

		public static string GetHelpText(ParserResult<CommandLineOptions> parsingResult)
		{
			var exeName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
			
			var help = HelpText.AutoBuild(parsingResult);

			help.AddPreOptionsLine(" ");
			help.AddPreOptionsLine($"Usage: {exeName} [options] --max-size size directory");

			return help.ToString();
		}
	}
}

