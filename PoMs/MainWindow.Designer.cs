namespace PoMs
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.paranoidModeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendProcessButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseProcessButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.psscanner = new System.ComponentModel.BackgroundWorker();
            this.blockcountMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PoMs";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blockcountMenu,
            this.paranoidModeButton,
            this.suspendProcessButton,
            this.toolStripSeparator1,
            this.releaseProcessButton,
            this.toolStripSeparator2,
            this.exitButton});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(183, 126);
            this.contextMenu.Text = "PsMs V 0.1";
            // 
            // paranoidModeButton
            // 
            this.paranoidModeButton.CheckOnClick = true;
            this.paranoidModeButton.ForeColor = System.Drawing.Color.DarkRed;
            this.paranoidModeButton.Name = "paranoidModeButton";
            this.paranoidModeButton.Size = new System.Drawing.Size(182, 22);
            this.paranoidModeButton.Text = "Paranoid mode";
            this.paranoidModeButton.ToolTipText = "Blocks all processes!!! Also the command prompt.";
            // 
            // suspendProcessButton
            // 
            this.suspendProcessButton.CheckOnClick = true;
            this.suspendProcessButton.Name = "suspendProcessButton";
            this.suspendProcessButton.Size = new System.Drawing.Size(182, 22);
            this.suspendProcessButton.Text = "Suspend processes";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // releaseProcessButton
            // 
            this.releaseProcessButton.Name = "releaseProcessButton";
            this.releaseProcessButton.Size = new System.Drawing.Size(182, 22);
            this.releaseProcessButton.Text = "Release all processes";
            this.releaseProcessButton.Click += new System.EventHandler(this.releaseProcessButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // exitButton
            // 
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(182, 22);
            this.exitButton.Text = "Exit";
            this.exitButton.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // psscanner
            // 
            this.psscanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.psscanner_DoWork);
            // 
            // blockcountMenu
            // 
            this.blockcountMenu.Name = "blockcountMenu";
            this.blockcountMenu.Size = new System.Drawing.Size(182, 22);
            this.blockcountMenu.Text = "Blocked tasks:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 124);
            this.Name = "MainWindow";
            this.Text = "PoMs";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitButton;
        private System.ComponentModel.BackgroundWorker psscanner;
        private System.Windows.Forms.ToolStripMenuItem suspendProcessButton;
        private System.Windows.Forms.ToolStripMenuItem releaseProcessButton;
        private System.Windows.Forms.ToolStripMenuItem paranoidModeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem blockcountMenu;
    }
}

