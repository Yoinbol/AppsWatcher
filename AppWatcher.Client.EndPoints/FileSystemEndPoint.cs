﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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
        /// <param name="day"></param>
        /// <returns></returns>
        internal static string GetFileName(this DateTime day) 
        {
            return string.Format("{0}_{1}_{2}.xml", day.Year, day.Month, day.Day);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static XNode ToNode(this Session session)
        {
            XAttribute totalAttribute = new XAttribute("total", string.Empty);
            XElement sessionNode = new XElement("Session", totalAttribute, new XAttribute("day", session.Day.ToMyString()), new XAttribute("user", session.User.UserLogin));
            XElement applicationsNode = new XElement("Apps");
            sessionNode.Add(applicationsNode);
            TimeSpan total = new TimeSpan();

            foreach (var app in session.Applications)
            {
                XElement nameNode = new XElement("Name") { Value = app.ApplicationName };
                XElement timeNode = new XElement("Time") { Value = app.Duration.ToString() };
                XElement appNode = new XElement("App", nameNode, timeNode);
                applicationsNode.Add(appNode);
                total += app.Duration;
            }

            totalAttribute.Value = total.ToString();

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
        internal static XElement FindSession(this XDocument xdocument, Session session)
        {
            return xdocument
                .Element("Sessions")
                .Elements("Session")
                .FirstOrDefault(e => e.Attribute("user").Value.Equals(session.User.UserLogin, StringComparison.InvariantCultureIgnoreCase) &&
                                    e.Attribute("day").Value.Equals(session.Day.ToMyString(), StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        internal static ApplicationTrack ToApplicationTrack(this XElement element)
        {
            var nameNode = element.Element("Name");
            var timeNode = element.Element("Time");
            return new ApplicationTrack { ApplicationName = nameNode.Value, Duration = TimeSpan.Parse(timeNode.Value) };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FileSystemEndPoint : EndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string GetStorePath(Session session)
        {
            return System.IO.Path.Combine(this.StorePath, session.Day.GetFileName());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override Response Save(Session session)
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
                Log.Error(ex.Message);
                response.Succed = false;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public override SingleResponse<Session> LoadSession(DateTime day, string userLogin)
        {
            var response = new SingleResponse<Session>();

            try
            {
                //Create the session
                response.Data = new Session { Day = day, User = new User { UserLogin = userLogin } };

                //"Resolve" the session file path
                var path = this.GetStorePath(response.Data);

                //Does the file exists?
                if (File.Exists(path))
                {
                    //Load the document
                    XDocument xdocument = XDocument.Load(path);

                    //Search for the user session
                    var sessionNode = xdocument.FindSession(response.Data);

                    //Does the session exists?
                    if (sessionNode != null)
                    { 
                        //Load all the app tracks
                        foreach (var appNode in sessionNode.Element("Apps").Elements("App"))
                        {
                            response.Data.Applications.Add(appNode.ToApplicationTrack());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                response.Succed = false;
                response.Message = "Unexpected error";
            }

            return response;
        }
    }
}
