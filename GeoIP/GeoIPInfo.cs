using System;
using System.Collections.Generic;
using System.Text;

namespace GeoIP
{
    public class GeoIPInfo
    {
        public string IP;
        public double? Latitude;
        public double? Longitude;
        public string Country;
        public string CountryName;
        public string Region;
        public string RegionName;
        public string City;
        public string ZIP;
        public string ISP;
        public short TimeZone = -480;
        public bool Proxy;
        public bool Local;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var f in this.GetType().GetFields())
            {
                sb.AppendFormat("{0}={1},", f.Name, f.GetValue(this));
            }

            return sb.ToString();
        }
    }
}
