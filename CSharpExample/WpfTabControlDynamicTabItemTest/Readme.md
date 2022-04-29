# TabControl의 TabItem을 동적으로 생성할 때 발생하는 바인딩 에러 예제

## 예제 사용방법

### MainWindow_XAML 바인딩 에러 테스트

1. App.xaml 상단을 아래와 같이 주석 변경

```xml
<!--<Application x:Class="WpfTabControlDynamicTabItemTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfTabControlDynamicTabItemTest"
             StartupUri="MainWindow_CodeBehind.xaml">-->
<Application x:Class="WpfTabControlDynamicTabItemTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfTabControlDynamicTabItemTest"
             StartupUri="MainWindow_XAML.xaml">
```

2. 왼쪽 버튼을 눌러 tab 생성

3. 오른쪽 버튼을 눌러 바인딩 에러 확인

4. for문의 TabItem 생성 개수를 변경해도 무조건 4개의 바인딩 에러 발생

### MainWindow_CodeBehind 바인딩 에러 테스트

1. App.xaml 상단을 아래와 같이 주석 변경

```xml
<Application x:Class="WpfTabControlDynamicTabItemTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfTabControlDynamicTabItemTest"
             StartupUri="MainWindow_CodeBehind.xaml">
<!--<Application x:Class="WpfTabControlDynamicTabItemTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfTabControlDynamicTabItemTest"
             StartupUri="MainWindow_XAML.xaml">-->
```

2. MainWindow_CodeBehind.xaml.cs 의 이벤트를 아래와 같이 변경

```cs
private void btnRemove_Click(object sender, RoutedEventArgs e)
{
    //foreach (var item in tbctlTest.Items)
    //{
    //    TabItem tabitem = item as TabItem;
    //    tabitem.Template = null;
    //}

    tbctlTest.Items.Clear();
}
```

3. 왼쪽 버튼을 눌러 tab 생성

4. 오른쪽 버튼을 눌러 바인딩 에러 확인

5. for문의 TabItem 생성 개수를 변경해도 무조건 4개의 바인딩 에러 발생

6. MainWindow_CodeBehind.xaml.cs 의 이벤트를 아래와 같이 변경

```cs
private void btnRemove_Click(object sender, RoutedEventArgs e)
{
    foreach (var item in tbctlTest.Items)
    {
        TabItem tabitem = item as TabItem;
        tabitem.Template = null;
    }

    tbctlTest.Items.Clear();
}
```

7. 왼쪽 버튼을 눌러 tab 생성

4. 오른쪽 버튼을 눌러 **바인딩 에러가 발생하지 않는 것을 확인**

## 결론

이유는 모르겠지만 무조건 바인딩 에러가 발생하면 4개가 고정되어 발생함.
XAML과 바인딩 형태라면 Template에 null값을 줄 수 없기 때문에 해결도 불가능함.

## 해결방법

?????? 아시는 분 알려주세요...