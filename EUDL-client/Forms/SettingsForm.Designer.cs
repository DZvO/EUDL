namespace EUDL {
	partial class SettingsForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxNickName = new System.Windows.Forms.TextBox();
			this.textBoxSteamName = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOkay = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nickname";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Steamname";
			// 
			// textBoxNickName
			// 
			this.textBoxNickName.Location = new System.Drawing.Point(92, 13);
			this.textBoxNickName.Name = "textBoxNickName";
			this.textBoxNickName.Size = new System.Drawing.Size(100, 20);
			this.textBoxNickName.TabIndex = 2;
			// 
			// textBoxSteamName
			// 
			this.textBoxSteamName.Location = new System.Drawing.Point(92, 39);
			this.textBoxSteamName.Name = "textBoxSteamName";
			this.textBoxSteamName.Size = new System.Drawing.Size(100, 20);
			this.textBoxSteamName.TabIndex = 3;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(92, 66);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
			this.textBoxPassword.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Password";
			// 
			// buttonOkay
			// 
			this.buttonOkay.Location = new System.Drawing.Point(124, 104);
			this.buttonOkay.Name = "buttonOkay";
			this.buttonOkay.Size = new System.Drawing.Size(75, 23);
			this.buttonOkay.TabIndex = 6;
			this.buttonOkay.Text = "Okay";
			this.buttonOkay.UseVisualStyleBackColor = true;
			this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(43, 104);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(211, 136);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOkay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.textBoxSteamName);
			this.Controls.Add(this.textBoxNickName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "SettingsForm";
			this.Text = "SettingsForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxNickName;
		private System.Windows.Forms.TextBox textBoxSteamName;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOkay;
		private System.Windows.Forms.Button buttonCancel;
	}
}