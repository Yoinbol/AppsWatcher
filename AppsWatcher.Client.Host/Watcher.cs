using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Xml.Linq;
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
        private string _currentAppKey;
        private Session _session;
        private DateTime _applfocustime;
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                    _session = new Session
                    {
                        Day = DateTime.Now.Date,
                        User = new User { UserLogin = WindowsIdentity.GetCurrent().Name }
                    };

                    _session.Applications = this.LoadSessionApplications(_session);
                }

                return _session;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private Dictionary<string, ApplicationTrack> LoadSessionApplications(Session session)
        {
            var apps = new Dictionary<string, ApplicationTrack>();

            try
            {
                var fileSystemEndPoint = ComponentsContainer.Instance.Resolve<IEnumerable<IEndPoint>>().First(ep => ep is FileSystemEndPoint) as FileSystemEndPoint;
                var path = fileSystemEndPoint.GetStorePath(session);

                if (File.Exists(path))
                {
                    XDocument xdocument = XDocument.Load(path);

                    //TODO...
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return apps;
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

                if (!Session.Applications.ContainsKey(appName))
                {
                    Session.Applications.Add(appName, new ApplicationTrack { ApplicationName = appName, Duration = new TimeSpan() });
                }

                if (appName != _currentAppKey)
                {
                    if (_currentAppKey != null && Session.Applications.ContainsKey(_currentAppKey))
                    {
                        var applfocusinterval = DateTime.Now.Subtract(_applfocustime);
                        var appInfo = Session.Applications[_currentAppKey];
                        appInfo.Duration += applfocusinterval;
                    }

                    _currentAppKey = appName;
                    _applfocustime = DateTime.Now;
                }

                //"Save"
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
