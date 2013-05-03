using System.Collections.ObjectModel;
using Friends.Lib.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
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

            MembersList = new ObservableCollection<Member>();
            TransactionsList = new ObservableCollection<Transaction>();

            MessengerInstance.Register<MemberAdded>(this, _ => UpdateMembersListAfterAdd());
            MessengerInstance.Register<TransactionAdded>(this, _ => UpdateTransactionListAfterAdd());

            UpdateMembersList();
            UpdateTransactionsList();
#if DEBUG
            if (IsInDesignMode)
            {
                //Refresh();
            }
#endif
        }


        private RelayCommand _addMemberCommand;
        private RelayCommand _addTransactionCommand;
        private RelayCommand _viewProjectsCommand;

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

        public IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDialogService>();
            }
        }

        public ObservableCollection<Member> MembersList { get;  private set;}

        public ObservableCollection<Transaction> TransactionsList { get; private set; }

        private void UpdateTransactionListAfterAdd()
        {
            SimpleIoc.Default.Unregister<AddTransactionViewModel>();
            UpdateTransactionsList();
            SimpleIoc.Default.Register<AddTransactionViewModel>();
        }

        private void UpdateTransactionsList()
        {
            var list = _dataService.GetTransactions();
            TransactionsList.Clear();
            foreach (var transaction in list)
            {
                TransactionsList.Add(transaction);
            }
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
    }
}