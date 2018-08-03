using System;
using System.Net;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;

namespace GeoIP.MaxMind
{
    public sealed class MaxMindGeoIP : IGeoIP
    {
        private DatabaseReader _dbReader;

        public MaxMindGeoIP(string dbPath) => _dbReader = new DatabaseReader(dbPath);

        public GeoIPInfo GetIPInfo(string ip)
        {
            return Utilities.GetIPInfo(_dbReader, ip);
        }

        public GeoIPInfo GetIPInfo(IPAddress ip) => GetIPInfo(ip.ToString());
    }
}
