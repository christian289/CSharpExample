using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleWriteThreadTestWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Click 메서드 진입: {Thread.CurrentThread.ManagedThreadId}");

            await DoSome();

            Console.WriteLine($"Click 메서드 종료: {Thread.CurrentThread.ManagedThreadId}");
        }

        private Task DoSome()
        {
            Console.WriteLine($"DoSome: {Thread.CurrentThread.ManagedThreadId}");

            return Task.Run(() => {
                Thread.Sleep(100);
                Console.WriteLine($"Inner: {Thread.CurrentThread.ManagedThreadId}");
            });
        }
    }
}
