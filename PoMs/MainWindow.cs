/*
MIT License

Copyright (c) 2016 Stephan Traub, audius GmbH

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace PoMs
{
    public partial class MainWindow : Form
    {
        PowerMon monitor = new PowerMon();
        ProcessController controller = new ProcessController();
        private int count = 0;
        private DateTime start = DateTime.Now;
        private int SLEEPTIME = 2000;
        private int trashold = 5;
        private List<int> suspendetPS = new List<int>(100);

        public MainWindow()
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
                            porcessManager(entry.processID);
                        }
                        if (entry.runcount > count)
                        {
                            createBalloon("User: " + entry.username, "PowerShell command executed!\nCount: " + entry.runcount + "\nDate logged: " + entry.datetime.ToString());
                            porcessManager(entry.processID);
                        }
                        if (entry.runcount >= this.trashold)
                        {
                            ceateMessageBox("To many PowerShell events detected! Are you hacked?", "Threshold reached!!");
                            porcessManager(entry.processID);
                        }
                        if (entry.opencommand && paranoidModeButton.Checked)
                        {
                            createBalloon("User: " + entry.username, "PowerShell command was opened!\nDate logged: " + entry.datetime.ToString());
                            porcessManager(entry.processID);
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

        private void porcessManager(int pid)
        {
            try
            {
                if (pid > 0 && !Process.GetProcessById(pid).HasExited && suspendProcessButton.Checked)
                {
                    controller.SuspendProcess(pid);
                    suspendetPS.Add(pid);
                }
            }
            catch (ArgumentException ex)
            {
                // do nothing - process already closed
            }
        }

        private void createBalloon(string titel, string text)
        {
            notifyIcon.BalloonTipText = text;
            notifyIcon.BalloonTipTitle = titel;
            notifyIcon.ShowBalloonTip(5000);
        }
        private void ceateMessageBox(string titel, string text)
        {
            MessageBox.Show(text,titel, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        private void releaseProcessButton_Click(object sender, EventArgs e)
        {
            foreach (int pid in suspendetPS)
            {
                try
                {
                    if (!Process.GetProcessById(pid).HasExited)
                    {
                        controller.ResumeProcess(pid);
                    }
                }
                catch (ArgumentException ex)
                {
                    // do nothing - process already closed
                }
            }
        }
    }
}
