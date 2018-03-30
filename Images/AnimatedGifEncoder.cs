using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Utilities.Images
{
	public class AnimatedGifEncoder
	{
		private const string EncoderVersionInfo = "GIF89a";
		private const int DefaultFrameRate = 200;

		private List<Bitmap> _frames;

		public List<int> FrameRates { get; set; }

		/// <summary>
		/// Return the GIF specification version.
		/// </summary>
		public string EncoderVersion => EncoderVersionInfo;

		/// <summary>
		/// Get or set a value that indicate if the GIF will repeat the animation after the last frame is shown. The default value is True
		/// </summary>
		public bool Repeat { get; set; }

		/// <summary>
		/// Get or set a collection of metadata string to be embedded in the GIF file. Each string has a max length of 254 
		/// characters (Any character above this limit will be truncated). The string will be encoded UTF-7. 
		/// </summary>
		public List<string> Metadata { get; set; }

		/// <summary>
		///  Get or set the amount of time each frame will be shown (in milliseconds). The default value is 200ms
		/// </summary>
		public int FrameRate { get; set; }

		public AnimatedGifEncoder()
		{
			Repeat = true;
			Metadata = new List<string>();
			FrameRate = DefaultFrameRate;
			_frames = new List<Bitmap>();
		}

		public AnimatedGifEncoder(IEnumerable<Bitmap> frames)
			: this()
		{
			if (frames != null && frames.Any())
				AddFrameRange(frames);
		}

		public void AddFrame(Bitmap frame) =>			_frames.Add(frame);

		public void AddFrameRange(IEnumerable<Bitmap> frames) =>			_frames.AddRange(frames);

		public void Save(Stream outputStream)
		{
			//	Make sure we have frames
			if (!_frames.Any())
				throw new InvalidDataException("Frames must be supplied.");

			//	Make sure they all have value, height and width
			if (_frames.Any(f => f == null || f.Height == 0 || f.Width == 0))
				throw new InvalidDataException("Bitmap frames cannot be zero (height or width).");

			//	Add frames to the encoder
			var gifEncoder = new GifBitmapEncoder();
			_frames.ForEach(frame =>
				gifEncoder.Frames.Add(
					BitmapFrame.Create(
						Imaging.CreateBitmapSourceFromHBitmap(frame.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
						)
					)
				);

			byte[] bytes;
			using (var ms = new MemoryStream())
			{
				gifEncoder.Save(ms);
				bytes = ms.ToArray();
			}

			//	Locate the right location where to insert the metadata in the binary
			//	This will be just before the first label &H0021F9 (Graphic Control Extension)
			int metadataPtr = 0;
			bool located = false;
			do
			{
				metadataPtr++;
				if (bytes[metadataPtr] == 0 && bytes[metadataPtr + 1] == 0x21 && bytes[metadataPtr + 2] == 0xf9)
					located = true;
			} while (!located);

			//	SET METADATA Repeat
			//	This add an Application Extension Netscape2.0
			if (Repeat)
			{
				byte[] temp = new byte[bytes.Length - 1 + 20];
				//	label: &H21, &HFF + one byte: length(&HB) + NETSCAPE2.0 + one byte: Datalength(&H3) + {1, 0, 0} + Block terminator, 1 byte, &H00
				byte[] appExtension = { 0x21, 0xff, 0xb, 0x4e, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45, 0x32, 0x2e, 0x30, 0x3, 0x1, 0x0, 0x0, 0x0 };
				Array.Copy(bytes, temp, metadataPtr);
				Array.Copy(appExtension, 0, temp, metadataPtr + 1, 19);
				Array.Copy(bytes, metadataPtr + 1, temp, metadataPtr + 20, bytes.Length - metadataPtr - 1);
				bytes = temp;
			}

			//	SET METADATA Comments
			//	This add a Comment Extension for each string
			if (Metadata.Any())
			{
				foreach (string comment in Metadata)
				{
					if (!string.IsNullOrEmpty(comment))
					{
						string theComment = null;
						if (comment.Length > 254)
							theComment = comment.Substring(0, 254);
						else
							theComment = comment;
						var commentBytes = UTF7Encoding.UTF7.GetBytes(theComment);
						var commentData = new byte[] { 0x21, 0xfe, (byte)commentBytes.Length }.Concat(commentBytes).Concat(new byte[] { 0x0 }).ToArray();
						var temp = new byte[bytes.Length - 1 + commentData.Length + 1];
						Array.Copy(bytes, temp, metadataPtr);
						Array.Copy(commentData, 0, temp, metadataPtr + 1, commentData.Length);
						Array.Copy(bytes, metadataPtr + 1, temp, metadataPtr + commentData.Length + 1, bytes.Length - metadataPtr - 1);
						bytes = temp;
					}
				}
			}

			//	SET METADATA frameRate
			//	Sets the third and fourth byte of each Graphic Control Extension (5 bytes from each label 0x0021F9)
			//	word, little endian, the hundredths of second to show this frame
			if (FrameRates == null)
				FrameRates = new List<int>();

			if (FrameRates.Count < _frames.Count)
				FrameRates.AddRange(Enumerable.Repeat<int>(FrameRate, _frames.Count - FrameRates.Count));

			int frameId = 0;
			for (int x = 0; x <= bytes.Length - 1; x++)
			{
				if (bytes[x] == 0 && bytes[x + 1] == 0x21 && bytes[x + 2] == 0xf9 && bytes[x + 3] == 4)
				{
					var fr = BitConverter.GetBytes(FrameRates[frameId++] / 10);
					bytes[x + 5] = fr[0];
					bytes[x + 6] = fr[1];
				}
			}
			outputStream.Write(bytes, 0, bytes.Length);
		}
		/// <summary>
		/// Saves encoded GIF to Image with MemoryStream in the .Tag property
		/// </summary>
		/// <returns>Image</returns>
		public Image ToImage()
		{
			var ms = new MemoryStream();
			Save(ms);
			return new Bitmap(ms) { Tag = ms };
		}
	}
}
