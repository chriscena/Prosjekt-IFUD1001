using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Model;
using Sluttprosjekt.ViewModel;

namespace Sluttprosjekt
{
    public partial class MapPage : PhoneApplicationPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private void MapLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO After submission
            // See http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207033(v=vs.105).aspx
            //MapsSettings.ApplicationContext.ApplicationId = "<applicationid>";
            //MapsSettings.ApplicationContext.AuthenticationToken = "<authenticationtoken>";
        }

        private MapLayer _pointsLayer;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Friend friend = null;

            if (NavigationContext.QueryString.ContainsKey("key"))
            {
                var parameter = ServiceLocator.Current.GetInstance<object>(NavigationContext.QueryString["key"]);
                friend = parameter as Friend;
            }

            if (_pointsLayer != null
                && MyMap.Layers.Contains(_pointsLayer))
            {
                MyMap.Layers.Remove(_pointsLayer);
            }

            var vm = (MapViewModel)DataContext;
            vm.Points.CollectionChanged += PointsCollectionChanged;

            _pointsLayer = new MapLayer();
            MyMap.Layers.Add(_pointsLayer);

            var points = vm.Points.ToList();

            foreach (var point in points)
            {
                AddPoint(point, Colors.Red);
            }

            if (points.Count > 0)
            {
                MyMap.Center = points[points.Count - 1];
            }

            MyMap.ZoomLevel = 11;
            MyMap.Pitch = 30.0;

            if (friend != null)
            {
                var friendLocation = new GeoCoordinate(friend.ParsedLocation.X, friend.ParsedLocation.Y);
                MyMap.Center = friendLocation;
                AddPoint(friendLocation, Colors.Blue);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var vm = (MapViewModel)DataContext;
            vm.Points.CollectionChanged -= PointsCollectionChanged;
            base.OnNavigatingFrom(e);
        }

        private void PointsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (var index = 0; index < e.NewItems.Count; index++)
            {
                AddPoint((GeoCoordinate)e.NewItems[index], Colors.Red);
            }

            MyMap.Center = (GeoCoordinate)e.NewItems[e.NewItems.Count - 1];
        }

        private void AddPoint(GeoCoordinate newItem, Color color)
        {
            var myCircle = new Ellipse
            {
                Fill = new SolidColorBrush(color),
                Height = 20,
                Width = 20,
                Opacity = 0.5
            };

            // Create a MapOverlay to contain the circle.
            var overlay = new MapOverlay
            {
                Content = myCircle,
                PositionOrigin = new Point(0.5, 0.5),
                GeoCoordinate = newItem
            };

            _pointsLayer.Add(overlay);
        }

        private void TrackClick(object sender, EventArgs e)
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
            var vm = (MapViewModel)DataContext;
            vm.StartTracking();
        }

        private void StopClick(object sender, EventArgs e)
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
            var vm = (MapViewModel)DataContext;
            vm.StopTracking();
        }
    }
}