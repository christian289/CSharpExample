using DevExpress.Mvvm.DataAnnotations;

namespace DxMvvmWpfDataTemplateNavigationExample.ViewModels
{
    [POCOViewModel]
    public class SubView1ViewModel
    {
        public SubView1ViewModel()
        {

        }

        public virtual string Message { get; set; } = nameof(SubView1ViewModel);
    }
}
