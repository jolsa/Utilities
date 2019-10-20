using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DiskUsage
{
	public partial class MainForm : Form
	{
		private const int KB = 1024;

		private int _multiplier = 1;
		private List<RadioButton> _sizeRadioButtons;

		private class DriveEntry
		{
			public string Name;
			public string Display;
			public override string ToString() => Display;
		}
		private class ItemTag
		{
			public DateTime Time;
			public long TotalSpace;
			public long FreeSpace;
			public ItemTag(DriveInfo info)
			{
				TotalSpace = info.TotalSize; FreeSpace = info.TotalFreeSpace; Time = DateTime.Now;
			}
		}
		public MainForm()
		{
			InitializeComponent();
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			Text = Application.ProductName;
			_sizeRadioButtons = new List<RadioButton>() { bytesRadio, kbRadio, mbRadio, gbRadio };
			drivesCombo.DataSource = DriveInfo.GetDrives()
				.Select(d => new DriveEntry()
				{
					Name = d.Name,
					Display = $@"{d.Name.Substring(0, 2)} {(string.IsNullOrEmpty(d.VolumeLabel)
						? "" : $"({d.VolumeLabel})")}"
				}).ToList();
		}
		private void CheckButton_Click(object sender, EventArgs e) => AddDriveInfo();
		private void FormatRadio_CheckedChanged(object sender, EventArgs e)
		{
			var radio = _sizeRadioButtons.First(r => r.Checked);
			_multiplier = (int)Math.Pow(KB, _sizeRadioButtons.IndexOf(radio));

			ItemTag prevTag = null;
			entryList.Items.Cast<ListViewItem>().ToList().ForEach(item =>
			{
				var tag = (ItemTag)item.Tag;
				var data = FormatInfo(tag, prevTag);
				for (var i = 1; i < data.Length; i++) // Skip date/time column[0]
					item.SubItems[i].Text = data[i];
				prevTag = tag;
			});
			SetDiffLabel();
			SizeItems();
		}
		private void DrivesCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			entryList.Items.Clear();
			diffLabel.Visible = false;
			AddDriveInfo();
		}
		private void ContextMenu_Opening(object sender, CancelEventArgs e)
		{
			var any = entryList.Items.Count > 0;
			copyMenu.Enabled = any;
			clearMenu.Enabled = any;
		}
		private void CopyMenu_Click(object sender, EventArgs e)
		{
			var content = new[] { string.Join("\t", entryList.Columns.Cast<ColumnHeader>().Select(c => c.Text)) }
				.Concat(entryList.Items.Cast<ListViewItem>()
					.Select(item => string.Join("\t", item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(s => s.Text)))
				);
			Clipboard.SetText(string.Join("\r\n", content));
		}
		private void ClearMenu_Click(object sender, EventArgs e)
		{
			entryList.Items.Clear();
			diffLabel.Visible = false;
		}
		private void EntryList_ItemChecked(object sender, ItemCheckedEventArgs e) => SetDiffLabel();
		private void SetDiffLabel()
		{
			var checkedItems = entryList.Items.Cast<ListViewItem>().Where(item => item.Checked).ToList();
			if (checkedItems.Count != 2)
			{
				diffLabel.Visible = false;
				return;
			}

			var previous = (ItemTag)checkedItems[0].Tag;
			var current = (ItemTag)checkedItems[1].Tag;
			var value = (current.FreeSpace - previous.FreeSpace) / _multiplier;
			diffLabel.Text = $"Difference: {value:#,0}";
			diffLabel.ForeColor = value < 0 ? Color.Red : Color.Black;
			diffLabel.Visible = true;
		}
		private void AddDriveInfo()
		{
			var entry = (DriveEntry)drivesCombo.SelectedValue;
			var info = DriveInfo.GetDrives().FirstOrDefault(d => d.Name == entry.Name);
			if (info == null) return;
			var previous = entryList.Items.Cast<ListViewItem>().LastOrDefault()?.Tag as ItemTag;
			var current = new ItemTag(info);

			var item = entryList.Items.Add(new ListViewItem(FormatInfo(current, previous)) { Tag = current });

			var diffItem = item.SubItems[diffColumn.Index];
			if (diffItem.Text.StartsWith("-"))
			{
				item.UseItemStyleForSubItems = false;
				diffItem.ForeColor = Color.Red;
			}

			SizeItems();
		}
		private string[] FormatInfo(ItemTag current, ItemTag previous)
		{
			var used = current.TotalSpace - current.FreeSpace;
			var diff = previous == null ? "" : $"{(current.FreeSpace - previous.FreeSpace) / _multiplier:#,0}";

			// Total, Used, Free, Difference
			return new[]
			{
				$"{current.Time:MM/dd H:mm:ss}",
				$"{current.TotalSpace / _multiplier:#,0}",
				$"{used / _multiplier:#,0}",
				$"{current.FreeSpace / _multiplier:#,0}",
				diff
			};
		}
		private void SizeItems()
		{
			entryList.BeginUpdate();
			entryList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			entryList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			entryList.EndUpdate();
		}
	}
}