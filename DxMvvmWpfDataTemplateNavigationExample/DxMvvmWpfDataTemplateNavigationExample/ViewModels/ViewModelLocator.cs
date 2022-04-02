using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;

namespace DxMvvmWpfDataTemplateNavigationExample.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => (MainWindowViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        public SubView1ViewModel SubView1ViewModel => (SubView1ViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(SubView1ViewModel)));
        public SubView2ViewModel SubView2ViewModel => (SubView2ViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(SubView2ViewModel)));
        public SubView3ViewModel SubView3ViewModel => (SubView3ViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(SubView3ViewModel)));
    }
}
