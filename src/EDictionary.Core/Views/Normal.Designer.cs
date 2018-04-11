namespace EDictionary.Core.Views
{
	partial class Normal
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
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.grpIndex = new System.Windows.Forms.GroupBox();
			this.lbxIndex = new System.Windows.Forms.ListBox();
			this.pnlDefiniton = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.txtDefinition = new System.Windows.Forms.TextBox();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.grpIndex.SuspendLayout();
			this.pnlDefiniton.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(23, 65);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtSearch.Multiline = true;
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(143, 27);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "Search";
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
			// 
			// grpIndex
			// 
			this.grpIndex.Controls.Add(this.lbxIndex);
			this.grpIndex.Location = new System.Drawing.Point(23, 101);
			this.grpIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grpIndex.Name = "grpIndex";
			this.grpIndex.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grpIndex.Size = new System.Drawing.Size(197, 441);
			this.grpIndex.TabIndex = 1;
			this.grpIndex.TabStop = false;
			this.grpIndex.Text = "Index";
			// 
			// lbxIndex
			// 
			this.lbxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbxIndex.FormattingEnabled = true;
			this.lbxIndex.ItemHeight = 16;
			this.lbxIndex.Location = new System.Drawing.Point(3, 21);
			this.lbxIndex.Margin = new System.Windows.Forms.Padding(4);
			this.lbxIndex.Name = "lbxIndex";
			this.lbxIndex.Size = new System.Drawing.Size(191, 404);
			this.lbxIndex.TabIndex = 5;
			this.lbxIndex.SelectedIndexChanged += new System.EventHandler(this.lbxIndex_SelectedIndexChanged);
			// 
			// pnlDefiniton
			// 
			this.pnlDefiniton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDefiniton.Controls.Add(this.vScrollBar1);
			this.pnlDefiniton.Controls.Add(this.txtDefinition);
			this.pnlDefiniton.Location = new System.Drawing.Point(265, 111);
			this.pnlDefiniton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pnlDefiniton.Name = "pnlDefiniton";
			this.pnlDefiniton.Size = new System.Drawing.Size(769, 431);
			this.pnlDefiniton.TabIndex = 3;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.Location = new System.Drawing.Point(744, 4);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(22, 423);
			this.vScrollBar1.TabIndex = 1;
			// 
			// txtDefinition
			// 
			this.txtDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDefinition.Location = new System.Drawing.Point(3, 0);
			this.txtDefinition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtDefinition.Multiline = true;
			this.txtDefinition.Name = "txtDefinition";
			this.txtDefinition.Size = new System.Drawing.Size(781, 443);
			this.txtDefinition.TabIndex = 0;
			// 
			// btnBack
			// 
			this.btnBack.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.btnBack.Location = new System.Drawing.Point(265, 65);
			this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(57, 30);
			this.btnBack.TabIndex = 4;
			this.btnBack.Text = "Back";
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(175, 65);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(45, 28);
			this.btnSearch.TabIndex = 5;
			this.btnSearch.Text = "button1";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// Normal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 554);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.pnlDefiniton);
			this.Controls.Add(this.grpIndex);
			this.Controls.Add(this.txtSearch);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Normal";
			this.Text = "Normal";
			this.Load += new System.EventHandler(this.Normal_Load);
			this.grpIndex.ResumeLayout(false);
			this.pnlDefiniton.ResumeLayout(false);
			this.pnlDefiniton.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.Panel pnlDefiniton;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.TextBox txtDefinition;
		private System.Windows.Forms.ListBox lbxIndex;
		private System.Windows.Forms.Button btnSearch;
	}
}