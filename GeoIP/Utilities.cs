using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using System;

namespace GeoIP
{
    public class Utilities
    {
        private const double _radian = 57.2957795130823;
        private const int _earthRadius = 3963;

        public static double GetDistanceInMiles(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return _earthRadius * Math.Acos(Math.Sin(latitude1 / _radian) * Math.Sin(latitude2 / _radian) + Math.Cos(latitude1 / _radian) * Math.Cos(latitude2 / _radian) * Math.Cos((longitude2 - longitude1) / _radian));
        }

        public static bool IsLocalIP(string ip)
        {
            return ip != null && (ip == "127.0.0.1" || ip == "::1" || ip.StartsWith("10.") || ip.StartsWith("192.168."));
        }

        public static GeoIPInfo GetIPInfo(DatabaseReader dbReader, string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return null;
            else if (Utilities.IsLocalIP(ip))
                return new GeoIPInfo() { IP = ip, IsLocal = true };

            try
            {
                var info = dbReader.City(ip);
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
                    IsProxy = info.Traits.IsAnonymousProxy
                };
            }
            catch (AddressNotFoundException anfex)
            {
                // TO DO: Log Exception
                return null;
            }
            catch (Exception ex)
            {
                // TO DO: Log Exception
                return null;
            }
        }
    }
}
