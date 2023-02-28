using System.Diagnostics;

namespace WindowsFormsThreadIdTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Button button1 = new()
            {
                Text = "Default",
                Dock = DockStyle.Fill
            };
            button1.Click += async (sender, e) =>
            {
                Debug.WriteLine("button1 시작");
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
                await Task.Run(() => Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}"));
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
            };

            Button button2 = new()
            {
                Text = "ConfigureAwait(true)",
                Dock = DockStyle.Fill
            };
            button2.Click += async (sender, e) =>
            {
                Debug.WriteLine("button2 시작");
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
                await Task.Run(() => Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}")).ConfigureAwait(true);
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
            };

            Button button3 = new()
            {
                Text = "ConfigureAwait(false)",
                Dock = DockStyle.Fill
            };
            button3.Click += async (sender, e) =>
            {
                Debug.WriteLine("button3 시작");
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
                await Task.Run(() => Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}")).ConfigureAwait(false);
                Debug.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
            };

            TableLayoutPanel tableLayoutPanel = new();
            tableLayoutPanel.SuspendLayout();
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            tableLayoutPanel.Controls.Add(button1, 0, 0);
            tableLayoutPanel.Controls.Add(button2, 1, 0);
            tableLayoutPanel.Controls.Add(button3, 2, 0);
            tableLayoutPanel.ResumeLayout();
            this.Controls.Add(tableLayoutPanel);
        }
    }
}