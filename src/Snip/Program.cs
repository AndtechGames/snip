using CommandLine;
using System;
using System.IO;

namespace Andtech.Snip
{

	public class Options
	{
		[Value(0, Default = null, HelpText = "The path to the target file/folder.")]
		public string Target { get; set; }
		[Option('c', "copy", Required = false, Default = false, HelpText = "Copy (instead of move) the file.")]
		public bool Copy { get; set; }
	}

	class Program
	{
		private static readonly string SnipboardLocation = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "/.snip");

		static void Main(string[] args)
		{
			Parser.Default
				.ParseArguments<Options>(args)
				.WithParsed(OnParse);
		}

		static void OnParse(Options options)
		{
			if (string.IsNullOrEmpty(options.Target))
			{
				Paste();
			}
			else
			{
				var operation = options.Copy ? SnipOperation.Copy : SnipOperation.Cut;
				var snipboard = Cut(options.Target, operation);

				Console.ForegroundColor = ConsoleColor.Green;
				switch (snipboard.Operation)
				{
					case SnipOperation.Cut:
						Console.WriteLine($"'{Path.GetFileName(snipboard.TargetPath)}' cut to snipboard");
						break;
					case SnipOperation.Copy:
						Console.WriteLine($"'{Path.GetFileName(snipboard.TargetPath)}' copied to snipboard");
						break;
				};
				Console.ResetColor();
			}

			Snipboard Cut(string targetPath, SnipOperation operation)
			{
				targetPath = Path.GetFullPath(targetPath);
				var snipboard = new Snipboard(targetPath, operation);
				Snipboard.Write(SnipboardLocation, snipboard);

				return snipboard;
			}

			void Paste(string destinationDirectory = "")
			{
				Snipboard snipboard;
				try
				{
					snipboard = Snipboard.Read(SnipboardLocation);
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Error.WriteLine("[ERROR] Snipboard is empty");
					Console.ResetColor();

					return;
				}

				var destination = Path.Combine(
					destinationDirectory,
					Path.GetFileName(snipboard.TargetPath));

				switch (snipboard.Operation)
				{
					case SnipOperation.Cut:
						FileMacros.Move(snipboard.TargetPath, destination);
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"'{Path.GetFileName(snipboard.TargetPath)}' cut from snipboard'");
						Console.ResetColor();

						File.Delete(SnipboardLocation);
						break;
					case SnipOperation.Copy:
						FileMacros.Copy(snipboard.TargetPath, destination);

						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"'{Path.GetFileName(snipboard.TargetPath)}' copied from snipboard'");
						Console.ResetColor();
						break;
				}
			}
		}
	}
}
