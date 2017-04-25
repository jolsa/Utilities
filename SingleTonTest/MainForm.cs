using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utilities;

namespace SingleTonTest
{
	public partial class MainForm : Form
	{
		private int _reads;
		private SingleTonAppManager _sam;

		public MainForm(SingleTonAppManager sam, string[] args)
		{
			InitializeComponent();
			_sam = sam;
			AddItems(args);
		}
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			_sam.Dispose();
		}
		private void OnItemsAdded(List<SingleTonAppManager.MappedItem> allItems)
		{
			AddItems(allItems.Where(item => item.WriteSequence > _reads).SelectMany(item => item.Text));
		}
		protected override void WndProc(ref Message m)
		{
			_sam.WndProcHandler(m, this, OnItemsAdded);
			base.WndProc(ref m);
		}
		private void AddItems(IEnumerable<string> items)
		{
			listBox1.Items.AddRange(items.ToArray());
			_reads++;
		}
		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape)
				Close();
		}
	}
}
