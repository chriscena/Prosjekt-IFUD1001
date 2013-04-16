using System;
using System.Device.Location;

namespace Sluttprosjekt.Model
{
    public interface IPointsService
    {
        void StartTracking(Action<GeoCoordinate, string> callback);
        void StopTracking();
    }
}
