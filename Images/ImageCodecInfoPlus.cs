using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Utilities.Images
{
	public class ImageCodecInfoPlus : IDisposable
	{
		public ImageCodecInfo ImageCodecInfo { get; private set; }
		public Image Image { get; private set; }
		//	Get the first extension used by the codec
		public string PrimaryExtension => ImageCodecInfo.FilenameExtension.Split(';').First().Substring(1).ToLower();
		public ImageFormat Format => ImageFilesHelper.ImageFormatFromGuid(ImageCodecInfo.FormatID);

		private bool _fromFile;

		public ImageCodecInfoPlus(string file) : this(Image.FromFile(file)) { _fromFile = true; }
		public ImageCodecInfoPlus(Image image)
		{
			Image = image;
			ImageCodecInfo = image.GetCodecInfo();
		}
		public void Dispose()
		{
			//	Only dispose of the image if it came from a file (we created it)
			if (!_fromFile) return;
			if (Image != null)
			{
				Image.Dispose();
				Image = null;
				GC.SuppressFinalize(this);
			}
		}
	}
}
