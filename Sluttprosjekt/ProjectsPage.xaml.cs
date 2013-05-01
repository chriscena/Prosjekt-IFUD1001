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
    public partial class ProjectsPage
    {
        public ProjectsPage()
        {
            InitializeComponent();
        }        
        
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public ProjectsViewModel ViewModel
        {
            get
            {
                return (ProjectsViewModel)DataContext;
            }
        }

        private void AddPersonClick(object sender, EventArgs e)
        {
            ViewModel.AddMemberCommand.Execute(null);
        }

        private void AddProjectClick(object sender, EventArgs e)
        {
            ViewModel.AddProjectsCommand.Execute(null);
        }

        private void PhonePageBase_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.AddProjectIfNeeded();
        }
    }
}