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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Normal));
            this.grpIndex = new System.Windows.Forms.GroupBox();
            this.lbxIndex = new System.Windows.Forms.ListBox();
            this.pnlDefiniton = new System.Windows.Forms.Panel();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
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
            this.grpIndex.Margin = new System.Windows.Forms.Padding(2);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Padding = new System.Windows.Forms.Padding(2);
            this.grpIndex.Size = new System.Drawing.Size(148, 394);
            this.grpIndex.TabIndex = 1;
            this.grpIndex.TabStop = false;
            this.grpIndex.Text = "Index";
            // 
            // lbxIndex
            // 
            this.lbxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxIndex.FormattingEnabled = true;
            this.lbxIndex.Location = new System.Drawing.Point(2, 15);
            this.lbxIndex.Name = "lbxIndex";
            this.lbxIndex.Size = new System.Drawing.Size(144, 377);
            this.lbxIndex.TabIndex = 5;
            this.lbxIndex.SelectedIndexChanged += new System.EventHandler(this.lbxIndex_SelectedIndexChanged);
            // 
            // pnlDefiniton
            // 
            this.pnlDefiniton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDefiniton.AutoScroll = true;
            this.pnlDefiniton.Controls.Add(this.txtDefinition);
            this.pnlDefiniton.Location = new System.Drawing.Point(190, 82);
            this.pnlDefiniton.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDefiniton.Name = "pnlDefiniton";
            this.pnlDefiniton.Size = new System.Drawing.Size(605, 394);
            this.pnlDefiniton.TabIndex = 3;
            // 
            // txtDefinition
            // 
            this.txtDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefinition.Location = new System.Drawing.Point(2, 2);
            this.txtDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.txtDefinition.Multiline = true;
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.ReadOnly = true;
            this.txtDefinition.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDefinition.Size = new System.Drawing.Size(603, 390);
            this.txtDefinition.TabIndex = 0;
            this.txtDefinition.TextChanged += new System.EventHandler(this.txtDefinition_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBack.Location = new System.Drawing.Point(300, 45);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(43, 24);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(190, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 24);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(17, 45);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(144, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // Normal
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 486);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlDefiniton);
            this.Controls.Add(this.grpIndex);
            this.Controls.Add(this.txtSearch);
            this.Name = "Normal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Normal";
            this.Load += new System.EventHandler(this.Normal_Load);
            this.grpIndex.ResumeLayout(false);
            this.pnlDefiniton.ResumeLayout(false);
            this.pnlDefiniton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.Panel pnlDefiniton;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtDefinition;
		private System.Windows.Forms.ListBox lbxIndex;
		private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
    }
}