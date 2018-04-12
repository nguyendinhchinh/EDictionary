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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
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
            this.grpIndex.Text = "ListWord";
            // 
            // lbxIndex
            // 
            this.lbxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxIndex.FormattingEnabled = true;
            this.lbxIndex.Location = new System.Drawing.Point(2, 15);
            this.lbxIndex.Name = "lbxIndex";
            this.lbxIndex.Size = new System.Drawing.Size(144, 368);
            this.lbxIndex.TabIndex = 0;
            this.lbxIndex.SelectedIndexChanged += new System.EventHandler(this.lbxIndex_SelectedIndexChanged);
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
            this.pnlDefiniton.Margin = new System.Windows.Forms.Padding(2);
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
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
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
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(191, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = global::EDictionary.Core.Properties.Resources.DownArrow;
            this.button2.Location = new System.Drawing.Point(281, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 24);
            this.button2.TabIndex = 5;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(251, 52);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 24);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::EDictionary.Core.Properties.Resources.RightArrow;
            this.button4.Location = new System.Drawing.Point(221, 52);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 24);
            this.button4.TabIndex = 5;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // frmNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 486);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
