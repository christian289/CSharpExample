using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace BackgroundWorkerSample
{
    public partial class Form1 : Form
    {
        const int MAX = 100;
        int progress1 = default;
        int progress2 = default;
        int progress3 = default;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;

            progress1 = default;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = MAX;
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 0;
            label1.Text = string.Empty;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // 워커스레드 (Invoke 필요)
            for (int i = 1; i <= MAX; i++)
            {
                progress1 = i;
                int percentComplete = (int)Math.Round((decimal)(100 * progress1) / MAX);
                Thread.Sleep(100);
                backgroundWorker1.ReportProgress(percentComplete);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // UI 스레드 (Invoke 필요없음.)
            label1.Text = $"백그라운드워커 1 동작 중...{e.ProgressPercentage}%";
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // UI 스레드 (Invoke 필요없음.)
            label1.Text = $"백그라운드워커 1 동작 완료.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy) return;

            progress2 = default;
            progressBar2.Minimum = 0;
            progressBar2.Maximum = MAX;
            progressBar2.Step = 1;
            progressBar2.Style = ProgressBarStyle.Blocks;
            progressBar2.Value = 0;
            label2.Text = string.Empty;
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= MAX; i++)
            {
                progress2 = i;
                int percentComplete = (int)Math.Round((decimal)(100 * progress2) / MAX);
                Thread.Sleep(100);
                backgroundWorker2.ReportProgress(percentComplete);
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label2.Text = $"백그라운드워커 2 동작 중...{e.ProgressPercentage}%";
            progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label2.Text = $"백그라운드워커 2 동작 완료.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backgroundWorker3.IsBusy) return;

            progress3 = default;
            progressBar3.Minimum = 0;
            progressBar3.Maximum = MAX;
            progressBar3.Step = 1;
            progressBar3.Style = ProgressBarStyle.Blocks;
            progressBar3.Value = 0;
            label3.Text = string.Empty;
            backgroundWorker3.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= MAX; i++)
            {
                progress3 = i;
                int percentComplete = (int)Math.Round((decimal)(100 * progress3) / MAX);
                Thread.Sleep(100);
                backgroundWorker3.ReportProgress(percentComplete);
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label3.Text = $"백그라운드워커 3 동작 중...{e.ProgressPercentage}%";
            progressBar3.Value = e.ProgressPercentage;
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label3.Text = $"백그라운드워커 3 동작 완료.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
