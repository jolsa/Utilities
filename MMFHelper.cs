using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Utilities
{
	public static class SizeConstants
	{
		public static readonly int KB = 1024;
		public static readonly int MB = (int)Math.Pow(KB, 2);
		public static readonly int GB = (int)Math.Pow(KB, 3);
	}
	public class MMFHelper : IDisposable
	{
		public static int DefaultMemoryMapFileSize = 4 * SizeConstants.KB;

		public string AppId { get;  }
		public long MemoryMapFileCapacity { get; }
		public bool IsRunning { get; private set; }
		public MemoryMappedFile MMF { get; private set; }

		private readonly uint ContentOffset = sizeof(uint);
		private readonly object _lock = new object();

		public MMFHelper(string appId) : this(appId, DefaultMemoryMapFileSize) { }
		public MMFHelper(string appId, long memoryMapSize)
		{
			AppId = appId;
			MemoryMapFileCapacity = memoryMapSize;
			CreateMMF(memoryMapSize);
		}
		private void CreateMMF(long capacity)
		{
			var security = new MemoryMappedFileSecurity();
			security.AddAccessRule(new AccessRule<MemoryMappedFileRights>(
				new SecurityIdentifier(
					WellKnownSidType.WorldSid, null),
					MemoryMappedFileRights.FullControl,
					AccessControlType.Allow));
			try
			{
				MMF = MemoryMappedFile.OpenExisting(AppId, MemoryMappedFileRights.FullControl, HandleInheritability.Inheritable);
				MMF.SetAccessControl(security);
				IsRunning = true;
			}
			catch (FileNotFoundException)
			{
				MMF = MemoryMappedFile.CreateNew(AppId, capacity, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, security, HandleInheritability.Inheritable);
				using (var accessor = MMF.CreateViewAccessor())
					accessor.Write(0, 0);
				IsRunning = false;
			}
		}

		public void WriteMMFText(string content)
		{
			lock (_lock)
			{
				using (var accessor = MMF.CreateViewAccessor())
				{

					//	Convert content to bytes
					var data = Encoding.UTF8.GetBytes(content);

					//	Write new size
					accessor.Write(0, (uint)data.Length);
					//	Write content
					accessor.WriteArray(ContentOffset, data, 0, data.Length);
				}
			}
		}

		public string GetMMFText()
		{
			using (var accessor = MMF.CreateViewAccessor())
			{
				//	Get size
				var size = accessor.ReadUInt32(0);
				//	Read bytes
				var bytes = new byte[size];
				accessor.ReadArray(ContentOffset, bytes, 0, bytes.Length);
				//	Return Data
				return Encoding.UTF8.GetString(bytes);
			}
		}

		#region IDisposable Support
		private bool _disposed;
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
					MMF.Dispose();
				_disposed = true;
			}
		}
		public void Dispose() => Dispose(true);
		#endregion

	}
}
