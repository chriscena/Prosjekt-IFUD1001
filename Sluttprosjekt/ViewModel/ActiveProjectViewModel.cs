﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class ActiveProjectViewModel : ViewModelBase
    {
        private const string Title = "Spleiselag";
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public ActiveProjectViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            MessengerInstance.Register<ActiveProjectChanging>(this, SetActiveProject);
            GetActiveProject();
            if (ActiveProject == null)
                _navigationService.Navigate("ProjectsPage");
        }

        private Project _activeProject;

        public Project ActiveProject
        {
            get { return _activeProject; }
            set
            {
                _activeProject = value;
                RaisePropertyChanged(() => ActiveProject);
                RaisePropertyChanged(() => ActiveProjectTitle);
            }
        }

        public string ActiveProjectTitle
        {
            get
            {
                if (ActiveProject == null) return Title.ToUpper();
                return string.Format("{0} - {1}", ActiveProject.Name.ToUpper(), Title.ToUpper());
            }
        }

        private void GetActiveProject()
        {
            ActiveProject = _dataService.GetActiveProject();
        }

        private void SetActiveProject(ActiveProjectChanging message)
        {
            ActiveProject = message.ActiveProject;
            if (message.ActiveProject == null)
                _navigationService.Navigate("ProjectsPage");
            else
                _dataService.SetActiveProject(message.ActiveProject);
            MessengerInstance.Send(new ActiveProjectChanged { ActiveProject = ActiveProject});
        }
    }


    public class ActiveProjectChanging
    {
        public Project ActiveProject { get; set; }
    }

    public class ActiveProjectChanged
    {
        public Project ActiveProject { get; set; }
    }
}
