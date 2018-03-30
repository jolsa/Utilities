using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Utilities.Images;

namespace System.Drawing
{
	public static class ImageExtensions
	{
		public static byte[] GetBytes(this Bitmap bmp) => GetBytes(bmp, ImageFormat.Jpeg);
		public static byte[] GetBytes(this Bitmap bmp, ImageFormat format)
		{
			using (var mem = new MemoryStream())
			{
				bmp.Save(mem, format);
				return mem.GetBuffer();
			}
		}

		public static ImageCodecInfo GetCodecInfo(this Image img) =>
			ImageCodecInfo.GetImageDecoders().FirstOrDefault(decoder => decoder.FormatID == img.RawFormat.Guid);

		public static ImageCodecInfo GetCodecInfo(this string file)
		{
			using (var img = Image.FromFile(file))
				return img.GetCodecInfo();
		}
		public static ImageCodecInfoPlus GetCodecInfoPlus(this Image img) => new ImageCodecInfoPlus(img);

		public static ImageCodecInfoPlus GetCodecInfoPlus(this string file) => new ImageCodecInfoPlus(file);

	}
}
