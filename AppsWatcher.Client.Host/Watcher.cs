using System;
using System.Collections.Generic;
using System.Diagnostics;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppWatcher.Client.EndPoints;

namespace AppsWatcher.Client.Host
{
    /// <summary>
    /// 
    /// </summary>
    internal class Watcher : System.Windows.Forms.Timer
    {
        private ApplicationKey _currentAppKey;
        private Session _session;
        private DateTime _applfocustime;
        //protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                    _session = new Session();
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
                var appltitle = hwnd.GetAppTitle();
                var appKey = new ApplicationKey { ApplicationName = appName, ApplicationTitle = appltitle };

                if (!Session.Applications.ContainsKey(appKey))
                {
                    Session.Applications.Add(appKey, new ApplicationInfo { ApplicationName = appName, ApplicationTitle = appltitle });
                }

                if (appKey != _currentAppKey)
                {
                    if (_currentAppKey != null && Session.Applications.ContainsKey(_currentAppKey))
                    {
                        var applfocusinterval = DateTime.Now.Subtract(_applfocustime);
                        var appInfo = Session.Applications[_currentAppKey];
                        appInfo.Seconds += applfocusinterval.TotalSeconds;
                    }

                    _currentAppKey = appKey;
                    _applfocustime = DateTime.Now;
                }

                //"Save"
                this.Save();
            }
            catch (Exception)
            {
                //log.Error(ex.Message);
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
                    if (DateTime.Now.Subtract(endPoint.LastSave).TotalSeconds > endPoint.Interval)
                    {
                        var saveResponse = endPoint.Save(this.Session);

                        if (saveResponse.Succed)
                        {
                            endPoint.LastSave = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception)
            { 
            }
        }
    }
}
