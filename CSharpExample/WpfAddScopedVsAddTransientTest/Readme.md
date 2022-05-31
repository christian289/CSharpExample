# WpfAddScopedVsAddTransientTest

Generic host를 사용하면서 ASP.NET Core에서는 AddScoped와 AddTransient가 구분이 비교적 잘 되는 편이다.

## ASP.NET Core

### AddScoped

- 하나의 Session이 생성되면 그 Session이 만료될때까지는 세션 채결 시 한번 생성된 이후 계속 같은 인스턴스를 재사용하게 된다.

### AddTransient

- 세션과 관계없이 해당 서비스를 호출할 때마다 새로운 인스턴스가 생성된다.

## WPF

- WPF는 네트워크 환경이 아니기 때문에 어디서 접속이 새로 들어오는 것은 없지만, View를 하나의 Client라고 봤을 때 같은 개념을 적용할 수 있다. 이 부분이 지금 이 예제 소스에서 테스트하려는 목적이다.

### AddScoped

1. App.xaml.cs에서 ConfigureServices 메서드 아래 소스 코드 부분을 다음과 같이 바꾼다.

```csharp

host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
    })
    .ConfigureServices((context, service) =>
    {
        service.AddScoped(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        service.AddScoped(ViewModelSource.GetPOCOType(typeof(BViewModel)));
        service.AddScoped(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        //service.AddTransient(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        //service.AddTransient(ViewModelSource.GetPOCOType(typeof(BViewModel)));
        //service.AddTransient(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        service.AddTransient<MainWindow>();
    })
    .Build();

```

2. 아래 4가지 생성자에 브레이크 포인트를 찍는다.

- AViewModel.cs
- BViewModel.cs
- AView.xaml.cs
- BView.xaml.cs

3. 프로그램을 실행하여 AView가 써있는 버튼, BView가 써있는 버튼을 각각 클릭하면서 브레이크 포인트에 걸리는 순서를 확인한다.

4. 다시 AView가 써있는 버튼을 눌렀을 때 AView.xaml.cs의 생성자에만 걸리고, AViewModel.cs의 생성자에는 커서가 걸리지 않는 것을 확인한다.

4. 다시 BView가 써있는 버튼을 눌렀을 때 BView.xaml.cs의 생성자에만 걸리고, BViewModel.cs의 생성자에는 커서가 걸리지 않는 것을 확인한다.

### AddTransient

1. App.xaml.cs에서 ConfigureServices 메서드 아래 소스 코드 부분을 다음과 같이 바꾼다.

```csharp

host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
    })
    .ConfigureServices((context, service) =>
    {
        //service.AddScoped(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        //service.AddScoped(ViewModelSource.GetPOCOType(typeof(BViewModel)));
        //service.AddScoped(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        service.AddTransient(ViewModelSource.GetPOCOType(typeof(AViewModel)));
        service.AddTransient(ViewModelSource.GetPOCOType(typeof(BViewModel)));
        service.AddTransient(ViewModelSource.GetPOCOType(typeof(MainWindowViewModel)));
        service.AddTransient<MainWindow>();
    })
    .Build();

```

2. 아래 4가지 생성자에 브레이크 포인트를 찍는다.

- AViewModel.cs
- BViewModel.cs
- AView.xaml.cs
- BView.xaml.cs

3. 프로그램을 실행하여 AView가 써있는 버튼, BView가 써있는 버튼을 각각 클릭하면서 브레이크 포인트에 걸리는 순서를 확인한다.

4. 다시 AView가 써있는 버튼을 눌렀을 때 AViewModel.cs와 AView.xaml.cs의 생성자 둘 다 브레이크 포인트가 걸리는 것을 확인한다.

4. 다시 BView가 써있는 버튼을 눌렀을 때 BView.xaml.cs와 BView.xaml.cs의 생성자 둘 다 브레이크 포인트가 걸리는 것을 확인한다.

# 결론

- WPF에서도 AddTransient와 AddScoped의 차이점을 확인할 수 있었고, 똑같은 View가 새로 생성더라도 기존의 ViewModel을 사용하고 싶다면 AddScoped를 사용해 타입을 등록해야한다는 것을 기억한다.
