﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Sluttprosjekt.Helpers;
using Sluttprosjekt.Model;

namespace Sluttprosjekt.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDataService>(() => new DataService());
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationService());
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ProjectsViewModel>();
            SimpleIoc.Default.Register<AddProjectViewModel>();
            SimpleIoc.Default.Register<AddMemberViewModel>();
            SimpleIoc.Default.Register<ActiveProjectViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Gets the AddProject property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddProjectViewModel AddProject
        {
            get
            {
                return SimpleIoc.Default.GetInstance<AddProjectViewModel>();
            }
        }

        /// <summary>
        /// Gets the AddProject property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddMemberViewModel AddMember
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddMemberViewModel>();
            }
        }

        /// <summary>
        /// Gets the AddProject property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ActiveProjectViewModel ActiveProjectViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ActiveProjectViewModel>();
            }
        }

        /// <summary>
        /// Gets the AddProject property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ProjectsViewModel ProjectsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectsViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}