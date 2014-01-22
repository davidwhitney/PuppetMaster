using Nancy;

namespace PuppetMaster.RequestModeDetection
{
    public static class NancyModulesExtensionsForSyntaticSugar
    {
        public static bool ModeIs(this NancyContext ctx, PuppetMasterMode mode)
        {
            return ctx.Request.InMode(mode);
        }
    }
}