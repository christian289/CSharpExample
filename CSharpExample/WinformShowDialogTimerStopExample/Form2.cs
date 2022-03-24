using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformShowDialogTimerStopExample
{
    public partial class Form2 : Form
    {
        private Form1 parentForm;

        public Form2()
        {
            InitializeComponent();
        }

        public void SetParentForm(Form1 parent)
        {
            parentForm = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.timer1.Enabled = false;
            //parentForm.timer2.Enabled = false;
        }
    }
}
