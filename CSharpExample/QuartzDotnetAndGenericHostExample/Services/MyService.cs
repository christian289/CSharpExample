using QuartzDotnetAndGenericHost.Models.DAO;
using System;

namespace QuartzDotnetAndGenericHost.Services
{
    public class MyService
    {
        private readonly MyContext dbContext;

        public MyService(MyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void MyVoidMethod()
        {
            Console.WriteLine("Void 메서드 실행!!");
        }

        public int MyIntMethod()
        {
            Console.WriteLine("Int 메서드 실행!!");

            return default;
        }
    }
}
