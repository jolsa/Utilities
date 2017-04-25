using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Utilities
{
    public static class ConfigHelper
    {
        public static NullDictionary<T, string> GetAppSettings<T>(params T[] notRequired)
        {
            var settings = Enum.GetValues(typeof(T)).Cast<T>().ToNullDictionary(k => k, v => ConfigurationManager.AppSettings[v.ToString()]);
            var missing = settings.Where(kv => string.IsNullOrWhiteSpace(kv.Value) && !notRequired.Contains(kv.Key)).Select(kv => kv.Key).ToList();
            if (missing.Any())
                throw new ConfigurationErrorsException($"Missing required settings: {string.Join(", ", missing)}.");
            return settings;
        }
    }
}
