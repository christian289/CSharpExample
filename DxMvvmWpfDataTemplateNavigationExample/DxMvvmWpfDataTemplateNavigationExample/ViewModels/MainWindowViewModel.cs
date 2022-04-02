using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxMvvmWpfDataTemplateNavigationExample.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {

        }

        public virtual string ViewName { get; set; }

        public virtual void Navigate(string viewName)
        {
            ViewName = viewName;
        }
    }
}
