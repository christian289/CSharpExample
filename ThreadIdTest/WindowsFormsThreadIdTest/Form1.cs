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
            Debug.WriteLine($"Click �޼��� ����: {Environment.CurrentManagedThreadId}");

            await Task.Run(() => Debug.WriteLine($"Inner: {Environment.CurrentManagedThreadId}"));

            Debug.WriteLine($"Click �޼��� ����: {Environment.CurrentManagedThreadId}");
        }
    }
}