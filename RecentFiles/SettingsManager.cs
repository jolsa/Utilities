using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilities;

namespace RecentFiles
{
	public static class SettingsManager
	{
		private const int MaxConfigs = 20;
		static SettingsManager() => ReadSettings();
		public static string ApplicationPath { get; } = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
		public static string SettingsFile { get; } = Path.Combine(ApplicationPath, "appSettings.json");
		private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore };
		public static AppSettings Settings { get; private set; }
		public static void ReadSettings()
		{
			// If there's no file create one
			if (!File.Exists(SettingsFile))
				SaveSettings();

			Settings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(SettingsFile));
			// If settings are empty, set to default and save
			if (Settings.Configurations?.Any() != true)
			{
				Settings = DefaultSettings;
				SaveSettings();
			}
		}
		public static void SaveSettings() =>
			File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(Settings ?? DefaultSettings, JsonSettings), Encoding.UTF8);
		public static bool UpdateConfig(string path, SearchOption searchOption, IEnumerable<string> extensions, bool searchContent)
		{
			var config = Settings.Configurations.FirstOrDefault(c => c.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
			var newConfig = config == null;
			if (newConfig)
				config = new AppSettings.Config() { Path = path };

			config.SearchOption = searchOption;
			config.Extensions = extensions.ToList();
			config.SearchContent = searchContent;
			config.LastUsed = DateTime.Now;

			// If new, add and sort
			if (newConfig)
				Settings.Configurations = Settings.Configurations.OrderByDescending(c => c.LastUsed).Take(MaxConfigs - 1).Concat(new[] { config }).ToList();

			SaveSettings();
			return newConfig;
		}
		private static AppSettings DefaultSettings => new AppSettings()
		{
			Configurations = new List<AppSettings.Config>()
			{
				new AppSettings.Config()
				{
					Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
					SearchOption = SearchOption.TopDirectoryOnly,
					SearchContent = true,
					Extensions = new List<string>() { ".txt" },
					LastUsed = DateTime.Now
				}
			}
		};
		public class AppSettings
		{
			public class Config
			{
				[JsonConverter(typeof(PathConverter))]
				public string Path { get; set; }
				public SearchOption SearchOption { get; set; }
				public List<string> Extensions { get; set; }
				public bool SearchContent { get; set; }
				public DateTime LastUsed { get; set; }
				[JsonIgnore]
				public string SearchPattern => Extensions?.Any() != true || Extensions.Count > 1 ? "*.*" : $"*{Extensions.First()}";
				public override string ToString() => $"{Path}: {string.Join(";", Extensions)}";
				public bool ExtensionMatches(string filename) =>
					Extensions?.Any() != true || Extensions.Contains(System.IO.Path.GetExtension(filename), StringComparer.OrdinalIgnoreCase);
			}
			[JsonConverter(typeof(PathConverter))]
			public string Editor { get; set; }
			public List<Config> Configurations { get; set; }
			[JsonIgnore]
			public Config LastConfig => Configurations?.OrderBy(c => c.LastUsed).Last();
		}
		public class PathConverter : JsonConverter<string>
		{
			public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer) =>
				writer.WriteValue(SpecialFolderSerialization.SerializePath(value));

			public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer) =>
				SpecialFolderSerialization.DeserializePath((string)reader.Value);
		}
	}
}
