using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AppsWatcher.Client.EndPoints;
using AppsWatcher.Client.EndPoints.Configuration;
using AppsWatcher.Common.Core;
using log4net;

namespace AppsWatcher.Client.Host
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        private Watcher _watcher;

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            if (this.ConfigureEndPoints())
            {
                _watcher = new Watcher(Settings.Interval);
                _watcher.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private EndPointsConfigurationSection EndPointsConfiguration
        {
            get
            {
                return (EndPointsConfigurationSection)ConfigurationManager.GetSection("EndPointsConfiguration");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<EndPointConfig> EndPoints
        {
            get
            {
                return EndPointsConfiguration.EndPoints.OfType<EndPointConfig>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool ConfigureEndPoints()
        {
            bool configured = false;

            try
            {
                var endPoints = new List<IEndPoint>();
                var configuredEndPoints = this.EndPoints.Where(ep => ep.Enabled);

                if (configuredEndPoints.Any())
                {

                    foreach (var endPointConfig in configuredEndPoints)
                    {
                        var assembly = Assembly.Load(new AssemblyName(endPointConfig.Assembly));
                        var ep = assembly.CreateInstance(endPointConfig.Type);
                        var iep = ep as IEndPoint;
                        iep.Config = endPointConfig;
                        endPoints.Add(iep);
                    }

                    if (endPoints.Count > 0)
                    {
                        ComponentsContainer.Instance.RegisterComponent<IEnumerable<IEndPoint>>(endPoints);
                        configured = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                configured = false;
            }

            return configured;
        }
    }
}
