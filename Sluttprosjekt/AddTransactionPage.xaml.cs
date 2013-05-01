using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sluttprosjekt.ViewModel;

namespace Sluttprosjekt
{
    public partial class AddTransactionPage
    {
        public AddTransactionPage()
        {
            InitializeComponent();
        }

        public AddTransactionViewModel ViewModel { get { return (AddTransactionViewModel) DataContext; } }

        private void SaveClick(object sender, EventArgs e)
        {
            ViewModel.SaveCommand.Execute(DataContext);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}