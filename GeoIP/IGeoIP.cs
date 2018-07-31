namespace GeoIP
{
    public interface IGeoIP
    {
        GeoIPInfo GetIPInfo(string ip);
        GeoIPInfo GetIPInfo(uint ip);
    }
}
