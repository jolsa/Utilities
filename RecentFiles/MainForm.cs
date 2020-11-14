using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RecentFiles
{
	public partial class MainForm : Form
	{
		private enum EventChanges
		{
			Attach,
			Detach
		}
		private class FileData
		{
			public bool ContentMatch;
		}
		private static readonly string CrLf = Environment.NewLine;
		private Dictionary<string, FileData> _searchResults = new Dictionary<string, FileData>(StringComparer.OrdinalIgnoreCase);
		private readonly Color _contentColor = Color.IndianRed;
		private Regex _search;
		private readonly List<EventManager> _eventManager;
		private bool EventsAttached { get; set; } = true;

		public MainForm()
		{
			InitializeComponent();
			_eventManager = new List<EventManager>()
			{
				new EventManager(h => checkOldestFirst.CheckedChanged += h, h => checkOldestFirst.CheckedChanged -= h, CheckOldestFirst_CheckedChanged ),
				new EventManager(h => settingSelection.SelectedIndexChanged += h, h => settingSelection.SelectedIndexChanged -= h, SettingSelection_SelectedIndexChanged )
			};
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			ChangeEvents(EventChanges.Detach);
			var screen = Screen.FromControl(this);
			var w = screen.WorkingArea.Width;
			var h = screen.WorkingArea.Height;
			var w80 = (int)(w * 0.8);
			var h80 = (int)(h * 0.8);

			SetBounds((w - w80) / 2, (h - h80) / 2, w80, h80);

			settingSelection.Items.AddRange(SettingsManager.Settings.Configurations.OrderByDescending(c => c.LastUsed).ToArray());
			settingSelection.DisplayMember = "Path";
			settingSelection.SelectedIndex = 0;

			GetFiles();
			ChangeEvents(EventChanges.Attach);
		}
		private void ChangeEvents(EventChanges changeType)
		{
			var change = (changeType == EventChanges.Attach && !EventsAttached) || (changeType == EventChanges.Detach && EventsAttached);
			if (change)
				_eventManager.ForEach(e =>
				{
					if (changeType == EventChanges.Detach)
						e.Detach();
					else
						e.Attach();
				});
			EventsAttached = changeType == EventChanges.Attach;
		}
		private (string name, DateTime date) ToEntry(string filename) => (filename, File.GetLastWriteTime(filename));
		private List<(string name, DateTime date)> _data;

		private void GetFiles()
		{
			var setting = SettingsManager.Settings.LastConfig;
			ChangeEvents(EventChanges.Detach);
			checkOldestFirst.Checked = false;
			ChangeEvents(EventChanges.Attach);
			detailLabel.Text = $"{(setting.SearchOption == SearchOption.AllDirectories ? "Include Subfolders" : "Top Folder")}; Types: {string.Join(";", setting.Extensions)}; Content Search: {setting.SearchContent}";
			_data = Directory.GetFiles(setting.Path, setting.SearchPattern, setting.SearchOption)
				.Where(setting.ExtensionMatches)
				.Select(ToEntry)
				.OrderByDescending(f => f.date)
				.ToList();
			LoadItems();
		}
		private void TryRegex()
		{
			var pattern = searchText.Text;
			if (string.IsNullOrEmpty(pattern))
			{
				_search = null;
				return;
			}
			try
			{
				_search = new Regex(pattern, RegexOptions.IgnoreCase);
				searchText.ForeColor = Color.DarkGreen;
			}
			catch
			{
				_search = new Regex(Regex.Escape(pattern), RegexOptions.IgnoreCase);
				searchText.ForeColor = Color.Black;
			}
		}
		private void LoadItems()
		{
			Cursor = Cursors.WaitCursor;
			var empty = string.IsNullOrWhiteSpace(searchText.Text);
			if (!empty)
				TryRegex();
			content.Items.Clear();
			if (!empty)
				_searchResults = GetSearchResults();
			else
				_searchResults.Clear();
			_data.Where(d => empty || _searchResults.ContainsKey(d.name)).ToList().ForEach(AddItem);
			content.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			labelCount.Text = $"{content.Items.Count:#,0} item(s)" + (empty ? "" : " (filtered)");
			labelCount.ForeColor = empty ? ForeColor : closeButton.LinkColor;
			Cursor = Cursors.Default;
		}
		private Dictionary<string, FileData> GetSearchResults()
		{
			var searchContent = SettingsManager.Settings.LastConfig.SearchContent;
			var nameMatch = false;
			return _data.Where(f => (nameMatch = _search.IsMatch(f.name)) || (searchContent && _search.IsMatch(File.ReadAllText(f.name))))
				.ToDictionary(k => k.name, v => new FileData() { ContentMatch = !nameMatch });
		}
		private void AddItem((string name, DateTime date) item)
		{

			content.Items.Add(new ListViewItem(new[] { item.name.Substring(SettingsManager.Settings.LastConfig.Path.Length).TrimStart('\\'), $"{item.date:yyyy-MM-dd HH:mm}" })
			{
				ForeColor = (_search?.IsMatch(item.name) ?? true) ? Color.White : _contentColor,
				Tag = item.name
			});
		}
		private void OpenItem()
		{
			if (content.SelectedItems.Count == 1)
			{
				var file = (string)content.SelectedItems[0].Tag;
				using (var p = new Process())
				{
					p.StartInfo.FileName = file;
					p.Start();
				}
			}
		}
		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape)
				Close();
		}
		private void Content_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				OpenItem();
			}
		}
		private void SearchText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				LoadItems();
			}
		}
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			bool handle = true;
			if (e.KeyCode == Keys.F5)
			{
				GetFiles();
			}
			else
				handle = false;
			e.SuppressKeyPress = handle;
			e.Handled = handle;
		}
		private void CheckOldestFirst_CheckedChanged(object sender, EventArgs e)
		{
			if (checkOldestFirst.Checked)
				_data = _data.OrderBy(d => d.date).ToList();
			else
				_data = _data.OrderByDescending(d => d.date).ToList();
			LoadItems();
		}
		private void Content_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				OpenItem();
		}
		private void SearchText_TextChanged(object sender, EventArgs e)
		{
			TryRegex();
		}
		private void MenuCopy_Click(object sender, EventArgs e)
		{
			if (content.SelectedItems.Count == 1)
				Clipboard.SetText((string)content.SelectedItems[0].Tag);
		}
		private void MenuDelete_Click(object sender, EventArgs e)
		{
			if (content.SelectedItems.Count == 0) return;
			var item = content.SelectedItems[0];
			var file = (string)item.Tag;
			if (MessageBox.Show(this, $@"Delete ""{file}""", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				File.Delete(file);
				item.Remove();
			}
		}
		private void CloseButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Close();
		private void MenuExplore_Click(object sender, EventArgs e)
		{
			if (content.SelectedItems.Count == 0) return;
			var item = content.SelectedItems[0];
			var file = (string)item.Tag;
			if (File.Exists(file))
				Process.Start("explorer.exe", $@"/select, ""{file}""");
		}
		private void LabelCount_Click(object sender, EventArgs e)
		{
			if (labelCount.Text.IndexOf("filter", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				searchText.Text = null;
				LoadItems();
			}
		}
		private void LabelCount_MouseHover(object sender, EventArgs e)
		{
			var filtered = labelCount.Text.IndexOf("filter", StringComparison.OrdinalIgnoreCase) >= 0;
			toolTipMain.SetToolTip(labelCount, filtered ? "Clear Filter" : null);
			labelCount.Cursor = filtered ? Cursors.Hand : Cursor.Current;
		}
		private void MainForm_Shown(object sender, EventArgs e) => searchText.Focus();
		private void SettingSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChangeEvents(EventChanges.Detach);
			var config = settingSelection.SelectedItem as SettingsManager.AppSettings.Config;
			SettingsManager.UpdateConfig(config.Path, config.SearchOption, config.Extensions, config.SearchContent);
			if (settingSelection.Items.IndexOf(config) != 0)
			{
				settingSelection.Items.Remove(config);
				settingSelection.Items.Insert(0, SettingsManager.Settings.LastConfig);
				settingSelection.SelectedIndex = 0;
			}
			GetFiles();
			ChangeEvents(EventChanges.Attach);
		}
		private void SettingSelection_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				var path = settingSelection.Text.TrimEnd('\\');
				if (!Directory.Exists(path))
				{
					MessageBox.Show(this, $@"Can't find path ""{path}""", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				var current = SettingsManager.Settings.LastConfig;
				var isNew = SettingsManager.UpdateConfig(path, current.SearchOption, current.Extensions, current.SearchContent);
				if (isNew)
					settingSelection.Items.Insert(0, SettingsManager.Settings.LastConfig);
				GetFiles();
			}
		}
		private void MenuEdit_Click(object sender, EventArgs e)
		{
			if (content.SelectedItems.Count == 1)
			{
				var file = (string)content.SelectedItems[0].Tag;
				using (var p = new Process())
				{
					p.StartInfo.FileName = SettingsManager.Settings.Editor ?? "NOTEPAD";
					p.StartInfo.Arguments = $@"""{file}""";
					try
					{
						p.Start();
					}
					catch (Exception ex)
					{
						var message = ex.Message;
						var editor = SettingsManager.Settings.Editor;
						if (!string.IsNullOrWhiteSpace(editor) && !File.Exists(editor))
							message += $@"{CrLf}{CrLf}Possibly because ""{editor}"" could not be found.{CrLf}{CrLf}Check Editor setting in ""{SettingsManager.SettingsFile}"".";
						MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}
		private void DetailLabel_Click(object sender, EventArgs e)
		{
			var config = SettingsManager.Settings.LastConfig;
			using var options = new Options(includeSubfolders: config.SearchOption == SearchOption.AllDirectories, config.Extensions, config.SearchContent);
			if (options.ShowDialog(this) == DialogResult.OK)
			{
				var path = settingSelection.Text.TrimEnd('\\');
				SettingsManager.UpdateConfig(path,
					searchOption: options.IncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly,
					options.Extensions, options.SearchContent);
				GetFiles();
			}
		}
	}
}
