using System;
using System.Windows.Forms;

namespace caro
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerVsPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerVsComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnectLan = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.prgTimeLine = new System.Windows.Forms.ProgressBar();
            this.txtPlayer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ptbDaiDien = new System.Windows.Forms.PictureBox();
            this.pnlBanCo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbDaiDien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1031, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerVsPlayerToolStripMenuItem,
            this.playerVsComputerToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // playerVsPlayerToolStripMenuItem
            // 
            this.playerVsPlayerToolStripMenuItem.Name = "playerVsPlayerToolStripMenuItem";
            this.playerVsPlayerToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.playerVsPlayerToolStripMenuItem.Text = "Player vs Player";
            this.playerVsPlayerToolStripMenuItem.Click += new System.EventHandler(this.playerVsPlayerToolStripMenuItem_Click);
            // 
            // playerVsComputerToolStripMenuItem
            // 
            this.playerVsComputerToolStripMenuItem.Name = "playerVsComputerToolStripMenuItem";
            this.playerVsComputerToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.playerVsComputerToolStripMenuItem.Text = "Player vs Computer";
            this.playerVsComputerToolStripMenuItem.Click += new System.EventHandler(this.playerVsComputerToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.restartToolStripMenuItem.Text = "Restart ";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnConnectLan);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.prgTimeLine);
            this.groupBox1.Controls.Add(this.txtPlayer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ptbDaiDien);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(0, 281);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(271, 351);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lượt đi";
            // 
            // btnConnectLan
            // 
            this.btnConnectLan.Location = new System.Drawing.Point(61, 267);
            this.btnConnectLan.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnectLan.Name = "btnConnectLan";
            this.btnConnectLan.Size = new System.Drawing.Size(141, 46);
            this.btnConnectLan.TabIndex = 5;
            this.btnConnectLan.Text = "LAN";
            this.btnConnectLan.UseVisualStyleBackColor = true;
            this.btnConnectLan.Click += new System.EventHandler(this.btnConnectLan_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(8, 193);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.Multiline = true;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(247, 47);
            this.txtIP.TabIndex = 4;
            // 
            // prgTimeLine
            // 
            this.prgTimeLine.Location = new System.Drawing.Point(8, 135);
            this.prgTimeLine.Margin = new System.Windows.Forms.Padding(4);
            this.prgTimeLine.Name = "prgTimeLine";
            this.prgTimeLine.Size = new System.Drawing.Size(248, 28);
            this.prgTimeLine.TabIndex = 3;
            // 
            // txtPlayer
            // 
            this.txtPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPlayer.Location = new System.Drawing.Point(123, 78);
            this.txtPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayer.Name = "txtPlayer";
            this.txtPlayer.Size = new System.Drawing.Size(132, 22);
            this.txtPlayer.TabIndex = 3;
            this.txtPlayer.TextChanged += new System.EventHandler(this.txtPlayer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên người chơi";
            // 
            // ptbDaiDien
            // 
            this.ptbDaiDien.Location = new System.Drawing.Point(8, 38);
            this.ptbDaiDien.Margin = new System.Windows.Forms.Padding(4);
            this.ptbDaiDien.Name = "ptbDaiDien";
            this.ptbDaiDien.Size = new System.Drawing.Size(89, 78);
            this.ptbDaiDien.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbDaiDien.TabIndex = 0;
            this.ptbDaiDien.TabStop = false;
            // 
            // pnlBanCo
            // 
            this.pnlBanCo.Location = new System.Drawing.Point(305, 49);
            this.pnlBanCo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBanCo.Name = "pnlBanCo";
            this.pnlBanCo.Size = new System.Drawing.Size(688, 582);
            this.pnlBanCo.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::caro.Properties.Resources.caro__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 49);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(263, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 693);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlBanCo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbDaiDien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            

                Application.Exit();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar prgTimeLine;
        private System.Windows.Forms.TextBox txtPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ptbDaiDien;
        private System.Windows.Forms.Panel pnlBanCo;
        private System.Windows.Forms.ToolStripMenuItem playerVsPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerVsComputerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.Button btnConnectLan;
        private System.Windows.Forms.TextBox txtIP;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
    }
}

