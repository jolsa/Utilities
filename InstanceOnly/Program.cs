using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace InstanceOnly
{
	static class Program
	{
		private const string appId = "[InstanceOnly]:20273528-5c4b-4d32-b683-6acc98e1fbd3";
		[STAThread]
		static void Main()
		{
			var instance = new InstanceManagement(appId);
			if (instance.IsRunning)
			{
				instance.Activate();
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(instance));
		}
	}
}
