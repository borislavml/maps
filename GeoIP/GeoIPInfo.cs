using System;
using System.Collections.Generic;
using System.Text;

namespace GeoIP
{
    public class GeoIPInfo
    {
        public string IP { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string ISP { get; set; }
        public string TimeZone { get; set; }
        public bool IsProxy { get; set; }
        public bool IsLocal { get; set; }

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
