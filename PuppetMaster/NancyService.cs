using System;
using System.Configuration;
using Nancy.Hosting.Self;
using SimpleServices;

namespace PuppetMaster
{
    public class NancyService : IWindowsService
    {
        public ApplicationContext AppContext { get; set; }

        private readonly NancyHost _nancyHost;
        private readonly string _address;

        public NancyService()
        {
            _address = ConfigurationManager.AppSettings["Address"];
            _nancyHost = new NancyHost(new Uri(_address),

                new NinjectBootstrapper(),
                new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } });
        }

        public void Start(string[] args)
        {
            Console.WriteLine("PuppetMaster endabled on: " + _address);
            _nancyHost.Start();
        }

        public void Stop()
        {
            _nancyHost.Stop();
        }
    }
}