using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities
{
	public static class SpecialFolderSerialization
	{
		public static IReadOnlyDictionary<string, Environment.SpecialFolder> SpecialFolderNameToValue { get; }
		public static IReadOnlyDictionary<string, Environment.SpecialFolder> PathToSpecialFolder { get; }
		public static IReadOnlyDictionary<Environment.SpecialFolder, string> SpecialFolderToPath { get; }

		static SpecialFolderSerialization()
		{
			// Get all the special folder values in the enum
			var specialFolderValues = Enum.GetValues(typeof(Environment.SpecialFolder)).Cast<Environment.SpecialFolder>()
				.Distinct()
				.ToList();

			// Get the values and physical path (don't verify, but exclude empty)
			var specialFolders = specialFolderValues
				.Select(v => new { Path = Environment.GetFolderPath(v, System.Environment.SpecialFolderOption.DoNotVerify), Type = v })
				.Where(f => !string.IsNullOrEmpty(f.Path))
				.ToList();

			// Create a dictionary of value name to value
			SpecialFolderNameToValue = specialFolderValues.ToDictionary(k => $"{k}", v => v);
			// Create a dictionary of Special Folder to physical path
			SpecialFolderToPath = specialFolders.ToDictionary(k => k.Type, v => v.Path);
			// Create a dictionary of physical path to special folder, if more than one, use the one with the shortest name
			PathToSpecialFolder = specialFolders.GroupBy(f => f.Path, StringComparer.OrdinalIgnoreCase)
				.Select(g => g.OrderBy(f => $"{f.Type}".Length).First())
				.ToDictionary(k => k.Path, v => v.Type, StringComparer.OrdinalIgnoreCase);
		}

		public static string DeserializePath(string path)
		{
			path = path.TrimEnd('\\');
			// Replace tokens with physical path
			Regex.Matches(path, @"<.*?>").Cast<Match>().Select(m => new
			{
				token = m.Value,
				value = SpecialFolderToPath[SpecialFolderNameToValue[m.Value.Trim('<', '>')]]
			}).ToList().ForEach(t => path = path.Replace(t.token, t.value));
			return path;
		}
		public static string SerializePath(string path)
		{
			path = path.TrimEnd('\\');
			// Get the first special folder that matches this path (get the longest one first)
			var sf = PathToSpecialFolder
				.OrderByDescending(p => p.Key.Length)
				.FirstOrDefault(p => path.Equals(p.Key, StringComparison.OrdinalIgnoreCase) || path.StartsWith(p.Key, StringComparison.OrdinalIgnoreCase));
			return sf.Key == null
				? path
				: Regex.Replace(path, Regex.Escape(sf.Key), $"<{sf.Value}>", RegexOptions.IgnoreCase);
		}
	}
}
