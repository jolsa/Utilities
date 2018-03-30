using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Utilities.Images
{
	public class AnimatedGifDecoder : IDisposable
	{
		private Stream _preStream;
		private Stream _stream;
		private Image _image;
		private FrameDimension _frameDimension;
		private bool _imageCreated;

		public List<Bitmap> Frames { get; private set; }
		public int FrameCount { get; private set; }
		public List<int> FrameRates { get; private set; }
		public List<string> MetaData { get; private set; }

		//	Constructors need to call this
		private void Initialize()
		{
			var gifDecoder = new GifBitmapDecoder(_stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
			FrameCount = gifDecoder.Frames.Count;
			MetaData = gifDecoder.Metadata.ToList();
			_frameDimension = new FrameDimension(_image.FrameDimensionsList.First());
			Frames = GetFrames(_image, FrameCount, _frameDimension);
			FrameRates = GetFrameRates(_image, FrameCount);
		}

		public AnimatedGifDecoder(byte[] bytes)
		{
			_preStream = new MemoryStream(bytes);
			_image = Image.FromStream(_preStream);
			_stream = new MemoryStream();
			_image.Save(_stream, ImageFormat.Gif);
			Initialize();
		}

		public AnimatedGifDecoder(Image image)
		{
			_image = image;
			_stream = new MemoryStream();
			image.Save(_stream, ImageFormat.Gif);
			Initialize();
		}
		public AnimatedGifDecoder(string file)
		{
			_image = Image.FromFile(file);
			_imageCreated = true;
			_stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			Initialize();
		}

		~AnimatedGifDecoder()
		{
			Dispose(false);
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_preStream != null)
					_preStream.Dispose();
				_stream.Dispose();
				if (_imageCreated)
					_image.Dispose();
			}
		}

		private static List<int> GetFrameRates(Image image, int frames) =>
			Enumerable.Range(0, frames).Select(f => BitConverter.ToInt32(image.GetPropertyItem(0x5100).Value, 4 * f) * 10).ToList();

		public static List<int> GetFrameRates(Image image) =>
			GetFrameRates(image, image.GetFrameCount(new FrameDimension(image.FrameDimensionsList.First())));

		private static List<Bitmap> GetFrames(Image image, int frames, FrameDimension dimension) =>
			Enumerable.Range(0, frames).Select(f =>
			{
				image.SelectActiveFrame(dimension, f);
				return new Bitmap(image);
			}).ToList();

		public static List<Bitmap> GetFrames(Image image)
		{
			var dim = new FrameDimension(image.FrameDimensionsList.First());
			return GetFrames(image, image.GetFrameCount(dim), dim);
		}

		public static bool IsAnimated(Image image) =>
			image.GetFrameCount(new FrameDimension(image.FrameDimensionsList.First())) > 1;

	}
}
