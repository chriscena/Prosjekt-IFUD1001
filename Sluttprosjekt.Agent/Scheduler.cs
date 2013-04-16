using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace Sluttprosjekt.Agent
{
    public class Scheduler
    {
        private const string PeriodicTaskName = "FriendsPeriodicAgent";
        private PeriodicTask _periodic;

        public void StartPeriodic()
        {
            // Variable for tracking enabled status of background agents for this app.
            AreAgentsEnabled = true;

            // Obtain a reference to the period task, if one exists
            _periodic = ScheduledActionService.Find(PeriodicTaskName) as PeriodicTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (_periodic != null)
            {
                RemoveAgent(PeriodicTaskName);
            }

            _periodic = new PeriodicTask(PeriodicTaskName)
            {
                Description = "Polls the web service to check if there are new Friends.",
                ExpirationTime = DateTime.Now + TimeSpan.FromHours(5)
            };

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(_periodic);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
                ScheduledActionService.LaunchForTest(
                    PeriodicTaskName, 
                    TimeSpan.FromMinutes(1));
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    AreAgentsEnabled = false;
                }

                if (exception.Message.Contains(
                    "BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                    AreAgentsEnabled = false;
                }
            }
            catch (SchedulerServiceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void StopPeriodic()
        {
            if (_periodic != null)
            {
                RemoveAgent(PeriodicTaskName);
                _periodic = null;
            }

            var mainTile = ShellTile.ActiveTiles.First();
            mainTile.Update(
                new StandardTileData
                {
                    Count = 0,
                    BackTitle = null
                });
        }

        private static void RemoveAgent(string periodicTaskName)
        {
            try
            {
                ScheduledActionService.Remove(periodicTaskName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool AreAgentsEnabled 
        { 
            get; 
            set; 
        }
    }
}
