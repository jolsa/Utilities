namespace RecentFiles
{
	partial class Options
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
			this.checkIncludeSubs = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.LinkLabel();
			this.applyButton = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.textExtensions = new System.Windows.Forms.TextBox();
			this.checkSearchContent = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkIncludeSubs
			// 
			this.checkIncludeSubs.AutoSize = true;
			this.checkIncludeSubs.Location = new System.Drawing.Point(30, 120);
			this.checkIncludeSubs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkIncludeSubs.Name = "checkIncludeSubs";
			this.checkIncludeSubs.Size = new System.Drawing.Size(317, 49);
			this.checkIncludeSubs.TabIndex = 0;
			this.checkIncludeSubs.Text = "Include &Subfolders";
			this.checkIncludeSubs.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.closeButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(800, 100);
			this.panel1.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(284, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 98);
			this.label1.TabIndex = 6;
			this.label1.Text = "Options";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// closeButton
			// 
			this.closeButton.AutoSize = true;
			this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.closeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.closeButton.LinkColor = System.Drawing.Color.Aqua;
			this.closeButton.Location = new System.Drawing.Point(675, 0);
			this.closeButton.Name = "closeButton";
			this.closeButton.Padding = new System.Windows.Forms.Padding(0, 11, 11, 0);
			this.closeButton.Size = new System.Drawing.Size(125, 56);
			this.closeButton.TabIndex = 5;
			this.closeButton.TabStop = true;
			this.closeButton.Text = "Cancel";
			this.closeButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CloseButton_LinkClicked);
			// 
			// applyButton
			// 
			this.applyButton.AutoSize = true;
			this.applyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.applyButton.LinkColor = System.Drawing.Color.Aqua;
			this.applyButton.Location = new System.Drawing.Point(345, 422);
			this.applyButton.Name = "applyButton";
			this.applyButton.Padding = new System.Windows.Forms.Padding(0, 11, 11, 0);
			this.applyButton.Size = new System.Drawing.Size(113, 56);
			this.applyButton.TabIndex = 4;
			this.applyButton.TabStop = true;
			this.applyButton.Text = "Apply";
			this.applyButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ApplyButton_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(170, 45);
			this.label2.TabIndex = 1;
			this.label2.Text = "E&xtensions";
			// 
			// textExtensions
			// 
			this.textExtensions.Location = new System.Drawing.Point(30, 253);
			this.textExtensions.Name = "textExtensions";
			this.textExtensions.Size = new System.Drawing.Size(737, 50);
			this.textExtensions.TabIndex = 2;
			// 
			// checkSearchContent
			// 
			this.checkSearchContent.AutoSize = true;
			this.checkSearchContent.Location = new System.Drawing.Point(30, 341);
			this.checkSearchContent.Name = "checkSearchContent";
			this.checkSearchContent.Size = new System.Drawing.Size(269, 49);
			this.checkSearchContent.TabIndex = 3;
			this.checkSearchContent.Text = "Search &Content";
			this.checkSearchContent.UseVisualStyleBackColor = true;
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(800, 508);
			this.ControlBox = false;
			this.Controls.Add(this.checkSearchContent);
			this.Controls.Add(this.textExtensions);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.checkIncludeSubs);
			this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Options";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.Options_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Options_KeyPress);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkIncludeSubs;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel closeButton;
		private System.Windows.Forms.LinkLabel applyButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textExtensions;
		private System.Windows.Forms.CheckBox checkSearchContent;
	}
}

