using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.ViewModel;

namespace Sluttprosjekt
{
    public partial class AddProjectPage
    {
        public AddProjectPage()
        {
            InitializeComponent();
        }


        public AddProjectViewModel ViewModel
        {
            get
            {
                return (AddProjectViewModel)DataContext;
            }
        }

        private void SaveClick(object sender, EventArgs e)
        {
            BindingHelper.UpdateSource();
            ViewModel.SaveCommand.Execute(DataContext);
        }

        private void CancelClick(object sender, EventArgs e)
        {
           NavigationService.GoBack();
        }
    }
}