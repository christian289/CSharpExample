using System.Diagnostics;

namespace WindowsFormsThreadIdTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Button button = new();
            button.Click += button1_Click;
            this.Controls.Add(button);
        }

        private async void button1_Click(object? sender, EventArgs e)
        {
            Debug.WriteLine($"Click 메서드 진입: {Environment.CurrentManagedThreadId}");

            await Task.Run(() => Debug.WriteLine($"Inner: {Environment.CurrentManagedThreadId}"));

            Debug.WriteLine($"Click 메서드 종료: {Environment.CurrentManagedThreadId}");
        }
    }
}