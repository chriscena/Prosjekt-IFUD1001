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
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public MainViewModel ViewModel
        {
            get
            {
                return (MainViewModel)DataContext;
            }
        }

        private void AddPersonClick(object sender, EventArgs e)
        {
            ViewModel.AddMemberCommand.Execute(null);
        }

        private void AddTransactionClick(object sender, EventArgs e)
        {
            ViewModel.AddTransactionCommand.Execute(null);
        }

        private void ShowProjectsClick(object sender, EventArgs e)
        {
            ViewModel.ViewProjectsCommand.Execute(null);
        }
    }
}