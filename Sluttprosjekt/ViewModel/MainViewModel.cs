using System;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using Friends.Lib.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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

        public MainViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            MessengerInstance.Register <MemberAdded>(this, _ => UpdateMembersListAfterAdd());
            MembersList = new ObservableCollection<Member>();
            ProjectsList = new ObservableCollection<Project>();

            UpdateMembersList();
            
#if DEBUG
            if (IsInDesignMode)
            {
                //Refresh();
            }
#endif
        }

        private void UpdateMembersListAfterAdd()
        {
            SimpleIoc.Default.Unregister<AddMemberViewModel>();
            UpdateMembersList();
            SimpleIoc.Default.Register<AddMemberViewModel>();
        }

        private void UpdateMembersList()
        {
            var list = _dataService.GetMembers();
            MembersList.Clear();
            foreach (var member in list)
            {
                MembersList.Add(member);
            }
        }


        public ObservableCollection<Member> MembersList
        {
            get;
            private set;
        }

        public ObservableCollection<Project> ProjectsList
        {
            get;
            private set;
        }

        private RelayCommand _viewProjectsCommand;

        /// <summary>
        /// Gets the ShowDetailsCommand.
        /// </summary>
        public RelayCommand ViewProjectsCommand
        {
            get
            {
                return _viewProjectsCommand
                    ?? (_viewProjectsCommand = new RelayCommand(() =>
                                              _navigationService.Navigate("ProjectsPage")
                                          ));
            }
        }

        private RelayCommand<Transaction> _saveCommand;
        private RelayCommand _addMemberCommand;
        private RelayCommand _addTransactionCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand<Transaction> SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand<Transaction>(
                                          async friend =>
                                          {
                                              //try
                                              //{
                                              //    var id = await _dataService.SaveFriend(friend);

                                              //    var intId = int.Parse(id);

                                              //    if (intId > 0)
                                              //    {
                                              //        friend.Id = id;
                                              //    }
                                              //    else
                                              //    {
                                              //        DialogService.ShowMessage("Error when saving", null);
                                              //    }
                                              //}
                                              //catch (Exception ex)
                                              //{
                                              //    DialogService.ShowError(ex, "Error", "OK", null);
                                              //}
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

        public RelayCommand AddMemberCommand
        {
            get
            {
                return _addMemberCommand ??
                       (_addMemberCommand = new RelayCommand(() => _navigationService.Navigate("AddMemberPage")));
            }
        }

        public RelayCommand AddTransactionCommand
        {
            get
            {
                return _addTransactionCommand ??
                       (_addTransactionCommand = new RelayCommand(() => _navigationService.Navigate("AddTransactionPage")));
            }
        }
    }
}