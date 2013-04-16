using System;
using System.Device.Location;
using System.Windows;
using Sluttprosjekt.Helpers;
using Windows.Devices.Geolocation;

namespace Sluttprosjekt.Model
{
    public class PointsService : IPointsService
    {
        private Action<GeoCoordinate, string> _callback;
        private Geolocator _locator;

        public void StartTracking(Action<GeoCoordinate, string> callback)
        {
            _callback = callback;
            _locator = new Geolocator
            {
                DesiredAccuracy = PositionAccuracy.High,
                MovementThreshold = 100
            };

            _locator.StatusChanged += LocatorStatusChanged;
            _locator.PositionChanged += LocatorPositionChanged;
        }

        private void LocatorStatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    DispatchCallback(null, "Location service disabled");
                    break;

                case PositionStatus.NoData:
                    DispatchCallback(null, "No location data");
                    break;

                case PositionStatus.NotAvailable:
                    DispatchCallback(null, "Location service not available on your device");
                    break;

                case PositionStatus.Ready:
                    DispatchCallback(null, "Sensor ready");
                    break;
            }
        }

        private void DispatchCallback(GeoCoordinate coord, string message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(
                () => _callback(coord, message));

        }

        private void LocatorPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            DispatchCallback(args.Position.Coordinate.ToGeoCoordinate(), null);
        }

        public void StopTracking()
        {
            if (_locator == null)
            {
                return;
            }

            _locator.PositionChanged -= LocatorPositionChanged;
            _locator.StatusChanged -= LocatorStatusChanged;
            _locator = null;
        }
    }
}