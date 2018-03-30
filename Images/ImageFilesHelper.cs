using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Utilities.Images
{
	public static class ImageFilesHelper
	{
		public static List<ImageFormat> ImageFormats =>
			typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public)
			  .Select(p => (ImageFormat)p.GetValue(null, null)).ToList();

		public static ImageFormat ImageFormatFromRawFormat(ImageFormat raw) =>
			ImageFormats.FirstOrDefault(f => raw.Equals(f)) ?? ImageFormat.Bmp;

		public static ImageFormat ImageFormatFromGuid(Guid id) =>
			ImageFormats.FirstOrDefault(f => id.Equals(f.Guid)) ?? ImageFormat.Bmp;

		public static List<string> ImageExtensions =>
			ImageCodecInfo.GetImageDecoders()
				 .SelectMany(d => d.FilenameExtension.Split(';').Select(x => x.Substring(1).Trim().ToLower())).ToList();

		public static List<string> GetImageFiles(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
			Directory.GetFiles(path, "*.*", searchOption)
				.Join(ImageExtensions, f => Path.GetExtension(f), e => e, (f, e) => f, StringComparer.OrdinalIgnoreCase).ToList();

		public static Image CropImage(Image image, Rectangle cropArea)
		{
			using (var bmp = new Bitmap(image))
				return bmp.Clone(cropArea, image.PixelFormat);
		}

		public static Image ResizeImage(Image original, decimal? percent) =>
			ResizeImage(original, percent, default(Size), false, false);

		public static Image ResizeImage(Image original, Size size, bool absolute = false, bool lesser = false) =>
			ResizeImage(original, null, size, absolute, lesser);

		private static Image ResizeImage(Image original, decimal? percent, Size size, bool absolute, bool lesser)
		{
			using (var bmpOriginal = new Bitmap(original))
			{
				int h = bmpOriginal.Height;
				int w = bmpOriginal.Width;
				if (percent.HasValue)
				{
					decimal p = percent.Value / 100m;
					h = (int)(h * p);
					w = (int)(w * p);
				}
				else
				{
					double wr = size.Width / (double)w;
					double hr = size.Height / (double)h;
					if (wr == hr || absolute)
					{
						h = size.Height;
						w = size.Width;
					}
					else
					{
						var r = lesser ? Math.Min(wr, hr) : Math.Max(wr, hr);
						h = (int)(h * r);
						w = (int)(w * r);
					}

				}
				return new Bitmap(bmpOriginal, new Size(w, h));
			}
		}
	}

}

