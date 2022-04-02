using System.Windows;
using System.Windows.Controls;

namespace DxMvvmWpfDataTemplateNavigationExample.DataTemplateSelectors
{
    public class NavigationDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            string ucName = item as string;

            if (ucName is null) ucName = "SubView1"; // build 할 때 유지 디자이너를 위해 기본값을 넣어줌.

            FrameworkElement element = container as FrameworkElement;

            return (DataTemplate)element.FindResource($"{ucName}Template");
        }
    }
}
