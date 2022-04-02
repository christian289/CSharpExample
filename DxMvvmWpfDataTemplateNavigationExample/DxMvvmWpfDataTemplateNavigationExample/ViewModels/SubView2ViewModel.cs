using DevExpress.Mvvm.DataAnnotations;

namespace DxMvvmWpfDataTemplateNavigationExample.ViewModels
{
    [POCOViewModel]
    public class SubView2ViewModel
    {
        public SubView2ViewModel()
        {

        }

        public virtual string Message { get; set; } = nameof(SubView2ViewModel);
    }
}
