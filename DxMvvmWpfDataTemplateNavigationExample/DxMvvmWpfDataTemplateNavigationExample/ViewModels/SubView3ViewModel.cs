using DevExpress.Mvvm.DataAnnotations;

namespace DxMvvmWpfDataTemplateNavigationExample.ViewModels
{
    [POCOViewModel]
    public class SubView3ViewModel
    {
        public SubView3ViewModel()
        {

        }

        public virtual string Message { get; set; } = nameof(SubView3ViewModel);
    }
}
