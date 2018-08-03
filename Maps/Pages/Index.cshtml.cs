using CsvHelper;
using CsvHelper.Configuration;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;

using GeoIP;
using GeoIP.MaxMind;

namespace Maps.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public IConfiguration Configuration { get; }
        public IGeoIP GeoIP { get; set; }

        public string MapboxAccessToken => Configuration["Mapbox:AccessToken"];
        public IPAddress IP => HttpContext.Connection.RemoteIpAddress;

        public double InitialLatitude { get; set; } = 0;
        public double InitialLongitude { get; set; } = 0;
        public int InitialZoom { get; set; } = 1;

        public IndexModel(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGeoIP geoIp)
        {
            Configuration = configuration;            
            GeoIP = geoIp;
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet()
        {
            var city = GeoIP.GetIPInfo(IP);
            if (city?.Latitude != null && city?.Longitude != null)
            {
                InitialLatitude = city.Latitude.Value;
                InitialLongitude = city.Longitude.Value;
                InitialZoom = 9;
            }
        }

        public IActionResult OnGetAirports()
        {
            var configuration = new Configuration
            {
                BadDataFound = context => { }
            };

            using (var sr = new StreamReader(Path.Combine(_hostingEnvironment.WebRootPath, Configuration["Airports:DbFileName"])))
            using (var reader = new CsvReader(sr, configuration))
            {
                FeatureCollection featureCollection = new FeatureCollection();

                while (reader.Read())
                {
                    string name = reader.GetField<string>(1);
                    string iataCode = reader.GetField<string>(4);
                    double latitude = reader.GetField<double>(6);
                    double longitude = reader.GetField<double>(7);

                    featureCollection.Features.Add(new Feature(
                        new Point(new Position(latitude, longitude)),
                        new Dictionary<string, object>
                        {
                            { "name", name },
                            { "iataCode" , iataCode }
                        }));
                }

                return new JsonResult(featureCollection);
            }
        }

    }
}
