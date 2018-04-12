using Nancy;
using System.IO;

namespace GCHeritagePlatform.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/crossdomain.xml"] = p =>
            {
                //SystemLogger.getLogger().Debug("进入上传");
                string str = @"<?xml version=""1.0"" ?> 
                <cross-domain-policy>
                <allow-access-from domain=""*""/>
                <site-control permitted-cross-domain-policies=""all""/>
                <allow-http-request-headers-from domain=""*"" headers=""*""/>
                </cross-domain-policy>";
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
                return Response.FromStream(new MemoryStream(bytes), "text/xml");
            };
        }
    }
}