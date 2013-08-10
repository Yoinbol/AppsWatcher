using System;
using System.Collections;
using System.Linq;
using AppsWatcher.Client.EndPoints.Configuration;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemEndPoint : EndPoint, IEndPoint
    {
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
                var path = Config.Settings.OfType<EndPointSetting>().FirstOrDefault(s => s.Name.Equals("FilePath", StringComparison.InvariantCultureIgnoreCase)).Value;
                System.IO.StreamWriter writer = new System.IO.StreamWriter(path, false);
                IDictionaryEnumerator en = session.Applications.GetEnumerator();
                writer.Write("<?xml version=\"1.0\"?>");
                writer.Write("<ApplDetails>");
                while (en.MoveNext())
                {
                    if (!en.Value.ToString().Trim().StartsWith("0"))
                    {
                        var appInfo = en.Value as ApplicationInfo;

                        writer.Write("<Application_Info>");
                        writer.Write("<ProcessName>");
                        writer.Write(appInfo.ApplicationTitle);
                        writer.Write("</ProcessName>");

                        writer.Write("<ApplicationName>");
                        writer.Write(appInfo.ApplicationName);
                        writer.Write("</ApplicationName>");

                        writer.Write("<TotalSeconds>");
                        if ((appInfo.Seconds / 60) < 1)
                        {
                            writer.Write(Convert.ToInt32(appInfo.Seconds) + " Seconds");
                        }
                        else
                        {
                            writer.Write(Convert.ToInt32(appInfo.Seconds / 60) + " Minutes");
                        }
                        writer.Write("</TotalSeconds>");
                        writer.Write("</Application_Info>");
                    }
                }
                writer.Write("</ApplDetails>");
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                response.Succed = false;
            }

            return response;
        }
    }
}
