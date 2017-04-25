using System;
using System.Linq;
using System.Windows.Forms;

namespace Utilities
{
	public static class Windows
	{
		public static bool IsKeyDown(Keys key)
		{
			const ushort MSB = 0x8000;
			return (WinAPI.GetAsyncKeyState(key) & MSB) == MSB;
		}

	}
}
