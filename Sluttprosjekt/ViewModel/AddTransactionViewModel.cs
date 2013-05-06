using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
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
            PaidDate = DateTime.Today;
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
            if (MembersList.Count > 0)
                Payer = MembersList.First();
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
            set
            {
                _amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        public Member Payer
        {
            get { return _payer; }
            set
            {
                _payer = value;
                RaisePropertyChanged(() => Payer);
            }
        }

        public ObservableCollection<Member> MembersList
        {
            get { return _membersList; }
            set { _membersList = value; RaisePropertyChanged(() => MembersList); }
        }

        private RelayCommand _saveCommand;
        private string _description;
        private decimal _amount;
        private Member _payer;
        private ObservableCollection<Member> _membersList = new ObservableCollection<Member>();
        private DateTime _paidDate;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveTransaction)); }
        }

        public IDialogService DialogService
        {
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }

        public DateTime PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; RaisePropertyChanged(() => PaidDate); }
        }


        private void SaveTransaction()
        {
            BindingHelper.UpdateSource();
            if (string.IsNullOrWhiteSpace(Description))
            {
                DialogService.ShowError("Du må fylle inn en beskrivelse.", "Mangler beskrivelse", "OK", null);
                return;
            }
            if (Payer == null)
            {
                DialogService.ShowError("Du må velge en betaler.", "Mangler betaler", "OK", null);
                return;
            }
            if (Amount <= 0)
            {
                DialogService.ShowError("Du må fylle inn et gyldig beløp.", "Mangler beløp", "OK", null);
                return;
            }
            var transaction = new Transaction {Description = Description, Amount = Amount, PaidBy = Payer.Id, PaidDate = PaidDate};
            _dataService.SaveTransaction(transaction);
            MessengerInstance.Send(new TransactionAdded());
            _navigationService.GoBack();
        }
    }

    public class TransactionAdded
    {
        
    }
}
