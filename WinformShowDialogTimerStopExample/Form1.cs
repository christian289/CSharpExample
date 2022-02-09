using System;
using System.Windows.Forms;

namespace WinformShowDialogTimerStopExample
{
    public partial class Form1 : Form
    {
        private int label1int = 0;
        private int label2int = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = label1int++.ToString();

            label1int = label1int > 10 ? 0 : label1int;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = label2int++.ToString();
            label2int = label2int > 10 ? 0 : label2int;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using Form2 form = new();
            form.SetParentForm(this);
            form.ShowDialog();
        }
    }
}
