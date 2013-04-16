using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using GalaSoft.MvvmLight;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        private readonly IPointsService _pointsService;

        public ObservableCollection<GeoCoordinate> Points
        {
            get;
            private set;
        }

        public MapViewModel()
        {
            Points = new ObservableCollection<GeoCoordinate>();
            _pointsService = new PointsService();
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                Set(StatusPropertyName, ref _status, value);
            }
        }

        public void StartTracking()
        {
            _pointsService.StartTracking(TrackingChanged);
        }

        public void StopTracking()
        {
            _pointsService.StopTracking();
        }

        private void TrackingChanged(GeoCoordinate coord, string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                Status = status;
            }

            if (coord != null)
            {
                Points.Add(coord);
            }
        }
    }
}
