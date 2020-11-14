namespace RecentFiles
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.detailLabel = new System.Windows.Forms.Label();
			this.settingSelection = new System.Windows.Forms.ComboBox();
			this.closeButton = new System.Windows.Forms.LinkLabel();
			this.checkOldestFirst = new System.Windows.Forms.CheckBox();
			this.labelCount = new System.Windows.Forms.Label();
			this.searchText = new System.Windows.Forms.TextBox();
			this.content = new System.Windows.Forms.ListView();
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.itemContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuExplore = new System.Windows.Forms.ToolStripMenuItem();
			this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.itemContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.detailLabel);
			this.panel1.Controls.Add(this.settingSelection);
			this.panel1.Controls.Add(this.closeButton);
			this.panel1.Controls.Add(this.checkOldestFirst);
			this.panel1.Controls.Add(this.labelCount);
			this.panel1.Controls.Add(this.searchText);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1758, 173);
			this.panel1.TabIndex = 1;
			// 
			// detailLabel
			// 
			this.detailLabel.AutoSize = true;
			this.detailLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.detailLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.detailLabel.ForeColor = System.Drawing.Color.Aqua;
			this.detailLabel.Location = new System.Drawing.Point(930, 18);
			this.detailLabel.Name = "detailLabel";
			this.detailLabel.Size = new System.Drawing.Size(116, 45);
			this.detailLabel.TabIndex = 4;
			this.detailLabel.Text = "Details";
			this.toolTipMain.SetToolTip(this.detailLabel, "Click to change settings.");
			this.detailLabel.Click += new System.EventHandler(this.DetailLabel_Click);
			// 
			// settingSelection
			// 
			this.settingSelection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.settingSelection.ForeColor = System.Drawing.SystemColors.WindowText;
			this.settingSelection.FormattingEnabled = true;
			this.settingSelection.Location = new System.Drawing.Point(18, 15);
			this.settingSelection.Name = "settingSelection";
			this.settingSelection.Size = new System.Drawing.Size(886, 53);
			this.settingSelection.TabIndex = 0;
			this.settingSelection.SelectedIndexChanged += new System.EventHandler(this.SettingSelection_SelectedIndexChanged);
			this.settingSelection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SettingSelection_KeyPress);
			// 
			// closeButton
			// 
			this.closeButton.AutoSize = true;
			this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.closeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.closeButton.LinkColor = System.Drawing.Color.Aqua;
			this.closeButton.Location = new System.Drawing.Point(1649, 0);
			this.closeButton.Name = "closeButton";
			this.closeButton.Padding = new System.Windows.Forms.Padding(0, 11, 11, 0);
			this.closeButton.Size = new System.Drawing.Size(109, 56);
			this.closeButton.TabIndex = 3;
			this.closeButton.TabStop = true;
			this.closeButton.Text = "Close";
			this.closeButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CloseButton_LinkClicked);
			// 
			// checkOldestFirst
			// 
			this.checkOldestFirst.AutoSize = true;
			this.checkOldestFirst.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkOldestFirst.Location = new System.Drawing.Point(1358, 107);
			this.checkOldestFirst.Name = "checkOldestFirst";
			this.checkOldestFirst.Size = new System.Drawing.Size(214, 49);
			this.checkOldestFirst.TabIndex = 2;
			this.checkOldestFirst.Text = "&Oldest First";
			this.checkOldestFirst.UseVisualStyleBackColor = true;
			this.checkOldestFirst.CheckedChanged += new System.EventHandler(this.CheckOldestFirst_CheckedChanged);
			// 
			// labelCount
			// 
			this.labelCount.AutoSize = true;
			this.labelCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCount.Location = new System.Drawing.Point(930, 106);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(155, 45);
			this.labelCount.TabIndex = 1;
			this.labelCount.Text = "Loading...";
			this.labelCount.Click += new System.EventHandler(this.LabelCount_Click);
			this.labelCount.MouseHover += new System.EventHandler(this.LabelCount_MouseHover);
			// 
			// searchText
			// 
			this.searchText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.searchText.Location = new System.Drawing.Point(12, 106);
			this.searchText.Name = "searchText";
			this.searchText.Size = new System.Drawing.Size(892, 50);
			this.searchText.TabIndex = 0;
			this.searchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
			this.searchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchText_KeyPress);
			// 
			// content
			// 
			this.content.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.content.BackColor = System.Drawing.Color.Black;
			this.content.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.content.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.date});
			this.content.ContextMenuStrip = this.itemContext;
			this.content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.content.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.content.ForeColor = System.Drawing.Color.White;
			this.content.FullRowSelect = true;
			this.content.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.content.HideSelection = false;
			this.content.HotTracking = true;
			this.content.HoverSelection = true;
			this.content.LabelWrap = false;
			this.content.Location = new System.Drawing.Point(0, 173);
			this.content.MultiSelect = false;
			this.content.Name = "content";
			this.content.ShowGroups = false;
			this.content.Size = new System.Drawing.Size(1758, 723);
			this.content.TabIndex = 2;
			this.content.UseCompatibleStateImageBehavior = false;
			this.content.View = System.Windows.Forms.View.Details;
			this.content.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Content_KeyPress);
			this.content.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Content_MouseClick);
			// 
			// name
			// 
			this.name.Width = 120;
			// 
			// date
			// 
			this.date.Width = 120;
			// 
			// itemContext
			// 
			this.itemContext.BackColor = System.Drawing.SystemColors.Control;
			this.itemContext.Font = new System.Drawing.Font("Segoe UI", 10F);
			this.itemContext.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.itemContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopy,
            this.menuExplore,
            this.menuDelete,
            this.menuEdit});
			this.itemContext.Name = "itemContext";
			this.itemContext.Size = new System.Drawing.Size(183, 180);
			// 
			// menuCopy
			// 
			this.menuCopy.Name = "menuCopy";
			this.menuCopy.Size = new System.Drawing.Size(182, 44);
			this.menuCopy.Text = "&Copy";
			this.menuCopy.Click += new System.EventHandler(this.MenuCopy_Click);
			// 
			// menuExplore
			// 
			this.menuExplore.Name = "menuExplore";
			this.menuExplore.Size = new System.Drawing.Size(182, 44);
			this.menuExplore.Text = "E&xplore";
			this.menuExplore.Click += new System.EventHandler(this.MenuExplore_Click);
			// 
			// menuDelete
			// 
			this.menuDelete.Name = "menuDelete";
			this.menuDelete.Size = new System.Drawing.Size(182, 44);
			this.menuDelete.Text = "&Delete";
			this.menuDelete.Click += new System.EventHandler(this.MenuDelete_Click);
			// 
			// menuEdit
			// 
			this.menuEdit.Name = "menuEdit";
			this.menuEdit.Size = new System.Drawing.Size(182, 44);
			this.menuEdit.Text = "&Edit";
			this.menuEdit.Click += new System.EventHandler(this.MenuEdit_Click);
			// 
			// toolTipMain
			// 
			this.toolTipMain.AutoPopDelay = 5000;
			this.toolTipMain.InitialDelay = 100;
			this.toolTipMain.IsBalloon = true;
			this.toolTipMain.ReshowDelay = 100;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1758, 896);
			this.Controls.Add(this.content);
			this.Controls.Add(this.panel1);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.itemContext.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView content;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader date;
		private System.Windows.Forms.TextBox searchText;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.CheckBox checkOldestFirst;
		private System.Windows.Forms.ContextMenuStrip itemContext;
		private System.Windows.Forms.ToolStripMenuItem menuCopy;
		private System.Windows.Forms.LinkLabel closeButton;
		private System.Windows.Forms.ToolStripMenuItem menuDelete;
		private System.Windows.Forms.ToolStripMenuItem menuExplore;
		private System.Windows.Forms.ToolTip toolTipMain;
		private System.Windows.Forms.ComboBox settingSelection;
		private System.Windows.Forms.Label detailLabel;
		private System.Windows.Forms.ToolStripMenuItem menuEdit;
	}
}

