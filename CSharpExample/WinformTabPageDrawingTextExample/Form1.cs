using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinformTabPageDrawingTextExample
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetTextCharacterExtra(IntPtr hdc, int nCharExtra);
        
        public Form1()
        {
            InitializeComponent();
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(100, 25);

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += TabControl1_DrawItem;
        }

        // 참고링크 : https://social.msdn.microsoft.com/Forums/vstudio/en-US/fbeaa99e-8bf3-4850-b3b7-24053424bdad/character-spacing?forum=csharpgeneral
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            IntPtr hdc = e.Graphics.GetHdc();
            SetTextCharacterExtra(hdc, 5);
            e.Graphics.ReleaseHdc(hdc);

            IDeviceContext HDC = Graphics.FromHdc(hdc);

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                Rectangle TabPageTextArea = tabControl1.GetTabRect(i);
                TextRenderer.DrawText(
                    dc: HDC,
                    text: string.Format("Tab{0}", i),
                    font: new Font("맑은 고딕", 11),
                    pt: new Point(TabPageTextArea.X, TabPageTextArea.Y),
                    foreColor: SystemColors.ControlText
                    );
            }
        }
    }
}
