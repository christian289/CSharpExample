using System;
using System.Threading;
using System.Windows.Forms;

namespace WinformUIThreadIsThreadPoolThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool check = Thread.CurrentThread.IsThreadPoolThread;
            label1.Text = check.ToString();
        }
    }
}
