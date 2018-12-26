using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Utilities.Images;

namespace System.Drawing
{
	public enum Rotations
	{
		RotateNone = 0,
		Rotate90 = 1,
		Rotate180 = 2,
		Rotate270 = 3
	}
	public static class ImageExtensions
	{
		private static readonly Dictionary<Rotations, RotateFlipType> _rotationMap = new Dictionary<Rotations, RotateFlipType>()
		{
			[Rotations.RotateNone] = RotateFlipType.RotateNoneFlipNone,
			[Rotations.Rotate90] = RotateFlipType.Rotate90FlipNone,
			[Rotations.Rotate180] = RotateFlipType.Rotate180FlipNone,
			[Rotations.Rotate270] = RotateFlipType.Rotate270FlipNone
		};
		private static readonly HashSet<Rotations> _rotations = new HashSet<Rotations>(Enum.GetValues(typeof(Rotations)).Cast<Rotations>());
		public static Bitmap Rotate(this Bitmap bitmap, Rotations rotation)
		{
			if (!_rotations.Contains(rotation))
				throw new ArgumentException($"Invalid {nameof(rotation)} value of {rotation}");
			var img2 = (Image)bitmap.Clone();
			if (rotation != Rotations.RotateNone)
				img2.RotateFlip(_rotationMap[rotation]);
			return (Bitmap)img2;
		}
		public static byte[] GetBytes(this Bitmap bmp) => GetBytes(bmp, ImageFormat.Jpeg);
		public static byte[] GetBytes(this Bitmap bmp, ImageFormat format)
		{
			using (var mem = new MemoryStream())
			{
				bmp.Save(mem, format);
				return mem.GetBuffer();
			}
		}
		public static byte[] GetBytesRaw(this Bitmap bmp, ImageFormat rawFormat)
		{
			using (var mem = new MemoryStream())
			{
				bmp.Save(mem, ImageFilesHelper.ImageFormatFromRawFormat(rawFormat));
				return mem.GetBuffer();
			}
		}
		public static Bitmap ToBitmap(this byte[] data) => (Bitmap)Image.FromStream(new MemoryStream(data));

		public static ImageCodecInfo GetCodecInfo(this Image img) =>
			ImageCodecInfo.GetImageDecoders().FirstOrDefault(decoder => decoder.FormatID == img.RawFormat.Guid);

		public static ImageCodecInfo GetCodecInfo(this string file)
		{
			using (var img = Image.FromFile(file))
				return img.GetCodecInfo();
		}
		public static ImageCodecInfoPlus GetCodecInfoPlus(this Image img) => new ImageCodecInfoPlus(img);

		public static ImageCodecInfoPlus GetCodecInfoPlus(this string file) => new ImageCodecInfoPlus(file);

		public static int GetFrameCount(this Image image) => image.GetFrameCount(new FrameDimension(image.FrameDimensionsList.First()));

	}
}
