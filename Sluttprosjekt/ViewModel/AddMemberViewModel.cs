﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class AddMemberViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public AddMemberViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        public string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; RaisePropertyChanged(() => MemberName); }
        }

        private RelayCommand _saveCommand;
        private string _memberName;

        public RelayCommand SaveCommand { get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveMember)); } }

        private void SaveMember()
        {

            var member = new Member { Name = MemberName };
            _dataService.SaveMember(member);
            MessengerInstance.Send(new MemberAdded());
            _navigationService.GoBack();
        }

    }

    internal class MemberAdded
    {
    }
}