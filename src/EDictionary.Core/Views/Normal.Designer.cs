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
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.pnlDefiniton = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpIndex.SuspendLayout();
            this.grpResults.SuspendLayout();
            this.pnlDefiniton.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(23, 65);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(198, 30);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search";
            // 
            // grpIndex
            // 
            this.grpIndex.Controls.Add(this.txtIndex);
            this.grpIndex.Location = new System.Drawing.Point(23, 111);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Size = new System.Drawing.Size(198, 248);
            this.grpIndex.TabIndex = 1;
            this.grpIndex.TabStop = false;
            this.grpIndex.Text = "Index";
            this.grpIndex.Enter += new System.EventHandler(this.grpIndex_Enter);
            // 
            // txtIndex
            // 
            this.txtIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIndex.Location = new System.Drawing.Point(3, 18);
            this.txtIndex.Multiline = true;
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Size = new System.Drawing.Size(192, 227);
            this.txtIndex.TabIndex = 0;
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.txtResults);
            this.grpResults.Location = new System.Drawing.Point(23, 365);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(200, 177);
            this.grpResults.TabIndex = 2;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // txtResults
            // 
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Location = new System.Drawing.Point(3, 18);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(194, 156);
            this.txtResults.TabIndex = 0;
            // 
            // pnlDefiniton
            // 
            this.pnlDefiniton.Controls.Add(this.vScrollBar1);
            this.pnlDefiniton.Controls.Add(this.txtDefinition);
            this.pnlDefiniton.Location = new System.Drawing.Point(265, 111);
            this.pnlDefiniton.Name = "pnlDefiniton";
            this.pnlDefiniton.Size = new System.Drawing.Size(769, 431);
            this.pnlDefiniton.TabIndex = 3;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(744, 4);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(22, 424);
            this.vScrollBar1.TabIndex = 1;
            // 
            // txtDefinition
            // 
            this.txtDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefinition.Location = new System.Drawing.Point(0, 0);
            this.txtDefinition.Multiline = true;
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Size = new System.Drawing.Size(769, 431);
            this.txtDefinition.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBack.Location = new System.Drawing.Point(265, 65);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(57, 30);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // Normal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlDefiniton);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.grpIndex);
            this.Controls.Add(this.txtSearch);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Normal";
            this.Text = "Normal";
            this.grpIndex.ResumeLayout(false);
            this.grpIndex.PerformLayout();
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.pnlDefiniton.ResumeLayout(false);
            this.pnlDefiniton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.Panel pnlDefiniton;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.TextBox txtDefinition;
    }
}