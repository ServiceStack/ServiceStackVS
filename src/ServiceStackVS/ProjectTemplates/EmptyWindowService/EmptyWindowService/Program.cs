using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace $safeprojectname$
{
    class Program
    {
        private const string ListeningOn = "http://localhost:8088/";

        static void Main(string[] args)
        {
            var appHost = new AppHost();
            //Allow you to debug your Windows Service while you're deleloping it. 
#if DEBUG
            Console.WriteLine("Running WinServiceAppHost in Console mode");
            try
            {
                appHost.Init();
                appHost.Start(ListeningOn);

                Console.WriteLine("Press <CTRL>+C to stop.");
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}: {1}", ex.GetType().Name, ex.Message);
                throw;
            }
            finally
            {
                appHost.Stop();
            }

            Console.WriteLine("WinServiceAppHost has finished");

#else
			//When in RELEASE mode it will run as a Windows Service with the code below

			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new WinService(appHost, ListeningOn) 
			};
			ServiceBase.Run(ServicesToRun);
#endif

            Console.ReadLine();
        }
    }
}
