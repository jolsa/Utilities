using System;
using System.Linq;
using System.Windows.Forms;
using Utilities;

namespace SingleTonTest
{
	static class Program
	{
		private const string appId = "[SingleTonTestApp]:20273528-5c4b-4d32-b683-6acc98e1fbd3";
		private const int MB = 0x100000;
		private const long MaxMem = 10 * MB;

		[STAThread]
		static void Main(string[] args)
		{
			if (!args.Any())
				args = new[] { $"{DateTime.Now:HH:mm:ss}" };

			var sam = Windows.IsKeyDown(Keys.ShiftKey) ? null : new SingleTonAppManager(appId, args, MaxMem);
			if (sam != null && sam.IsRunning)
			{
				sam.ActivateRunningApp();
				sam.Dispose();
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(sam, args));

		}
	}
}
