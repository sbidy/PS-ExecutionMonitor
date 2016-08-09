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
                    if (start < start.AddDays(1))
                    {
                        monitor.getEventOperational();
                        monitor.getEventNomal();
                        if (monitor.runcount > count)
                        {
                            notifyIcon1.BalloonTipText = "PowerShell command executed!\nCount: "+ monitor.runcount + "\nStart date: "+start.ToShortDateString();
                            notifyIcon1.BalloonTipTitle = "User: " + monitor.user;
                            notifyIcon1.ShowBalloonTip(5000);
                        }
                        
                        if (monitor.runcount >= this.trashold)
                        {
                            MessageBox.Show("To many PowerShell events detected! Are you hacked?","Threshold reached!!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                        if (monitor.openps)
                        {
                            notifyIcon1.BalloonTipText = "PowerShell command was opened!\nStart date: " + start.ToShortDateString();
                            notifyIcon1.BalloonTipTitle = "User: " + monitor.user;
                            notifyIcon1.ShowBalloonTip(5000);
                            monitor.openps = false;
                        }
                        count = monitor.runcount;
                    }
                    else
                    {
                        monitor.runcount = 0;
                        start = DateTime.Now;
                    }
                    Thread.Sleep(SLEEPTIME);
                }
            }
            
        }
    }
}
