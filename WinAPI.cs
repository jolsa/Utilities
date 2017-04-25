using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
	internal static class WinAPI
	{
		public static readonly IntPtr HWND_BROADCAST = (IntPtr)0xffff;
		[DllImport("user32")]
		public static extern ushort GetAsyncKeyState(Keys key);
		[DllImport("user32")]
		public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);
	}
}
