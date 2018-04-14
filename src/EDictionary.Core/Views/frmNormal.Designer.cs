namespace EDictionary.Core.Views
{
	partial class frmNormal
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNormal));
			this.grpIndex = new System.Windows.Forms.GroupBox();
			this.lbxIndex = new System.Windows.Forms.ListBox();
			this.pnlDefiniton = new System.Windows.Forms.Panel();
			this.rtxDefinition = new System.Windows.Forms.RichTextBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.btnPrevHistory = new System.Windows.Forms.Button();
			this.btnNextHistory = new System.Windows.Forms.Button();
			this.grpIndex.SuspendLayout();
			this.pnlDefiniton.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpIndex
			// 
			this.grpIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.grpIndex.Controls.Add(this.lbxIndex);
			this.grpIndex.Location = new System.Drawing.Point(17, 82);
			this.grpIndex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.grpIndex.Name = "grpIndex";
			this.grpIndex.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.grpIndex.Size = new System.Drawing.Size(148, 394);
			this.grpIndex.TabIndex = 1;
			this.grpIndex.TabStop = false;
			this.grpIndex.Text = "ListWord";
			// 
			// lbxIndex
			// 
			this.lbxIndex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbxIndex.FormattingEnabled = true;
			this.lbxIndex.Location = new System.Drawing.Point(2, 15);
			this.lbxIndex.Name = "lbxIndex";
			this.lbxIndex.Size = new System.Drawing.Size(144, 377);
			this.lbxIndex.TabIndex = 0;
			this.lbxIndex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxIndex_MouseDoubleClick);
			// 
			// pnlDefiniton
			// 
			this.pnlDefiniton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDefiniton.AutoScroll = true;
			this.pnlDefiniton.Controls.Add(this.rtxDefinition);
			this.pnlDefiniton.Location = new System.Drawing.Point(190, 97);
			this.pnlDefiniton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.pnlDefiniton.Name = "pnlDefiniton";
			this.pnlDefiniton.Size = new System.Drawing.Size(605, 379);
			this.pnlDefiniton.TabIndex = 3;
			// 
			// rtxDefinition
			// 
			this.rtxDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxDefinition.Location = new System.Drawing.Point(0, 0);
			this.rtxDefinition.Name = "rtxDefinition";
			this.rtxDefinition.ReadOnly = true;
			this.rtxDefinition.Size = new System.Drawing.Size(605, 379);
			this.rtxDefinition.TabIndex = 0;
			this.rtxDefinition.Text = "";
			this.rtxDefinition.DoubleClick += new System.EventHandler(this.rtxDefinition_DoubleClick);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(19, 54);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(112, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
			// 
			// btnSearch
			// 
			this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
			this.btnSearch.Location = new System.Drawing.Point(134, 52);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(24, 24);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.UseVisualStyleBackColor = true;
			// 
			// btnPrevHistory
			// 
			this.btnPrevHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevHistory.Image")));
			this.btnPrevHistory.Location = new System.Drawing.Point(191, 52);
			this.btnPrevHistory.Name = "btnPrevHistory";
			this.btnPrevHistory.Size = new System.Drawing.Size(24, 24);
			this.btnPrevHistory.TabIndex = 4;
			this.btnPrevHistory.UseVisualStyleBackColor = true;
			this.btnPrevHistory.Click += new System.EventHandler(this.btnPrevHistory_Click);
			// 
			// btnNextHistory
			// 
			this.btnNextHistory.Image = global::EDictionary.Core.Properties.Resources.RightArrow;
			this.btnNextHistory.Location = new System.Drawing.Point(221, 52);
			this.btnNextHistory.Name = "btnNextHistory";
			this.btnNextHistory.Size = new System.Drawing.Size(24, 24);
			this.btnNextHistory.TabIndex = 5;
			this.btnNextHistory.UseVisualStyleBackColor = true;
			this.btnNextHistory.Click += new System.EventHandler(this.btnNextHistory_Click);
			// 
			// frmNormal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(819, 486);
			this.Controls.Add(this.btnNextHistory);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnPrevHistory);
			this.Controls.Add(this.pnlDefiniton);
			this.Controls.Add(this.grpIndex);
			this.Controls.Add(this.txtSearch);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmNormal";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "E-Dictionary";
			this.Load += new System.EventHandler(this.Normal_Load);
			this.grpIndex.ResumeLayout(false);
			this.pnlDefiniton.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.Panel pnlDefiniton;
		private System.Windows.Forms.ListBox lbxIndex;
        private System.Windows.Forms.RichTextBox rtxDefinition;
        private System.Windows.Forms.Button btnPrevHistory;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNextHistory;
    }
}
