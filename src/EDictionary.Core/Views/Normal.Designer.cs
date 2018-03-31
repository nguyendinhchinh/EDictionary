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
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.pnlDefiniton = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
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
            this.grpIndex.Location = new System.Drawing.Point(23, 111);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Size = new System.Drawing.Size(198, 248);
            this.grpIndex.TabIndex = 1;
            this.grpIndex.TabStop = false;
            this.grpIndex.Text = "Index";
            // 
            // grpResults
            // 
            this.grpResults.Location = new System.Drawing.Point(23, 398);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(200, 144);
            this.grpResults.TabIndex = 2;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // pnlDefiniton
            // 
            this.pnlDefiniton.Location = new System.Drawing.Point(265, 111);
            this.pnlDefiniton.Name = "pnlDefiniton";
            this.pnlDefiniton.Size = new System.Drawing.Size(769, 431);
            this.pnlDefiniton.TabIndex = 3;
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Normal";
            this.Text = "Normal";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.Panel pnlDefiniton;
        private System.Windows.Forms.Button btnBack;
    }
}