using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAddScopedVsAddTransientTest.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public virtual AbsItemViewModel ItemViewModel { get; set; }

        public MainWindowViewModel()
        {

        }

        public void AViewChange()
        {
            ItemViewModel = (AViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        }

        public void BViewChange()
        {
            ItemViewModel = (BViewModel)App.ServiceProvider.GetRequiredService(ViewModelSource.GetPOCOType(typeof(BViewModel)));
        }
    }
}
