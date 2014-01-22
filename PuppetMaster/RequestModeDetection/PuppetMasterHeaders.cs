namespace PuppetMaster.RequestModeDetection
{
    public class PuppetMasterHeaders
    {
        public static string ModeHeader { get { return "x-puppetmaster-mode"; } }
        public static string RecordingHeader { get { return "x-puppetmaster-recorder"; } }
    }
}