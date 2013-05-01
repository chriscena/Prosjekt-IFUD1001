using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Model;
using Sluttprosjekt.ViewModel;
using Windows.Networking.Proximity;
using Windows.Phone.Speech.Synthesis;

namespace Sluttprosjekt
{
    public partial class DetailsPage
    {
        private readonly ProximityDevice _device;
        private long _messageId;

        public DetailsPage()
        {
            InitializeComponent();

            _device = ProximityDevice.GetDefault();

            if (_device == null)
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
            }
            else
            {
                _device.DeviceDeparted += dev =>
                {
                    if (_messageId > 0)
                    {
                        dev.StopPublishingMessage(_messageId);
                        _messageId = 0;

                        DispatcherHelper.CheckBeginInvokeOnUI(
                            () =>
                            {
                                IsEnabled = true;
                            });
                    }
                };
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("key"))
            {
                var parameter = ServiceLocator.Current.GetInstance<object>(NavigationContext.QueryString["key"]);
                DataContext = parameter;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _device.StopPublishingMessage(_messageId);
            _messageId = 0;
            IsEnabled = true;
            base.OnNavigatingFrom(e);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            // Update current binding
            var element = FocusManager.GetFocusedElement() as TextBox;
            if (element != null)
            {
                var currentBinding = element.GetBindingExpression(TextBox.TextProperty);
                if (currentBinding != null)
                {
                    currentBinding.UpdateSource();
                }
            }

            var main = ServiceLocator.Current.GetInstance<MainViewModel>();
            main.SaveCommand.Execute(DataContext);
        }

        private void SendClick(object sender, EventArgs e)
        {
            IsEnabled = false;

            _device.StopPublishingMessage(_messageId);

            var serial = DataContext.ToString();
            _messageId = _device.PublishMessage(
                "Windows.GalaSoft.Friend",
                serial);
        }

        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

        private async void ReadClick(object sender, EventArgs e)
        {
            //var friend = (Friend)DataContext;
            //await _synth.SpeakTextAsync(friend.Message);
        }

        private void ShowMapClick(object sender, RoutedEventArgs e)
        {
            var key = Guid.NewGuid().ToString();
            SimpleIoc.Default.Register<object>(() => DataContext, key);

            NavigationService.Navigate(new Uri(string.Format("/MapPage.xaml?key={0}", key), UriKind.Relative));
        }
    }
}