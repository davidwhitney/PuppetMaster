using Nancy.Bootstrapper;
using Nancy.Json;
using Ninject;
using PuppetMaster.Recording;
using PuppetMaster.Recording.Storage;

namespace PuppetMaster
{
    public class NinjectBootstrapper : Nancy.Bootstrappers.Ninject.NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = int.MaxValue;
            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureApplicationContainer(IKernel container)
        {
            container.Bind<ICallStore>().To<InMemoryCallStore>().InSingletonScope();
            base.ConfigureApplicationContainer(container);
        }
    }
}