using System;
using System.Net.Http;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiEndPoint : EndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public override string GetStorePath(Session session)
        {
            return string.Format("{0}/{1}", this.StorePath, "Sessions/Save");
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
                //Create the web api client
                using (var client = new HttpClient())
                {
                    //Post the session
                    client.PostAsJsonAsync<Session>(this.GetStorePath(session), session).Wait();
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
                var url = string.Format("{0}/Sessions/GetSession?day={1}&userLogin={2}", this.StorePath, day.ToString(), userLogin);

                //Create the web api client
                using (var client = new HttpClient())
                {
                    var result = client.GetAsync(url).Result;
                    response = result.Content.ReadAsAsync<SingleResponse<Session>>().Result;
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
