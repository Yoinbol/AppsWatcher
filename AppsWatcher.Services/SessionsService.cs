using System;
using System.Transactions;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using AppsWatcher.Repositories.Contracts;
using AppsWatcher.Services.Contracts;
using AppsWatcher.Services.Helpers.Contracts;
using Autofac;

namespace AppsWatcher.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionsService : BaseService, ISessionsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public SingleResponse<long> Save(Session session)
        {
            var response = new SingleResponse<long>();

            try
            {
                using (var lifetimeScope = ComponentsContainer.Instance.BeginLifetimeScope())
                {
                    using (var tranScope = new TransactionScope())
                    {
                        //Get the session (if exists)
                        var sessionsRepository = lifetimeScope.Resolve<ISessionsRepository>();
                        var storedSession = sessionsRepository.GetSession(session.Day, session.User.UserLogin);

                        //Does the session exists?
                        if (storedSession == null)
                        {
                            //The session does not exists
                            //Validate the user
                            var usersRepository = lifetimeScope.Resolve<IUsersRepository>();
                            var user = usersRepository.GetFirst(new { session.User.UserLogin });

                            //Do we need to add the user "automatically"
                            var configurationService = lifetimeScope.Resolve<IConfigurationService>();
                            if (user == null && configurationService.AutoSaveUsers)
                            { 
                                //The user does not exists. According to configuration we need to add him/her
                                user = session.User;
                                usersRepository.Add(user);
                            }

                            if (user != null)
                            {
                                //Add the session
                                storedSession = new Session { UserId = user.UserId, Day = session.Day, AddedOn = DateTime.Now };
                                sessionsRepository.Add(storedSession);

                                //Add each app track
                                var appTracksRepository = lifetimeScope.Resolve<IApplicationTracksRepository>();
                                foreach (var appTrack in session.Applications)
                                {
                                    var track = appTrack.Value;
                                    track.AddedOn = DateTime.Now;
                                    track.LastModifiedOn = DateTime.Now;
                                    appTracksRepository.Save(track);
                                }

                                //Commit
                                tranScope.Complete();
                            }
                            else
                            {
                                //The user does not exists
                                response.Succed = false;
                                response.Message = "User does not exists";
                            }
                        }
                        else
                        {
                            //The session exists
                            //Update all the apps
                            //Add each app track
                            var appTracksRepository = lifetimeScope.Resolve<IApplicationTracksRepository>();
                            foreach (var appTrack in session.Applications)
                            {
                                var track = appTrack.Value;
                                track.AddedOn = DateTime.Now;
                                track.LastModifiedOn = DateTime.Now;
                                appTracksRepository.Save(track);
                            }

                            //Commit
                            tranScope.Complete();
                        }

                        response.Data = storedSession.SessionId;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Succed = false;
                response.Message = "Unexpected error";
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="sessionId"></param>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public CollectionResponse<SessionHeader> GetSessions(int start, int end, long? sessionId = null, DateTime? day = null, string userLogin = null)
        {
            var response = new CollectionResponse<SessionHeader>();

            try
            {
                var repository = ComponentsContainer.Instance.Resolve<ISessionsRepository>();
                response.Data = repository.GetSessions(start, end, sessionId, day, userLogin);
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
        public SingleResponse<Session> GetSession(DateTime day, string userLogin)
        {
            var response = new SingleResponse<Session>();

            try
            {
                var repository = ComponentsContainer.Instance.Resolve<ISessionsRepository>();
                response.Data = repository.GetSession(day, userLogin);
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
