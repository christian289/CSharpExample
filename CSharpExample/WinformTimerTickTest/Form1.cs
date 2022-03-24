using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformTimerTick
{
    public partial class Form1 : Form
    {
        int TimerInterval = 55; // MSDN 권고사항 : Winform Timer는 55ms가 최소 보장된 정확도이며, 그 밑의 Interval을 사용하면 정확하지 않을 수 있다. 고해상도 타이머를 원한다면 Timers.Timer를 사용하라.
        // https://docs.microsoft.com/ko-kr/dotnet/api/system.windows.forms.timer?view=netframework-4.8

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnWorkStart1_Click(object sender, EventArgs e)
        {
            timer1.Interval = TimerInterval; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("무거운 Tick 시작");

            for (long i = 0; i < 100000000000000; i++)
            {
                label1.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }
        }

        private void BtnWorkStart2_Click(object sender, EventArgs e)
        {
            timer2.Interval = TimerInterval; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("무거운 Enable 추가 Tick 시작");

            timer2.Enabled = false;

            for (long i = 0; i < 100000000000000; i++)
            {
                label2.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }

            timer2.Enabled = true;
        }

        private void BtnWorkStart3_Click(object sender, EventArgs e)
        {
            timer3.Interval = TimerInterval; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("가벼운 Tick 시작");

            for (long i = 0; i < 100; i++)
            {
                label3.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }
        }

        private void BtnWorkStart4_Click(object sender, EventArgs e)
        {
            timer4.Interval = TimerInterval; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer4.Enabled = true;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("가벼운 Enable 추가 Tick 시작");

            timer4.Enabled = false;

            for (long i = 0; i < 100; i++)
            {
                label4.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }

            timer4.Enabled = true;
        }

        private void BtnWorkStart5_Click(object sender, EventArgs e)
        {
            Task.Run(() => {
                while (true)
                {
                    Console.WriteLine("무거운 작업 worker thread로 시작");

                    for (long i = 0; i < 100000000000000; i++)
                    {
                        Invoke(new MethodInvoker(delegate () { label5.Text = i.ToString(); })); // 다른 Worker Thread 상에서 동작하므로 Invoke가 필요하다.
                    }

                    Task.Delay(10);
                }
            });
        }
    }
}
