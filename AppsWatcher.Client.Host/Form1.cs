using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AppsWatcher.Common.Core;
using AppWatcher.Client.EndPoints;

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

            this.ConfigureEndPoints();

            _watcher = new Watcher(Configuration.Interval);
            _watcher.Start();
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
        private void ConfigureEndPoints()
        {
            var endPoints = new List<IEndPoint> { new FileSystemEndPoint() };
            ComponentsContainer.Instance.RegisterComponent<IEnumerable<IEndPoint>>(endPoints);
        }
    }
}
