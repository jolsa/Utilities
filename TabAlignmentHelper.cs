using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities
{
	public static class TabAlignmentHelper
	{
		public const string CrLf = "\r\n";
		public const int DefaultTabSize = 4;
		public static string GetTabs(string text, int maxLength, int tabSize = DefaultTabSize)
		{
			//	If divisible by tabSize, add one tab
			int extraTab = 1 - Math.Sign(maxLength % tabSize);
			//	Adjust to make it divisible by tab size
			if (maxLength % tabSize > 0) maxLength += (tabSize - maxLength % tabSize);
			//	Get characters under the max length
			int under = maxLength - text.Length;
			//	Divide "under" by tab size. Add 1 if not divisible by tab size. Add 1 more if extra tab is required
			return new string('\t', under / tabSize + Math.Sign(under % tabSize) + extraTab);
		}
		public static IEnumerable<string> AlignLines(IEnumerable<string> lines, int tabSize = DefaultTabSize)
		{
			var data = lines.Select(l => Regex.Split(l, @"\t+"));
			//	Get number of "fields"
			var maxFields = data.Max(f => f.Length);
			//	Get max lengths for each field
			var maxLengths = new int[maxFields];
			for (int i = 0; i < maxFields; i++)
				maxLengths[i] = data.Max(f => f.Length > i ? f[i].Length : 0);
			return data.Select(d => string.Join("", d.Select((f, i) => $"{f}{GetTabs(f, maxLengths[i], tabSize)}")));
		}
		public static string AlignText(string text, int tabSize = DefaultTabSize, string lineDelimiter = CrLf)
		{
			return string.Join(lineDelimiter, AlignLines(text.Split(new[] { lineDelimiter }, StringSplitOptions.None), tabSize));
		}
	}
}
