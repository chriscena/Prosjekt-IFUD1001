using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
//using Sluttprosjekt.Model;

namespace Sluttprosjekt.Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        public const string CountSettingsKey = "FriendsCount";

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            //var service = new DataService();
            //var list = await service.GetFriends();

            //int friendsCount;
            //IsolatedStorageSettings.ApplicationSettings
            //    .TryGetValue(CountSettingsKey, out friendsCount);

            //if (list.Count != friendsCount)
            //{
            //    ShowToast("Friends list changed!");

            //    var mainTile = ShellTile.ActiveTiles.First();

            //    mainTile.Update(new StandardTileData
            //    {
            //        Count = list.Count,
            //        BackTitle = "List changed!"
            //    });

            //    SaveSettings(list.Count);
            //}

            //// If debugging is enabled, use LaunchForTest to launch the agent in one minute.
            //ScheduledActionService.LaunchForTest(
            //    task.Name,
            //    TimeSpan.FromMinutes(1));

            //NotifyComplete();
        }

        private void ShowToast(string message)
        {
            var toast = new ShellToast
            {
                Title = "Friends!",
                Content = message
            };

            toast.Show();
        }

        public static void SaveSettings(int count)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(CountSettingsKey))
            {
                IsolatedStorageSettings.ApplicationSettings[CountSettingsKey] = count;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(CountSettingsKey, count);
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}