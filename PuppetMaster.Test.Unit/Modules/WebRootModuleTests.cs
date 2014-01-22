using NUnit.Framework;

namespace PuppetMaster.Test.Unit.Modules
{
    [TestFixture]
    public class WebRootModuleTests : BrowserTest
    {
        [Test]
        public void RequestRoot_Returns200Ok()
        {
            // When
            var result = Browser.Get("/", with => with.HttpRequest());

            // Then
            Assert.That(result.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.OK));
        }
    }
}
