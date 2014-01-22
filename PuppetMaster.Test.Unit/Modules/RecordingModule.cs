using NUnit.Framework;

namespace PuppetMaster.Test.Unit.Modules
{
    [TestFixture]
    public class RecordingModule : BrowserTest
    {
        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            // When
            var result = Browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.That(result.StatusCode, Is.EqualTo(Nancy.HttpStatusCode.OK));
        }
    }
}
