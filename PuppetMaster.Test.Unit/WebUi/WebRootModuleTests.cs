using Nancy.Testing;
using NUnit.Framework;

namespace PuppetMaster.Test.Unit.WebUi
{
    [TestFixture]
    public class WebRootModuleTests : BrowserTest
    {
        [Test]
        public void RequestRoot_Returns200Ok()
        {
            var result = Browser.Get("/", with => with.HttpRequest());
            var body = result.Body.AsString();

            Assert.That(result.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.OK));
        }
    }
}
