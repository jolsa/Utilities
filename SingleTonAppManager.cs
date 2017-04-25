using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Utilities
{
	/// <summary>
	/// Manange a singleton app with shared data using Memory Mapped File
	/// </summary>
	public class SingleTonAppManager : IDisposable
	{
		private const string AttributeDateFormat = "yyyyMMddHHmmssffffff";
		private const string MapDelimiter = "<\0>";
		private const int WriteOffset = 0;
		private const int ContentOffset = sizeof(int); // Using first 4 bytes to store # of writes
		private const int KB = 0x400;
		private const long DefaultMem = 4 * KB;

		private const string TagEntry = "e";
		private const string TagSeq = "q";
		private const string TagTime = "t";
		private const string TagContent = "c";

		private MemoryMappedFile _mmf;
		private object _lock = new object();
		private string _appId;
		private int _writes;

		private int WM_SHOW_APP;

		public class MappedItem
		{
			public int WriteSequence { get; private set; }
			public DateTime Added { get; private set; }
			public List<string> Text { get; private set; }
			public static MappedItem FromXElement(XElement element)
			{
				return new MappedItem()
				{
					WriteSequence = int.Parse(element.Attribute(TagSeq).Value),
					Added = DateTime.ParseExact(element.Attribute(TagTime).Value, AttributeDateFormat, CultureInfo.CurrentCulture),
					Text = element.Elements(TagContent).Select(e => e.Value).ToList()
				};
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="appId">Presumably unique application id</param>
		/// <param name="args">Application Arguments</param>
		/// <param name="memoryMapSize">Size (in bytes) of memory mapped file</param>
		public SingleTonAppManager(string appId, IEnumerable<string> args, long memoryMapSize = DefaultMem)
		{
			_appId = appId;
			WM_SHOW_APP = WinAPI.RegisterWindowMessage(appId);
			CreateMMF(memoryMapSize);
			AppendToMMF(args.ToArray());
		}

		public bool IsRunning { get; private set; }

		public void WndProcHandler(Message m, Form theForm, Action<List<MappedItem>> getMappedItems = null)
		{
			if (m.Msg == WM_SHOW_APP)
			{
				InstanceManagement.SetForeground(theForm);
				getMappedItems?.Invoke(GetMMFData());
			}
		}

		public void ActivateRunningApp()
		{
			WinAPI.PostMessage(WinAPI.HWND_BROADCAST, WM_SHOW_APP, IntPtr.Zero, IntPtr.Zero); 
		}

		private void CreateMMF(long capacity)
		{
			try
			{
				_mmf = MemoryMappedFile.OpenExisting(_appId);
				IsRunning = true;
			}
			catch (FileNotFoundException)
			{
				_mmf = MemoryMappedFile.CreateNew(_appId, capacity);
				using (var accessor = _mmf.CreateViewAccessor())
					accessor.Write(ContentOffset, 0);
				IsRunning = false;
			}
		}

		private void AppendToMMF(params string[] content)
		{
			lock (_lock)
			{
				uint size, pos;
				using (var accessor = _mmf.CreateViewAccessor())
				{
					//	Increment Writes and Save
					_writes = accessor.ReadInt32(WriteOffset) + 1;
					accessor.Write(WriteOffset, _writes);

					//	Format text as minimal XML
					string text = new XElement(TagEntry,
						new XAttribute(TagSeq, _writes),
						new XAttribute(TagTime, DateTime.Now.ToString(AttributeDateFormat)),
						content.Select(c => new XElement(TagContent, c))
						).ToString();

					//	Convert delimiter and content to bytes
					var delim = Encoding.UTF8.GetBytes(MapDelimiter);
					var data = Encoding.UTF8.GetBytes(text);

					//	Get current size
					size = accessor.ReadUInt32(ContentOffset);
					//	Write new size
					accessor.Write(ContentOffset, size + (uint)delim.Length + (uint)data.Length);
					//	Write delimiter
					pos = ContentOffset + size + sizeof(uint);
					accessor.WriteArray(pos, delim, 0, delim.Length);
					//	Write content
					pos += (uint)delim.Length;
					accessor.WriteArray(pos, data, 0, data.Length);
				}
			}
		}

		public List<MappedItem> GetMMFData()
		{
			using (var accessor = _mmf.CreateViewAccessor())
			{
				//	Get size
				var size = accessor.ReadUInt32(ContentOffset);
				//	Read bytes
				var bytes = new byte[size];
				accessor.ReadArray(ContentOffset + sizeof(uint), bytes, 0, bytes.Length);
				//	Parse data
				return Encoding.UTF8.GetString(bytes).Split(new[] { MapDelimiter }, StringSplitOptions.None).Skip(1)
					.Select(t => MappedItem.FromXElement(XElement.Parse(t)))
					.ToList();
			}
		}

		#region IDisposable Support
		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
					_mmf.Dispose();
				_disposed = true;
			}
		}
		public void Dispose() => Dispose(true);
		#endregion

	}
}
