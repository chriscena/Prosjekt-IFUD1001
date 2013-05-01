using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; RaisePropertyChanged(() => ProjectName); }
        }

        private RelayCommand _saveCommand;
        private string _projectName;

        public RelayCommand SaveCommand { get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveProject)); } }

        private void SaveProject()
        {

            var project = new Project { Name = ProjectName };
            _dataService.SaveProject(project);
            MessengerInstance.Send(new ActiveProjectChanged { ActiveProject = project });
            MessengerInstance.Send(new ProjectAdded());
            _navigationService.GoBack();
        }

        
    }
}
