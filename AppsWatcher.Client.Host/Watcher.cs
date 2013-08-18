using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using AppsWatcher.Client.EndPoints;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using log4net;

namespace AppsWatcher.Client.Host
{
    /// <summary>
    /// 
    /// </summary>
    internal class Watcher : System.Windows.Forms.Timer
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private string _currentAppKey;

        /// <summary>
        /// 
        /// </summary>
        private Session _session;

        /// <summary>
        /// 
        /// </summary>
        private DateTime _applfocustime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        internal Watcher(int interval)
        {
            //Configure the timer
            this.Interval = interval;
            this.Tick += new System.EventHandler(this.TimerTick);
        }

        /// <summary>
        /// 
        /// </summary>
        public Session Session
        {
            get
            {
                if (_session == null)
                {
                    //Get the "default" endpoint
                    var defaultEndPoint = ComponentsContainer.Instance.Resolve<IEnumerable<IEndPoint>>().First(ep => ep.Config.AutoLoadSession);

                    //Load the session from the default endpoint
                    var sessionResponse = defaultEndPoint.LoadSession(DateTime.Now.Date, WindowsIdentity.GetCurrent().Name);

                    //Was the session loaded successfully?
                    if(sessionResponse.Succed)
                    {
                        _session = sessionResponse.Data;
                    }
                }

                return _session;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;

                IntPtr hwnd = MyWin32.GetForegroundWindow();
                Int32 pid = MyWin32.GetWindowProcessID(hwnd);
                Process p = Process.GetProcessById(pid);
                var appName = p.ProcessName;

                if (!Session.Applications.Any(app => app.ApplicationName.Equals(appName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Session.Applications.Add(new ApplicationTrack { ApplicationName = appName, Duration = new TimeSpan() });
                }

                if (appName != _currentAppKey)
                {
                    if (_currentAppKey != null)
                    {
                        var appInfo = Session.Applications.FirstOrDefault(app => app.ApplicationName.Equals(_currentAppKey, StringComparison.InvariantCultureIgnoreCase));

                        if (appInfo != null)
                        {
                            var applfocusinterval = DateTime.Now.Subtract(_applfocustime);
                            appInfo.Duration += applfocusinterval;
                        }
                    }

                    _currentAppKey = appName;
                    _applfocustime = DateTime.Now;
                }

                //Save the session info into the configured endpoints
                this.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Save()
        {
            try
            {
                var endPoints = ComponentsContainer.Instance.Resolve<IEnumerable<IEndPoint>>();

                foreach (var endPoint in endPoints)
                {
                    try
                    {
                        if (DateTime.Now.Subtract(endPoint.LastSave).TotalMilliseconds > endPoint.Config.Interval)
                        {
                            var saveResponse = endPoint.Save(this.Session);

                            if (saveResponse.Succed)
                            {
                                endPoint.LastSave = DateTime.Now;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
