namespace EDictionary.Core.Views
{
	partial class Testing
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
			this.txtSpellCheck = new System.Windows.Forms.TextBox();
			this.btnSpellCheck = new System.Windows.Forms.Button();
			this.lbxSpellCheck = new System.Windows.Forms.ListBox();
			this.btnReadJson = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtSpellCheck
			// 
			this.txtSpellCheck.Location = new System.Drawing.Point(20, 14);
			this.txtSpellCheck.Name = "txtSpellCheck";
			this.txtSpellCheck.Size = new System.Drawing.Size(100, 20);
			this.txtSpellCheck.TabIndex = 0;
			// 
			// btnSpellCheck
			// 
			this.btnSpellCheck.Location = new System.Drawing.Point(126, 12);
			this.btnSpellCheck.Name = "btnSpellCheck";
			this.btnSpellCheck.Size = new System.Drawing.Size(75, 23);
			this.btnSpellCheck.TabIndex = 1;
			this.btnSpellCheck.Text = "Spellcheck";
			this.btnSpellCheck.UseVisualStyleBackColor = true;
			this.btnSpellCheck.Click += new System.EventHandler(this.btnSpellCheck_Click);
			// 
			// lbxSpellCheck
			// 
			this.lbxSpellCheck.FormattingEnabled = true;
			this.lbxSpellCheck.Items.AddRange(new object[] {
            "<Empty>"});
			this.lbxSpellCheck.Location = new System.Drawing.Point(20, 42);
			this.lbxSpellCheck.Name = "lbxSpellCheck";
			this.lbxSpellCheck.Size = new System.Drawing.Size(100, 147);
			this.lbxSpellCheck.TabIndex = 2;
			// 
			// btnReadJson
			// 
			this.btnReadJson.Location = new System.Drawing.Point(126, 42);
			this.btnReadJson.Name = "btnReadJson";
			this.btnReadJson.Size = new System.Drawing.Size(75, 23);
			this.btnReadJson.TabIndex = 3;
			this.btnReadJson.Text = "Read Json";
			this.btnReadJson.UseVisualStyleBackColor = true;
			this.btnReadJson.Click += new System.EventHandler(this.btnReadJson_Click);
			// 
			// Testing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 283);
			this.Controls.Add(this.btnReadJson);
			this.Controls.Add(this.lbxSpellCheck);
			this.Controls.Add(this.btnSpellCheck);
			this.Controls.Add(this.txtSpellCheck);
			this.Name = "Testing";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Testing";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSpellCheck;
		private System.Windows.Forms.Button btnSpellCheck;
		private System.Windows.Forms.ListBox lbxSpellCheck;
		private System.Windows.Forms.Button btnReadJson;
	}
}