using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class ProjectsViewModel : ViewModelBase
    {        
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public ProjectsViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            ProjectsList = new ObservableCollection<Project>();
            MembersList = new ObservableCollection<Member>();

            MessengerInstance.Register<ProjectAdded>(this, _ => UpdateProjectsListAfterAdd());
            MessengerInstance.Register<MemberAdded>(this, _ => UpdateMembersListAfterAdd());

            UpdateProjectsList();
            UpdateMembersList();
#if DEBUG
            if (IsInDesignMode)
            {
                //UpdateProjectsList();
            }
#endif
        }

        public void AddProjectIfNeeded()
        {
            if (ProjectsList.Count == 0)
                _navigationService.Navigate("AddProjectPage");
        }

        private void UpdateMembersListAfterAdd()
        {
            SimpleIoc.Default.Unregister<AddMemberViewModel>();
            UpdateMembersList();
            SimpleIoc.Default.Register <AddMemberViewModel>();
        }

        private void UpdateProjectsListAfterAdd()
        {
            SimpleIoc.Default.Unregister<AddProjectViewModel>();
            UpdateProjectsList();
            SimpleIoc.Default.Register<AddProjectViewModel>();
        }

        private void UpdateMembersList()
        {
            var list = _dataService.GetMembers();
            MembersList.Clear();
            foreach (var member in list)
            {
                MembersList.Add(member);
            }
        }

        private void UpdateProjectsList()
        {
            var list = _dataService.GetProjects();
            ProjectsList.Clear();
            foreach (var project in list)
            {
                ProjectsList.Add(project);
            }
        }

        public ObservableCollection<Member> MembersList
        {
            get;
            private set;
        }

        public ObservableCollection<Project> ProjectsList
        {
            get;
            private set;
        }

        private RelayCommand _addProjectCommand;
        private RelayCommand _addMemberCommand;
        
        public RelayCommand AddProjectsCommand
        {
            get
            {
                return _addProjectCommand
                       ?? (_addProjectCommand = new RelayCommand(() => _navigationService.Navigate("AddProjectPage")));
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
    }

    public class ProjectAdded
    {
    }
}
