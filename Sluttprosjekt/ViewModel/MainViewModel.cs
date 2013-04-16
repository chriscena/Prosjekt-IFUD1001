using System;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using Friends.Lib.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Agent;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Friend> FriendsList
        {
            get;
            private set;
        }

        private RelayCommand _refreshCommand;

        /// <summary>
        /// Gets the RefreshCommand.
        /// </summary>
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(
                                          async () =>
                                          {
                                              await Refresh();
                                          }));
            }
        }

        public async Task Refresh()
        {
            try
            {
                var list = (await _dataService.GetFriends()).ToList();

                FriendsList.Clear();

                foreach (var friend in list)
                {
                    FriendsList.Add(friend);
                }

                ScheduledAgent.SaveSettings(list.Count);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex, "Error", "OK", null);
            }
        }

        private RelayCommand<Friend> _showDetailsCommand;

        /// <summary>
        /// Gets the ShowDetailsCommand.
        /// </summary>
        public RelayCommand<Friend> ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand
                    ?? (_showDetailsCommand = new RelayCommand<Friend>(
                                          friend =>
                                          {
                                              _navigationService.Navigate("DetailsPage", friend);
                                          }));
            }
        }

        private RelayCommand<Friend> _saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand<Friend> SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand<Friend>(
                                          async friend =>
                                          {
                                              try
                                              {
                                                  var id = await _dataService.SaveFriend(friend);

                                                  var intId = int.Parse(id);

                                                  if (intId > 0)
                                                  {
                                                      friend.Id = id;
                                                  }
                                                  else
                                                  {
                                                      DialogService.ShowMessage("Error when saving", null);
                                                  }
                                              }
                                              catch (Exception ex)
                                              {
                                                  DialogService.ShowError(ex, "Error", "OK", null);
                                              }
                                          }));
            }
        }

        public IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDialogService>();
            }
        }

        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            FriendsList = new ObservableCollection<Friend>();

#if DEBUG
            if (IsInDesignMode)
            {
                Refresh();
            }
#endif
        }

        public void UpdateFiend(string serial)
        {
            var friend = new Friend(serial)
            {
                IsDirty = true
            };

            var existing = FriendsList.FirstOrDefault(f => f.Id == friend.Id);

            if (existing != null)
            {
                existing.Update(friend);
            }
            else
            {
                FriendsList.Add(friend);
            }
        }
    }
}