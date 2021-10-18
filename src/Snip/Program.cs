using CommandLine;
using System;
using System.IO;

namespace Andtech
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
		private static readonly string SnipFilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "/.snip");
		private static string SnipTarget
		{
			get
			{
				if (File.Exists(SnipFilePath))
				{
					return File.ReadAllText(SnipFilePath);
				}

				return null;
			}
			set => File.WriteAllText(SnipFilePath, value);
		}

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
				Paste(clear: !options.Copy);
			}
			else
			{
				Cut(options.Target);

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(SnipTarget);
				Console.ResetColor();
			}

			void Cut(string path)
			{
				SnipTarget = Path.GetFullPath(path);
			}

			void Paste(string destinationDirectory = "./", bool clear = true)
			{
				var path = SnipTarget;
				if (string.IsNullOrEmpty(path))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Error.WriteLine("[ERROR] Clipboard is empty!");
					Console.ResetColor();
				}
				else
				{
					var destination = Path.Combine(
						destinationDirectory,
						Path.GetFileName(path)
					);

					var attributes = File.GetAttributes(path);
					if (attributes.HasFlag(FileAttributes.Directory))
					{
						Directory.Move(path, destination);
					}
					else
					{
						File.Move(path, destination);
					}

					if (clear)
					{
						File.Delete(SnipFilePath);
					}
				}
			}
		}
	}
}
