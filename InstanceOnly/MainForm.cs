using System;
using System.Linq;
using System.Windows.Forms;
using Utilities;

namespace InstanceOnly
{
	public partial class MainForm : Form
	{
		InstanceManagement _instance;
		public MainForm(InstanceManagement instance)
		{
			InitializeComponent();
			_instance = instance;
		}
		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape)
				Close();
		}
		protected override void WndProc(ref Message m)
		{
			//	Call this method. If another instance is started, this will activate the form
			_instance.WndProcHandler(m, this);
			base.WndProc(ref m);
		}
	}
}
