namespace EUDL {
	partial class MainForm {
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
			this.components = new System.ComponentModel.Container();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.main = new System.Windows.Forms.TabPage();
			this.textBoxMain = new System.Windows.Forms.RichTextBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.listBoxPendingGames = new System.Windows.Forms.ListBox();
			this.contextMenuStripPendingGames = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listBoxStartedGames = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonSettings = new System.Windows.Forms.Button();
			this.buttonSign = new System.Windows.Forms.Button();
			this.buttonConfirm = new System.Windows.Forms.Button();
			this.panelContextLobby = new System.Windows.Forms.Panel();
			this.buttonCreateLobby = new System.Windows.Forms.Button();
			this.listBoxOnlineUsers = new System.Windows.Forms.ListBox();
			this.panelContextGame = new System.Windows.Forms.Panel();
			this.buttonUnsign = new System.Windows.Forms.Button();
			this.buttonResultDraw = new System.Windows.Forms.Button();
			this.buttonResultDire = new System.Windows.Forms.Button();
			this.buttonResultRadiant = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listBoxDire = new System.Windows.Forms.ListBox();
			this.listBoxRadiant = new System.Windows.Forms.ListBox();
			this.radioButtonConnected = new System.Windows.Forms.RadioButton();
			this.radioButtonMatchmaking = new System.Windows.Forms.RadioButton();
			this.main.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.contextMenuStripPendingGames.SuspendLayout();
			this.panelContextLobby.SuspendLayout();
			this.panelContextGame.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonConnect.Location = new System.Drawing.Point(578, 5);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(195, 23);
			this.buttonConnect.TabIndex = 1;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonSend
			// 
			this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSend.Enabled = false;
			this.buttonSend.Location = new System.Drawing.Point(694, 509);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(79, 23);
			this.buttonSend.TabIndex = 2;
			this.buttonSend.Text = "Send";
			this.buttonSend.UseVisualStyleBackColor = true;
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// textBoxInput
			// 
			this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxInput.Enabled = false;
			this.textBoxInput.Location = new System.Drawing.Point(5, 509);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(683, 20);
			this.textBoxInput.TabIndex = 3;
			this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInput_KeyPress);
			// 
			// main
			// 
			this.main.Controls.Add(this.textBoxMain);
			this.main.Location = new System.Drawing.Point(4, 22);
			this.main.Name = "main";
			this.main.Padding = new System.Windows.Forms.Padding(3);
			this.main.Size = new System.Drawing.Size(559, 465);
			this.main.TabIndex = 0;
			this.main.Text = "Main";
			this.main.UseVisualStyleBackColor = true;
			// 
			// textBoxMain
			// 
			this.textBoxMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.textBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMain.Location = new System.Drawing.Point(3, 3);
			this.textBoxMain.Name = "textBoxMain";
			this.textBoxMain.ReadOnly = true;
			this.textBoxMain.Size = new System.Drawing.Size(553, 459);
			this.textBoxMain.TabIndex = 0;
			this.textBoxMain.Text = "";
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.main);
			this.tabControl.Location = new System.Drawing.Point(5, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(567, 491);
			this.tabControl.TabIndex = 4;
			// 
			// listBoxPendingGames
			// 
			this.listBoxPendingGames.ContextMenuStrip = this.contextMenuStripPendingGames;
			this.listBoxPendingGames.FormattingEnabled = true;
			this.listBoxPendingGames.Location = new System.Drawing.Point(3, 23);
			this.listBoxPendingGames.Name = "listBoxPendingGames";
			this.listBoxPendingGames.Size = new System.Drawing.Size(91, 121);
			this.listBoxPendingGames.TabIndex = 5;
			this.listBoxPendingGames.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxPendingGames_MouseDown);
			// 
			// contextMenuStripPendingGames
			// 
			this.contextMenuStripPendingGames.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.detailsToolStripMenuItem});
			this.contextMenuStripPendingGames.Name = "contextMenuStrip1";
			this.contextMenuStripPendingGames.ShowImageMargin = false;
			this.contextMenuStripPendingGames.Size = new System.Drawing.Size(85, 48);
			this.contextMenuStripPendingGames.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripPendingGames_Closed);
			this.contextMenuStripPendingGames.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(84, 22);
			this.toolStripMenuItem1.Text = "Join";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// detailsToolStripMenuItem
			// 
			this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
			this.detailsToolStripMenuItem.Size = new System.Drawing.Size(84, 22);
			this.detailsToolStripMenuItem.Text = "Details";
			// 
			// listBoxStartedGames
			// 
			this.listBoxStartedGames.FormattingEnabled = true;
			this.listBoxStartedGames.Location = new System.Drawing.Point(100, 23);
			this.listBoxStartedGames.Name = "listBoxStartedGames";
			this.listBoxStartedGames.Size = new System.Drawing.Size(92, 121);
			this.listBoxStartedGames.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Pending Games";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(97, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Started Games";
			// 
			// buttonSettings
			// 
			this.buttonSettings.Location = new System.Drawing.Point(493, 5);
			this.buttonSettings.Name = "buttonSettings";
			this.buttonSettings.Size = new System.Drawing.Size(79, 23);
			this.buttonSettings.TabIndex = 9;
			this.buttonSettings.Text = "Settings";
			this.buttonSettings.UseVisualStyleBackColor = true;
			this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
			// 
			// buttonSign
			// 
			this.buttonSign.Enabled = false;
			this.buttonSign.Location = new System.Drawing.Point(3, 443);
			this.buttonSign.Name = "buttonSign";
			this.buttonSign.Size = new System.Drawing.Size(94, 23);
			this.buttonSign.TabIndex = 10;
			this.buttonSign.Text = "Search";
			this.buttonSign.UseVisualStyleBackColor = true;
			this.buttonSign.Click += new System.EventHandler(this.buttonSign_Click);
			// 
			// buttonConfirm
			// 
			this.buttonConfirm.Location = new System.Drawing.Point(108, 316);
			this.buttonConfirm.Name = "buttonConfirm";
			this.buttonConfirm.Size = new System.Drawing.Size(79, 23);
			this.buttonConfirm.TabIndex = 11;
			this.buttonConfirm.Text = "Confirm";
			this.buttonConfirm.UseVisualStyleBackColor = true;
			// 
			// panelContextLobby
			// 
			this.panelContextLobby.Controls.Add(this.buttonCreateLobby);
			this.panelContextLobby.Controls.Add(this.listBoxOnlineUsers);
			this.panelContextLobby.Controls.Add(this.label1);
			this.panelContextLobby.Controls.Add(this.buttonSign);
			this.panelContextLobby.Controls.Add(this.listBoxPendingGames);
			this.panelContextLobby.Controls.Add(this.listBoxStartedGames);
			this.panelContextLobby.Controls.Add(this.label2);
			this.panelContextLobby.Location = new System.Drawing.Point(578, 34);
			this.panelContextLobby.Name = "panelContextLobby";
			this.panelContextLobby.Size = new System.Drawing.Size(195, 469);
			this.panelContextLobby.TabIndex = 12;
			// 
			// buttonCreateLobby
			// 
			this.buttonCreateLobby.Enabled = false;
			this.buttonCreateLobby.Location = new System.Drawing.Point(100, 443);
			this.buttonCreateLobby.Name = "buttonCreateLobby";
			this.buttonCreateLobby.Size = new System.Drawing.Size(91, 23);
			this.buttonCreateLobby.TabIndex = 11;
			this.buttonCreateLobby.Text = "Create";
			this.buttonCreateLobby.UseVisualStyleBackColor = true;
			this.buttonCreateLobby.Click += new System.EventHandler(this.buttonCreateLobby_Click);
			// 
			// listBoxOnlineUsers
			// 
			this.listBoxOnlineUsers.FormattingEnabled = true;
			this.listBoxOnlineUsers.Location = new System.Drawing.Point(3, 150);
			this.listBoxOnlineUsers.Name = "listBoxOnlineUsers";
			this.listBoxOnlineUsers.Size = new System.Drawing.Size(189, 290);
			this.listBoxOnlineUsers.TabIndex = 9;
			// 
			// panelContextGame
			// 
			this.panelContextGame.Controls.Add(this.buttonUnsign);
			this.panelContextGame.Controls.Add(this.buttonResultDraw);
			this.panelContextGame.Controls.Add(this.buttonResultDire);
			this.panelContextGame.Controls.Add(this.buttonConfirm);
			this.panelContextGame.Controls.Add(this.buttonResultRadiant);
			this.panelContextGame.Controls.Add(this.label4);
			this.panelContextGame.Controls.Add(this.label3);
			this.panelContextGame.Controls.Add(this.listBoxDire);
			this.panelContextGame.Controls.Add(this.listBoxRadiant);
			this.panelContextGame.Location = new System.Drawing.Point(578, 34);
			this.panelContextGame.Name = "panelContextGame";
			this.panelContextGame.Size = new System.Drawing.Size(194, 469);
			this.panelContextGame.TabIndex = 13;
			this.panelContextGame.Visible = false;
			// 
			// buttonUnsign
			// 
			this.buttonUnsign.Location = new System.Drawing.Point(7, 316);
			this.buttonUnsign.Name = "buttonUnsign";
			this.buttonUnsign.Size = new System.Drawing.Size(75, 23);
			this.buttonUnsign.TabIndex = 7;
			this.buttonUnsign.Text = "Unsign";
			this.buttonUnsign.UseVisualStyleBackColor = true;
			// 
			// buttonResultDraw
			// 
			this.buttonResultDraw.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonResultDraw.Location = new System.Drawing.Point(7, 427);
			this.buttonResultDraw.Name = "buttonResultDraw";
			this.buttonResultDraw.Size = new System.Drawing.Size(180, 35);
			this.buttonResultDraw.TabIndex = 6;
			this.buttonResultDraw.Text = "Draw";
			this.buttonResultDraw.UseVisualStyleBackColor = true;
			// 
			// buttonResultDire
			// 
			this.buttonResultDire.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonResultDire.Location = new System.Drawing.Point(7, 386);
			this.buttonResultDire.Name = "buttonResultDire";
			this.buttonResultDire.Size = new System.Drawing.Size(180, 35);
			this.buttonResultDire.TabIndex = 5;
			this.buttonResultDire.Text = "Dire";
			this.buttonResultDire.UseVisualStyleBackColor = true;
			// 
			// buttonResultRadiant
			// 
			this.buttonResultRadiant.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonResultRadiant.Location = new System.Drawing.Point(7, 345);
			this.buttonResultRadiant.Name = "buttonResultRadiant";
			this.buttonResultRadiant.Size = new System.Drawing.Size(180, 35);
			this.buttonResultRadiant.TabIndex = 4;
			this.buttonResultRadiant.Text = "Radiant";
			this.buttonResultRadiant.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("MS Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(3, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 19);
			this.label4.TabIndex = 3;
			this.label4.Text = "Dire";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("MS Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(3, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 19);
			this.label3.TabIndex = 2;
			this.label3.Text = "Radiant";
			// 
			// listBoxDire
			// 
			this.listBoxDire.FormattingEnabled = true;
			this.listBoxDire.Location = new System.Drawing.Point(3, 166);
			this.listBoxDire.Name = "listBoxDire";
			this.listBoxDire.Size = new System.Drawing.Size(188, 95);
			this.listBoxDire.TabIndex = 1;
			// 
			// listBoxRadiant
			// 
			this.listBoxRadiant.FormattingEnabled = true;
			this.listBoxRadiant.Location = new System.Drawing.Point(3, 32);
			this.listBoxRadiant.Name = "listBoxRadiant";
			this.listBoxRadiant.Size = new System.Drawing.Size(188, 95);
			this.listBoxRadiant.TabIndex = 0;
			// 
			// radioButtonConnected
			// 
			this.radioButtonConnected.AutoCheck = false;
			this.radioButtonConnected.AutoSize = true;
			this.radioButtonConnected.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioButtonConnected.Location = new System.Drawing.Point(410, 5);
			this.radioButtonConnected.Name = "radioButtonConnected";
			this.radioButtonConnected.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioButtonConnected.Size = new System.Drawing.Size(77, 17);
			this.radioButtonConnected.TabIndex = 14;
			this.radioButtonConnected.TabStop = true;
			this.radioButtonConnected.Text = "Connected";
			this.radioButtonConnected.UseVisualStyleBackColor = true;
			// 
			// radioButtonMatchmaking
			// 
			this.radioButtonMatchmaking.AutoCheck = false;
			this.radioButtonMatchmaking.AutoSize = true;
			this.radioButtonMatchmaking.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioButtonMatchmaking.Location = new System.Drawing.Point(315, 5);
			this.radioButtonMatchmaking.Name = "radioButtonMatchmaking";
			this.radioButtonMatchmaking.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioButtonMatchmaking.Size = new System.Drawing.Size(89, 17);
			this.radioButtonMatchmaking.TabIndex = 15;
			this.radioButtonMatchmaking.TabStop = true;
			this.radioButtonMatchmaking.Text = "Matchmaking";
			this.radioButtonMatchmaking.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 536);
			this.Controls.Add(this.radioButtonMatchmaking);
			this.Controls.Add(this.radioButtonConnected);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.buttonSettings);
			this.Controls.Add(this.textBoxInput);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panelContextLobby);
			this.Controls.Add(this.panelContextGame);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.main.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.contextMenuStripPendingGames.ResumeLayout(false);
			this.panelContextLobby.ResumeLayout(false);
			this.panelContextLobby.PerformLayout();
			this.panelContextGame.ResumeLayout(false);
			this.panelContextGame.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.TabPage main;
		private System.Windows.Forms.RichTextBox textBoxMain;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ListBox listBoxPendingGames;
		private System.Windows.Forms.ListBox listBoxStartedGames;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonSettings;
		private System.Windows.Forms.Button buttonSign;
		private System.Windows.Forms.Button buttonConfirm;
		private System.Windows.Forms.Panel panelContextLobby;
		private System.Windows.Forms.ListBox listBoxOnlineUsers;
		private System.Windows.Forms.Panel panelContextGame;
		private System.Windows.Forms.Button buttonResultDraw;
		private System.Windows.Forms.Button buttonResultDire;
		private System.Windows.Forms.Button buttonResultRadiant;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listBoxDire;
		private System.Windows.Forms.ListBox listBoxRadiant;
		private System.Windows.Forms.Button buttonUnsign;
		private System.Windows.Forms.Button buttonCreateLobby;
		private System.Windows.Forms.RadioButton radioButtonConnected;
		private System.Windows.Forms.RadioButton radioButtonMatchmaking;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripPendingGames;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
	}
}

