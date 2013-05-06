using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class AddProjectViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public AddProjectViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        private RelayCommand _saveCommand;
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; RaisePropertyChanged(() => ProjectName); }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveProject)); }
        }


        public IDialogService DialogService
        {
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }

        private void SaveProject()
        {
            BindingHelper.UpdateSource();
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                DialogService.ShowError("Du må fylle inn et navn for å lagre spleiselaget.", "Mangler navn", "OK", null);
                return;
            }

            var project = new Project { Name = ProjectName };
            _dataService.SaveProject(project);
            MessengerInstance.Send(new ActiveProjectChanged { ActiveProject = project });
            MessengerInstance.Send(new ProjectAdded());
            _navigationService.GoBack();
        }
    }
}
