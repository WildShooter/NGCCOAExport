using Nancy.Hosting.Aspnet;

namespace GCHeritagePlatform.Handlers
{
    public class NancyHandler: NancyHttpRequestHandler, System.Web.SessionState.IRequiresSessionState
    {
    }
}