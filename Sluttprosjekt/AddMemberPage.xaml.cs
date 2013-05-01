using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.ViewModel;

namespace Sluttprosjekt
{
    public partial class AddMemberPage
    {
        public AddMemberPage()
        {
            InitializeComponent();
        }

        public AddMemberViewModel ViewModel { get { return (AddMemberViewModel) DataContext; } }

        private void SaveClick(object sender, EventArgs e)
        {
            BindingHelper.UpdateSource();
            ViewModel.SaveCommand.Execute(null);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}