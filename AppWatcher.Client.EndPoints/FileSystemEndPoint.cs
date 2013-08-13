using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using AppsWatcher.Client.EndPoints.Configuration;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Client.EndPoints
{
    static class LocalExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string ToMyString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static string GetStorePath(this Session session) 
        {
            return string.Format("{0}_{1}_{2}.xml", session.Day.Year, session.Day.Month, session.Day.Day);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static XNode ToNode(this Session session)
        {
            XElement sessionNode = new XElement("Session", new XAttribute("user", session.User.UserLogin), new XAttribute("day", session.Day.ToMyString()));
            XElement applicationsNode = new XElement("Apps");
            sessionNode.Add(applicationsNode);

            foreach (var app in session.Applications)
            {
                XElement nameNode = new XElement("Name") { Value = app.Value.ApplicationName };
                XElement timeNode = new XElement("Time") { Value = app.Value.Seconds.ToFriendlyTime() };
                XElement appNode = new XElement("App", nameNode, timeNode);
                applicationsNode.Add(appNode);
            }

            return sessionNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        internal static string ToFriendlyTime(this double seconds)
        {
            if ((seconds / 60) < 1)
            {
                return string.Format("{0} Seconds", Convert.ToInt32(seconds));
            }
            else
            {
                return string.Format("{0} Minutes", Convert.ToInt32(seconds / 60));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdocument"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static XNode FindSession(this XDocument xdocument, Session session)
        {
            return xdocument
                .Element("Sessions")
                .Elements("Session")
                .FirstOrDefault(e => e.Attribute("user").Value.Equals(session.User.UserLogin, StringComparison.InvariantCultureIgnoreCase) &&
                                    e.Attribute("day").Value.Equals(session.Day.ToMyString(), StringComparison.InvariantCultureIgnoreCase));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FileSystemEndPoint : EndPoint, IEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        private string DirPath
        {
            get
            {
                return Config.Settings.OfType<EndPointSetting>().FirstOrDefault(s => s.Name.Equals("FilePath", StringComparison.InvariantCultureIgnoreCase)).Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string GetStorePath(Session session)
        {
            return System.IO.Path.Combine(DirPath, session.GetStorePath());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Response Save(Session session)
        {
            var response = new Response();

            try
            {
                var path = this.GetStorePath(session);

                if (File.Exists(path))
                {
                    XDocument xdocument = XDocument.Load(path);
                    XNode sessionNode = xdocument.FindSession(session);

                    if (sessionNode != null)
                        sessionNode.Remove();

                    xdocument.Element("Sessions").Add(session.ToNode());
                    xdocument.Save(path);
                }
                else
                {
                    //File does not exists
                    //Save the session into the repository
                    XDocument xdocument = new XDocument();
                    XElement sessionsNode = new XElement("Sessions");
                    XNode sessionNode = session.ToNode();
                    sessionsNode.Add(sessionNode);
                    xdocument.Add(sessionsNode);
                    xdocument.Save(path);

                }
            }
            catch (Exception ex)
            {
                response.Succed = false;
            }

            return response;
        }
    }
}
