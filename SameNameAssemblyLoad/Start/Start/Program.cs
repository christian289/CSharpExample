using System.Reflection;

AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

// 어셈블리 A와 B에서 동일한 이름의 메서드를 호출합니다.
dynamic assemblyAInstance = CreateInstance(@"D:\SameNameAssemblyLoad\A\ClassLibrary1\bin\Debug\net7.0", "ClassLibrary1.Class1");
assemblyAInstance.HelloWorld();

dynamic assemblyBInstance = CreateInstance(@"D:\SameNameAssemblyLoad\B\ClassLibrary1\bin\Debug\net7.0", "ClassLibrary1.Class1");
assemblyBInstance.HelloWorld();

dynamic assemblyCInstance = CreateInstance(@"D:\SameNameAssemblyLoad\C\ClassLibrary1\bin\Debug\net7.0", "ClassLibrary1.Class1");
assemblyCInstance.HelloWorld();

dynamic assemblyDInstance = CreateInstance(@"D:\SameNameAssemblyLoad\D\ClassLibrary1\bin\Debug\net7.0", "ClassLibrary1.Class1");
assemblyDInstance.HelloWorld();

Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
{
    AssemblyName assemblyName = new(args.Name);
    string assemblyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName.Name);

    if (!Directory.Exists(assemblyFolder))
        return null;

    string assemblyPath = Path.Combine(assemblyFolder, assemblyName.Name + ".dll");
    if (!File.Exists(assemblyPath))
        return null;

    return Assembly.LoadFrom(assemblyPath);
}

object CreateInstance(string folderName, string typeName)
{
    string assemblyName = typeName[..typeName.LastIndexOf('.')];
    string assemblyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
    string assemblyPath = Path.Combine(assemblyFolder, assemblyName + ".dll");

    byte[] assemblyBytes = File.ReadAllBytes(assemblyPath);
    Assembly assembly = Assembly.Load(assemblyBytes);
    AssemblyName an = assembly.GetName();
    an.Name = assemblyPath.Replace(Path.DirectorySeparatorChar.ToString(), string.Empty);
    Assembly modifiedAssembly = Assembly.Load(assemblyBytes);

    Type type = modifiedAssembly.GetType(typeName, true);

    return Activator.CreateInstance(type);
}
