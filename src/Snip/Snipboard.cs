using System.IO;
using System.Text.Json;

namespace Andtech.Snip
{

	public enum SnipOperation
	{
		Cut,
		Copy,
	}

	public class Snipboard
	{
		public string TargetPath { get; set; }
		public SnipOperation Operation { get; set; }

		public Snipboard(string targetPath, SnipOperation operation = default)
		{
			this.TargetPath = targetPath;
			this.Operation = operation;
		}

		public static Snipboard Read(string path)
		{
			var content = File.ReadAllText(path);
			return JsonSerializer.Deserialize<Snipboard>(content);
		}

		public static void Write(string path, Snipboard snipboard)
		{
			var json = JsonSerializer.Serialize(snipboard);
			File.WriteAllText(path, json);
		}
	}
}
