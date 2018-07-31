using MaxMind.GeoIP2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeoIP.MaxMind
{
    public sealed class MaxMindGeoIP : IGeoIP
    {
        private static MaxMindGeoIP _instance = null;
        private static readonly object _lock = new object();

        private DatabaseReader _dbReader;

        public static MaxMindGeoIP GetInstance(string dbPath)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MaxMindGeoIP(dbPath);
                }

                return _instance;
            }
        }

        private MaxMindGeoIP(string dbPath)
        {
            _dbReader = new DatabaseReader(dbPath);
        }


        public GeoIPInfo GetIPInfo(string ip)
        {
            var info = _dbReader.City(ip);

            return new GeoIPInfo()
            {
                IP = ip,
                ISP = info.Traits.Isp,
                Country = info.Country.IsoCode,
                CountryName = info.Country.Name,
                Region = info.Subdivisions != null && info.Subdivisions.Count != 0 ? info.Subdivisions[0].IsoCode : "",
                RegionName = info.Subdivisions != null && info.Subdivisions.Count != 0 ? info.Subdivisions[0].Name : "",
                City = info.City.Name,
                ZIP = info.Postal.Code,
                Latitude = info.Location.Latitude,
                Longitude = info.Location.Longitude,
                Proxy = info.Traits.IsAnonymousProxy
            };
        }

        public GeoIPInfo GetIPInfo(uint ip)
        {
            throw new NotImplementedException();
        }


    }
}
