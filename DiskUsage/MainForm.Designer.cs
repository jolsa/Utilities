namespace DiskUsage
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.clearMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.PanelBottom = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mbRadio = new System.Windows.Forms.RadioButton();
			this.gbRadio = new System.Windows.Forms.RadioButton();
			this.kbRadio = new System.Windows.Forms.RadioButton();
			this.bytesRadio = new System.Windows.Forms.RadioButton();
			this.checkButton = new System.Windows.Forms.Button();
			this.PanelTop = new System.Windows.Forms.Panel();
			this.diffLabel = new System.Windows.Forms.Label();
			this.drivesCombo = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.entryList = new System.Windows.Forms.ListView();
			this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.diffColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip1.SuspendLayout();
			this.PanelBottom.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.PanelTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenu,
            this.clearMenu});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(146, 80);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
			// 
			// copyMenu
			// 
			this.copyMenu.Name = "copyMenu";
			this.copyMenu.Size = new System.Drawing.Size(145, 38);
			this.copyMenu.Text = "&Copy";
			this.copyMenu.Click += new System.EventHandler(this.CopyMenu_Click);
			// 
			// clearMenu
			// 
			this.clearMenu.Name = "clearMenu";
			this.clearMenu.Size = new System.Drawing.Size(145, 38);
			this.clearMenu.Text = "&Clear";
			this.clearMenu.Click += new System.EventHandler(this.ClearMenu_Click);
			// 
			// PanelBottom
			// 
			this.PanelBottom.Controls.Add(this.groupBox1);
			this.PanelBottom.Controls.Add(this.checkButton);
			this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PanelBottom.Location = new System.Drawing.Point(0, 410);
			this.PanelBottom.Name = "PanelBottom";
			this.PanelBottom.Size = new System.Drawing.Size(1243, 114);
			this.PanelBottom.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mbRadio);
			this.groupBox1.Controls.Add(this.gbRadio);
			this.groupBox1.Controls.Add(this.kbRadio);
			this.groupBox1.Controls.Add(this.bytesRadio);
			this.groupBox1.Location = new System.Drawing.Point(201, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416, 90);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// mbRadio
			// 
			this.mbRadio.AutoSize = true;
			this.mbRadio.Location = new System.Drawing.Point(230, 30);
			this.mbRadio.Name = "mbRadio";
			this.mbRadio.Size = new System.Drawing.Size(75, 29);
			this.mbRadio.TabIndex = 9;
			this.mbRadio.Text = "&MB";
			this.mbRadio.UseVisualStyleBackColor = true;
			this.mbRadio.Click += new System.EventHandler(this.FormatRadio_CheckedChanged);
			// 
			// gbRadio
			// 
			this.gbRadio.AutoSize = true;
			this.gbRadio.Location = new System.Drawing.Point(325, 30);
			this.gbRadio.Name = "gbRadio";
			this.gbRadio.Size = new System.Drawing.Size(73, 29);
			this.gbRadio.TabIndex = 10;
			this.gbRadio.Text = "&GB";
			this.gbRadio.UseVisualStyleBackColor = true;
			this.gbRadio.Click += new System.EventHandler(this.FormatRadio_CheckedChanged);
			// 
			// kbRadio
			// 
			this.kbRadio.AutoSize = true;
			this.kbRadio.Location = new System.Drawing.Point(137, 30);
			this.kbRadio.Name = "kbRadio";
			this.kbRadio.Size = new System.Drawing.Size(71, 29);
			this.kbRadio.TabIndex = 8;
			this.kbRadio.Text = "&KB";
			this.kbRadio.UseVisualStyleBackColor = true;
			this.kbRadio.Click += new System.EventHandler(this.FormatRadio_CheckedChanged);
			// 
			// bytesRadio
			// 
			this.bytesRadio.AutoSize = true;
			this.bytesRadio.Checked = true;
			this.bytesRadio.Location = new System.Drawing.Point(18, 30);
			this.bytesRadio.Name = "bytesRadio";
			this.bytesRadio.Size = new System.Drawing.Size(97, 29);
			this.bytesRadio.TabIndex = 7;
			this.bytesRadio.TabStop = true;
			this.bytesRadio.Text = "&Bytes";
			this.bytesRadio.UseVisualStyleBackColor = true;
			this.bytesRadio.CheckedChanged += new System.EventHandler(this.FormatRadio_CheckedChanged);
			this.bytesRadio.Click += new System.EventHandler(this.FormatRadio_CheckedChanged);
			// 
			// checkButton
			// 
			this.checkButton.Location = new System.Drawing.Point(18, 18);
			this.checkButton.Name = "checkButton";
			this.checkButton.Size = new System.Drawing.Size(152, 79);
			this.checkButton.TabIndex = 5;
			this.checkButton.Text = "&Check Now";
			this.checkButton.UseVisualStyleBackColor = true;
			this.checkButton.Click += new System.EventHandler(this.CheckButton_Click);
			// 
			// PanelTop
			// 
			this.PanelTop.Controls.Add(this.diffLabel);
			this.PanelTop.Controls.Add(this.drivesCombo);
			this.PanelTop.Controls.Add(this.label1);
			this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.PanelTop.Location = new System.Drawing.Point(0, 0);
			this.PanelTop.Name = "PanelTop";
			this.PanelTop.Size = new System.Drawing.Size(1243, 53);
			this.PanelTop.TabIndex = 0;
			// 
			// diffLabel
			// 
			this.diffLabel.AutoSize = true;
			this.diffLabel.Location = new System.Drawing.Point(715, 15);
			this.diffLabel.Name = "diffLabel";
			this.diffLabel.Size = new System.Drawing.Size(110, 25);
			this.diffLabel.TabIndex = 3;
			this.diffLabel.Text = "Difference";
			this.diffLabel.Visible = false;
			// 
			// drivesCombo
			// 
			this.drivesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.drivesCombo.FormattingEnabled = true;
			this.drivesCombo.Location = new System.Drawing.Point(96, 9);
			this.drivesCombo.Name = "drivesCombo";
			this.drivesCombo.Size = new System.Drawing.Size(519, 33);
			this.drivesCombo.TabIndex = 2;
			this.drivesCombo.SelectedIndexChanged += new System.EventHandler(this.DrivesCombo_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "&Drive";
			// 
			// entryList
			// 
			this.entryList.CheckBoxes = true;
			this.entryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.diffColumn,
            this.columnHeader5});
			this.entryList.ContextMenuStrip = this.contextMenuStrip1;
			this.entryList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.entryList.FullRowSelect = true;
			this.entryList.GridLines = true;
			this.entryList.HideSelection = false;
			this.entryList.LabelWrap = false;
			this.entryList.Location = new System.Drawing.Point(0, 53);
			this.entryList.Name = "entryList";
			this.entryList.ShowGroups = false;
			this.entryList.Size = new System.Drawing.Size(1243, 357);
			this.entryList.TabIndex = 3;
			this.entryList.UseCompatibleStateImageBehavior = false;
			this.entryList.View = System.Windows.Forms.View.Details;
			this.entryList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.EntryList_ItemChecked);
			// 
			// columnHeader0
			// 
			this.columnHeader0.Text = "Date/Time";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Total";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Used";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Free";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// diffColumn
			// 
			this.diffColumn.Text = "Difference";
			this.diffColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "";
			// 
			// MainForm
			// 
			this.AcceptButton = this.checkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1243, 524);
			this.Controls.Add(this.entryList);
			this.Controls.Add(this.PanelTop);
			this.Controls.Add(this.PanelBottom);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.PanelBottom.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.PanelTop.ResumeLayout(false);
			this.PanelTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem copyMenu;
		private System.Windows.Forms.ToolStripMenuItem clearMenu;
		private System.Windows.Forms.Panel PanelBottom;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton gbRadio;
		private System.Windows.Forms.RadioButton kbRadio;
		private System.Windows.Forms.RadioButton bytesRadio;
		private System.Windows.Forms.Button checkButton;
		private System.Windows.Forms.Panel PanelTop;
		private System.Windows.Forms.ComboBox drivesCombo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton mbRadio;
		private System.Windows.Forms.ListView entryList;
		private System.Windows.Forms.ColumnHeader columnHeader0;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader diffColumn;
		private System.Windows.Forms.Label diffLabel;
		private System.Windows.Forms.ColumnHeader columnHeader5;
	}
}

