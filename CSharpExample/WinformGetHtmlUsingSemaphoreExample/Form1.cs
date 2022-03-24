using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemaphoreCrawlingTest
{
    public partial class Form1 : Form
    {
        public static HttpClient httpClient;
        public const int SEMAPHORE_LIMIT = 3;

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient
            {
                DefaultRequestVersion = new Version(2, 0),
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string uriStr = textBox1.Text;

            if (!Uri.IsWellFormedUriString(uriStr, UriKind.Absolute) ||
                !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri uri) ||
                uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                MessageBox.Show("올바른 URI 형식이 아닙니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            SemaphoreSlim semaphore = new(SEMAPHORE_LIMIT);
            List<Task> tasks = new();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();

                    try
                    {
                        string rawHtml = await httpClient.GetStringAsync(uriStr);

                        if (!string.IsNullOrEmpty(rawHtml))
                        {
                            HtmlAgilityPack.HtmlDocument mydoc = new();
                            mydoc.LoadHtml(rawHtml);

                            await Task.Delay(5000);
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
