using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppHost().Init().Start("http://*:8088/api/");
            Console.WriteLine("ServiceStack SelfHost listening at http://localhost:8088/api");
            Console.ReadLine();
        }
    }
}
