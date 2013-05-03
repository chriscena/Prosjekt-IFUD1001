using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class AddTransactionViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public AddTransactionViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            UpdateMembersList();
        }

        private void UpdateMembersList()
        {
            var members = _dataService.GetMembers();
            MembersList.Clear();
            foreach (var member in members)
            {
                MembersList.Add(member);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; RaisePropertyChanged(() => Amount); }
        }

        public Member Payer
        {
            get { return _payer; }
            set { _payer = value; RaisePropertyChanged(() => Payer); }
        }

        public ObservableCollection<IMember> MembersList
        {
            get { return _membersList; }
            set { _membersList = value; RaisePropertyChanged(() => MembersList); }
        }

        private RelayCommand _saveCommand;
        private string _description;
        private decimal _amount;
        private Member _payer;
        private ObservableCollection<IMember> _membersList = new ObservableCollection<IMember>();
        private RelayCommand _cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveTransaction)); }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(() => _navigationService.GoBack())); }
        }

        private void SaveTransaction()
        {
            BindingHelper.UpdateSource();
            var transaction = new Transaction {Description = Description, Amount = Amount, PaidBy = Payer.Id, PaidDate = DateTime.Today};
            _dataService.SaveTransaction(transaction);
            MessengerInstance.Send(new TransactionAdded());
            _navigationService.GoBack();
        }
    }

    public class TransactionAdded
    {
        
    }
}
