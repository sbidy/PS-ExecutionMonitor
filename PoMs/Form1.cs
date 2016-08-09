using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PoMs
{
    public partial class Form1 : Form
    {
        PowerMon monitor = new PowerMon();
        private int count = 0;
        private DateTime start = DateTime.Now;
        private int SLEEPTIME = 10000;
        private int trashold = 5;

        public Form1()
        {
            InitializeComponent();
            psscanner.WorkerSupportsCancellation = true;
            startScanner();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (psscanner.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                psscanner.CancelAsync();
            }
            Application.Exit();
        }
        private void startScanner()
        {
            if (psscanner.IsBusy != true)
            {
                // Start the asynchronous operation.
                psscanner.RunWorkerAsync();
            }
        }

        private void psscanner_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    PSEventEntry entry = monitor.getPSEvent();
                    if (start < start.AddDays(1))
                    {
                        if (entry.malware)
                        {
                            ceateMessageBox("Suspicious script block logged !!! Are u hacked?", "Suspicious script blocked");
                        }
                        if (entry.runcount > count)
                        {
                            createBalloon("User: " + entry.username, "PowerShell command executed!\nCount: " + entry.runcount + "\nDate logged: " + entry.datetime.ToString());
                        }
                        if (entry.runcount >= this.trashold)
                        {
                            ceateMessageBox("To many PowerShell events detected! Are you hacked?", "Threshold reached!!");
                        }
                        if (entry.opencommand)
                        {
                            createBalloon("User;" + entry.username, "PowerShell command was opened!\nDate logged: " + entry.datetime.ToString());
                        }
                        count = entry.runcount;
                    }
                    else
                    {
                        entry.runcount = 0;
                        start = DateTime.Now;
                    }
                    Thread.Sleep(SLEEPTIME);
                }
            }
        }
        private void createBalloon(string titel, string text)
        {
            notifyIcon1.BalloonTipText = text;
            notifyIcon1.BalloonTipTitle = titel;
            notifyIcon1.ShowBalloonTip(5000);
        }
        private void ceateMessageBox(string titel, string text)
        {
            MessageBox.Show(text,titel, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
