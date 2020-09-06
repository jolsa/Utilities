using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace SpecialFoldersTests
{
	[TestClass]
	public class SerializationTests
	{
		public List<string> RawPaths { get; }
		public List<string> SerializedPaths { get; }
		public List<(string Raw, string Serialized)> Matched { get; }

		public SerializationTests()
		{
			var profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			// Raw paths will handle user paths, but if special folders have been changed, could fail
			RawPaths = new List<string>()
			{
				@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\Okay.txt",
				$@"{profile}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Okay.txt",
				$@"{profile}\AppData\Roaming\Microsoft\Windows\Start Menu\Okay.txt",
				$@"{profile}\DocumenTs\Lyrics", // intentionally capitalized T
				$@"{profile}\DocumEnts\\", // intentionally capitalized E and added extra \\
				@"C:\Hello\\" // intentionally added extra \\
			};
			SerializedPaths = new List<string>()
			{
				@"<CommonAdminTools>\Okay.txt",
				@"<Programs>\Okay.txt",
				@"<StartMenu>\Okay.txt",
				@"<MyDocuments>\Lyrics",
				@"<MyDocuments>",
				@"C:\Hello"
			};
			Matched = RawPaths.Zip(SerializedPaths, (raw, serialized) => ( raw, serialized )).ToList();
		}

		[TestMethod]
		public void Serialize()
		{
			Matched.ForEach(m =>
			{
				var actual = SpecialFolderSerialization.SerializePath(m.Raw);
				if (Debugger.IsAttached && actual != m.Serialized)
					Debugger.Break();
				Assert.AreEqual(actual, m.Serialized, $"{actual} should match {m.Serialized}");
			});
		}
		[TestMethod]
		public void Deserialize()
		{
			Matched.ForEach(m =>
			{
				var actual = SpecialFolderSerialization.DeserializePath(m.Serialized);
				var expected = m.Raw.TrimEnd('\\');
				if (Debugger.IsAttached && !actual.Equals( expected, StringComparison.OrdinalIgnoreCase ))
					Debugger.Break();
				Assert.AreEqual(actual, expected, ignoreCase: true, $"{actual} should match {expected}");
			});
		}
	}
}
