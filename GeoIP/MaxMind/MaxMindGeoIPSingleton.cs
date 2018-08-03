using System;
using System.Net;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;

namespace GeoIP.MaxMind
{
    class MaxMindGeoIPSingleton
    {
        private static MaxMindGeoIPSingleton _instance = null;
        private static readonly object _lock = new object();

        private DatabaseReader _dbReader;

        public static MaxMindGeoIPSingleton GetInstance(string dbPath)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MaxMindGeoIPSingleton(dbPath);
                }

                return _instance;
            }
        }

        private MaxMindGeoIPSingleton(string dbPath) => _dbReader = new DatabaseReader(dbPath);

        public GeoIPInfo GetIPInfo(string ip)
        {
            return Utilities.GetIPInfo(_dbReader, ip);
        }

        public GeoIPInfo GetIPInfo(IPAddress ip) => GetIPInfo(ip.ToString());
    }
}
