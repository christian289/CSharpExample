using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTabControlDynamicTabItemTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow_CodeBehind : Window
    {
        public MainWindow_CodeBehind()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 2; i++)
            {
                tbctlTest.Items.Add(new TabItem
                {
                    Name = $"tabitem{i}",
                    Header = $"tabitem{i}"
                });
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in tbctlTest.Items)
            {
                TabItem tabitem = item as TabItem;
                tabitem.Template = null;
            }

            tbctlTest.Items.Clear();
        }
    }
}
