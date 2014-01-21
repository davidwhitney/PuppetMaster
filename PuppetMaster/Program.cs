using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;
using SimpleServices;

namespace PuppetMaster
{
    [RunInstaller(true)]
    public class Program : SimpleServiceApplication
    {
        private static void Main(string[] args)
        {
            new Service(args,
                        new List<IWindowsService> { new NancyService() }.ToArray,
                        installationSettings: (serviceInstaller, serviceProcessInstaller) =>
                        {
                            serviceInstaller.ServiceName = "PuppetMaster";
                            serviceInstaller.StartType = ServiceStartMode.Manual;
                            serviceProcessInstaller.Account = ServiceAccount.LocalService;
                        },
                        configureContext: x => { x.Log = Console.WriteLine; })
                .Host();
        }
    }
}
