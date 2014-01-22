using Nancy.Testing;
using NUnit.Framework;

namespace PuppetMaster.Test.Unit
{
    public abstract class BrowserTest
    {       
        protected NinjectBootstrapper Bootstrapper;
        protected Browser Browser;

        [SetUp]
        public void SetUp()
        {
            Bootstrapper = new NinjectBootstrapper();
            Browser = new Browser(Bootstrapper);
        }
    }
}