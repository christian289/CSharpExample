using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAddScopedVsAddTransientTest.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => (MainWindowViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        public AViewModel AViewModel => (AViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        public BViewModel BViewModel => (BViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(BViewModel)));
    }
}
