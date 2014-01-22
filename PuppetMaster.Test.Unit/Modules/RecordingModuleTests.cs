using NUnit.Framework;
using PuppetMaster.Modules;

namespace PuppetMaster.Test.Unit.Modules
{
    [TestFixture]
    public class RecordingModuleTests : BrowserTest
    {
        [Test]
        public void RequestRoot_RecordModeSpecified_RootedToRecordingModule()
        {
            // When
            var result = Browser.Get("/anything", with => with.Header(PuppetMasterHeaders.ModeHeader, PuppetMasterMode.Record.ToString()));

            // Then
            Assert.That(result.Headers[PuppetMasterHeaders.RecordingHeader], Is.EqualTo("true"));
        }
    }
}