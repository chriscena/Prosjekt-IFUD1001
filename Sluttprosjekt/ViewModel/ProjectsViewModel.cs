using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

            MessengerInstance.Register<ProjectAdded>(this, _ => UpdateProjectsListProjectChange());
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

        public IDialogService DialogService
        {
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }

        public void AddProjectOrMemberIfNeeded(RoutedEventArgs routedEventArgs)
        {
            if (!ProjectsList.Any())
            {
                DialogService.ShowMessage(
                    "Det ser ut som at det ikke er opprettet noen spleiselag. Du må derfor opprette et spleiselag og minst ett medlem av laget.",
                    "Opprette spleiselag", "Fortsett", null);
                _navigationService.Navigate("AddProjectPage");
            }

            if (!MembersList.Any())
                _navigationService.Navigate("AddMemberPage");
        }

        private void UpdateMembersListAfterAdd()
        {
            UpdateMembersList();
            SimpleIoc.Default.Unregister<AddMemberViewModel>();
            SimpleIoc.Default.Register <AddMemberViewModel>();
        }

        private void UpdateProjectsListProjectChange()
        {
            UpdateProjectsList();
            UpdateMembersList();
            SimpleIoc.Default.Unregister<AddProjectViewModel>();
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
        private RelayCommand<RoutedEventArgs> _createProjectCommand;

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

        public RelayCommand<RoutedEventArgs> CreateProjectCommand
        {
            get { return _createProjectCommand ?? (_createProjectCommand = new RelayCommand<RoutedEventArgs>(AddProjectOrMemberIfNeeded)); }
        }

        private Project _selectedProject;
        private RelayCommand _deleteProjectCommand;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged(() => SelectedProject);

                if (SelectedProject == null) return;
                MessengerInstance.Send(new ActiveProjectChanged { ActiveProject = SelectedProject });
                UpdateProjectsListProjectChange();
            }
        }

        public RelayCommand DeleteProjectCommand { get { return _deleteProjectCommand ?? (_deleteProjectCommand = new RelayCommand(ConfirmDeleteProject)); } }

        private void ConfirmDeleteProject()
        {
            DialogService.ShowMessage("Dette vil også slette alle personer og betalinger knyttet til spleiselaget.",
                                      "Slette spleiselag", "Utfør", "Avbryt", b => { if (b) DeleteActiveProject(); });
        }

        private void DeleteActiveProject()
        {
            Project activeProject = null;
            try
            {
                activeProject = _dataService.DeleteActiveProject();
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex, "Sletting feilet", "OK", null);
                Application.Current.Terminate();
            }
            UpdateProjectsListProjectChange();
            MessengerInstance.Send(new ActiveProjectChanged { ActiveProject = activeProject });
            _navigationService.GoBack();
        }
    }

    public class ProjectAdded
    {

    }
}
