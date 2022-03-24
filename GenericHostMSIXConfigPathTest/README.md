# GenericHostMSIXConfigPathTest

- WPF에서 Generic Host를 사용할 때 설정파일을 ConfigureAppConfiguration() 메서드 내부에서 IConfigurationBuilder에 AddJsonFile(), AddXmlFile(), AddYamlFile() 등의 메서드를 이용해 설정값을 불러와서 사용합니다.

- 이 때 HostBuilderContext.HostingEnvironment.ContentRootPath 를 이용하면, 런타임에서 실행 경로값을 가져와서, 빌드 액션을 '항상 복사' 또는 '새 버전이면 복사'로 설정된 설정파일을 가져올 수 있습니다.

- 하지만 MSIX로 실행할 경우 HostBuilderContext.HostingEnvironment.ContentRootPath 가 C:\Windows\System32 또는 C:\Windows\SysWOW64 로 바뀌게 됩니다.

- 따라서 MSIX에서는 HostBuilderContext.HostingEnvironment.ContentRootPath 를 설정파일을 불러오는 목적으로는 사용할 수 없고, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) 로 대체해서 사용해야 합니다.