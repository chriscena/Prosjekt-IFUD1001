using System;
using System.Device.Location;
using Windows.Devices.Geolocation;

namespace Sluttprosjekt.Helpers
{
    public static class Extensions
    {
        public static GeoCoordinate ToGeoCoordinate(this Geocoordinate coordinate)
        {
            return new GeoCoordinate(
                coordinate.Latitude,
                coordinate.Longitude,
                coordinate.Altitude ?? Double.NaN,
                coordinate.Accuracy,
                coordinate.AltitudeAccuracy ?? Double.NaN,
                coordinate.Speed ?? Double.NaN,
                coordinate.Heading ?? Double.NaN);
        }
    }
}
