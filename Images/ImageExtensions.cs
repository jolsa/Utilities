using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace System.Drawing
{
	public static class ImageExtensions
	{
		public static byte[] GetBytes(this Bitmap bmp)
		{
			using (var mem = new MemoryStream())
			{
				bmp.Save(mem, ImageFormat.Jpeg);
				return mem.GetBuffer();
			}
		}
	}
}
