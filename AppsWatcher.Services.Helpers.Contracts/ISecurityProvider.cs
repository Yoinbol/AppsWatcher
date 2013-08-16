
namespace AppsWatcher.Services.Helpers.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int CreateSalt();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        byte[] Compute(string password, int salt);
    }
}
