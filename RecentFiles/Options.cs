using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RecentFiles
{
	public partial class Options : Form
	{
		public bool IncludeSubfolders { get; private set; }
		public List<string> Extensions { get; private set; }
		public bool SearchContent { get; private set; }
		public Options(bool includeSubfolders, List<string> extensions, bool searchContent)
		{
			InitializeComponent();
			IncludeSubfolders = includeSubfolders;
			Extensions = extensions;
			SearchContent = searchContent;
			DialogResult = DialogResult.Cancel;
		}
		private void Options_Load(object sender, EventArgs e)
		{
			checkIncludeSubs.Checked = IncludeSubfolders;
			textExtensions.Text = string.Join(", ", Extensions);
			checkSearchContent.Checked = SearchContent;
		}
		private void Options_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape)
				Close();
		}
		private void CloseButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Close();
		private void ApplyButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult = DialogResult.OK;
			IncludeSubfolders = checkIncludeSubs.Checked;
			Extensions = textExtensions.Text.Split(' ', ',', ';').Where(x => !string.IsNullOrWhiteSpace(x))
				.Select(x => Regex.Match('.' + x.Trim(), @".*(\..*)").Groups[1].Value).ToList();
			SearchContent = checkSearchContent.Checked;
		}
	}
}
