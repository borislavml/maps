using System.Net;

namespace GeoIP
{
    public interface IGeoIP
    {
        GeoIPInfo GetIPInfo(string ip);
        GeoIPInfo GetIPInfo(IPAddress ip);
    }
}
