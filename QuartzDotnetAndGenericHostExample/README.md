# QuartzDotnetAndGenericHost

Quartz.NET과 Generic Host를 이용한 형태 예제입니다.

1. Program.cs에서 Generic Host의 설정파일 불러오는 방법
2. Quartz.net에서 trigger와 job을 설정하는 방법
3. 전처리 명령어를 이용하는 방법
4. EF Core의 DB Context를 설정하는 방법
5. Service 생성방법
6. IHttpClientFactory를 이용하여 HttpClient와 HttpClientHandler로 정의한 HttpClient를 사용하는 방법
7. List<<Task>> 와 SemaphoreSlim을 이용하여 멀티 태스킹 시 쓰로틀링 제어하는 방법

위의 예제를 알 수 있습니다.
EF Core의 DB Context는 Visual Studio를 설치할 때 생기는 기본 Local DB를 이용했으며 스케폴딩하는 명령어는 아래와 같습니다.

> scaffold-dbcontext -Connection "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuartzDotnetAndGenericHost;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DAO -Context MyContext
