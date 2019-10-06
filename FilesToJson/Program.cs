using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FilesToJson
{
	internal class Constants
	{
		public const string CrLf = "\r\n";
		public const string Prefix = @"let files =
[
  {
";
		public const string Suffix = @"  }
]";
		public const string Delimiter = @"  },
  {
";
	}
	class Program
	{
		static void Main(string[] args)
		{
			var includeSubs = args.Any(a => a.Equals("/s", StringComparison.OrdinalIgnoreCase));
			var includePath = args.Any(a => a.Equals("/i", StringComparison.OrdinalIgnoreCase));
			var path = Path.GetFullPath(ReplaceEnvironmentVars(args.FirstOrDefault(a => Directory.Exists(ReplaceEnvironmentVars(a))) ?? "."));
			var excludeFile = args.FirstOrDefault(a => a.StartsWith("/x:", StringComparison.OrdinalIgnoreCase) && File.Exists(ReplaceEnvironmentVars(a.Substring(3))));
			var exclusions = new string[0];
			if (excludeFile != null)
			{
				var xpath = Path.GetDirectoryName(ReplaceEnvironmentVars(excludeFile.Substring(3)));
				if (string.IsNullOrEmpty(xpath))
					xpath = ".";
				excludeFile = Path.Combine(Path.GetFullPath(xpath), Path.GetFileName(excludeFile));
				exclusions = File.ReadAllLines(excludeFile);
			}
			if (!path.EndsWith("\\"))
				path += '\\';
			if (!Console.IsOutputRedirected)
			{
				Console.WriteLine($"Include subdirectories: {includeSubs}\r\nPath: {path}\r\nExclude File: {excludeFile ?? "none"}");
				if (excludeFile != null)
					Console.WriteLine($"Exclusions:\r\n{string.Join("\r\n", exclusions)}");
				Console.WriteLine();
			}
			var subStart = includePath ? 0 : path.Length;
			var files = Directory.GetFiles(path, "*.*", includeSubs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				.Where(f => !exclusions.Any(x => Regex.IsMatch(f, x, RegexOptions.IgnoreCase)))
				.Select(f => $@"    ""file"": ""{f.Substring(subStart).Replace('\\', '/')}"",
    ""title"": ""{Path.GetFileNameWithoutExtension(f)}""{Constants.CrLf}");
			Console.WriteLine($"{Constants.Prefix}{string.Join(Constants.Delimiter, files)}{Constants.Suffix}");
		}
		private static string ReplaceEnvironmentVars(string text) =>
			Regex.Replace(text, "%(.*?)%", (match) => Environment.GetEnvironmentVariable(match.Groups[1].Value) ?? match.Groups[0].Value);
	}
}
