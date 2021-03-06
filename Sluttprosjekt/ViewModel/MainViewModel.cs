﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    /// <summary>
    /// View model for the main page.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            MembersList = new ObservableCollection<MemberWithTotalDueAmount>();
            TransactionsList = new ObservableCollection<Transaction>();
            PaymentsList = new ObservableCollection<Payment>();

            MessengerInstance.Register<ActiveProjectChanged>(this, _ => UpdateListsAfterAdd());
            MessengerInstance.Register<MemberAdded>(this, _ => UpdateListsAfterAdd());
            MessengerInstance.Register<TransactionAdded>(this, _ => UpdateListsAfterAdd());

            UpdateMembersList();
            UpdateTransactionsList();
            UpdatePayments();
        }


        public ObservableCollection<MemberWithTotalDueAmount> MembersList { get; private set; }
        public ObservableCollection<Transaction> TransactionsList { get; private set; }
        public ObservableCollection<Payment> PaymentsList { get; private set; }

        public RelayCommand<RoutedEventArgs> CheckProjectCommand
        {
            get { return _checkProjectCommand ?? (_checkProjectCommand = new RelayCommand<RoutedEventArgs>(CheckProject)); }
        }

        public RelayCommand ViewProjectsCommand
        {
            get
            {
                return _viewProjectsCommand
                    ?? (_viewProjectsCommand = new RelayCommand(() => _navigationService.Navigate("ProjectsPage")));
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
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private RelayCommand _addMemberCommand;
        private RelayCommand _addTransactionCommand;
        private RelayCommand _viewProjectsCommand;
        private RelayCommand<RoutedEventArgs> _checkProjectCommand;


        private void CheckProject(RoutedEventArgs obj)
        {
            var activeProjectViewModel = SimpleIoc.Default.GetInstance<ActiveProjectViewModel>();
            if (activeProjectViewModel.ActiveProject == null)
                _navigationService.Navigate("ProjectsPage");
        }

        private void UpdateListsAfterAdd()
        {
            UpdateTransactionsList();
            UpdatePayments();
            UpdateMembersList();

            SimpleIoc.Default.Unregister<AddTransactionViewModel>();
            SimpleIoc.Default.Register<AddTransactionViewModel>();
            SimpleIoc.Default.Unregister<AddMemberViewModel>();
            SimpleIoc.Default.Register<AddMemberViewModel>();
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

        private void UpdatePayments()
        {
            var list = _dataService.GetPayments();
            PaymentsList.Clear();
            foreach (var payment in list)
            {
                PaymentsList.Add(payment);
            }
        }

        private void UpdateMembersList()
        {
            var list = _dataService.GetMembersWithTotalDueAmount();
            MembersList.Clear();
            foreach (var member in list)
            {
                MembersList.Add(member);
            }
        }
    }
}