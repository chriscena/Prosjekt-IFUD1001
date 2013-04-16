using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sluttprosjekt.Resources;
using Sluttprosjekt.ViewModel;

namespace Sluttprosjekt
{
    public partial class MainPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public MainViewModel Vm
        {
            get
            {
                return (MainViewModel)DataContext;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            Vm.RefreshCommand.Execute(null);
        }
    }
}