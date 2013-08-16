using System;
using System.Security.Cryptography;
using AppsWatcher.Services.Helpers.Contracts;
using AppsWatcher.Common.Core;

namespace AppsWatcher.Services.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityProvider : ISecurityProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CreateSalt()
        {
            Byte[] _saltBytes = new Byte[4];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(_saltBytes);
            }

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
              (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public byte[] Compute(string password, int salt)
        {
            // Create Byte array of password string
            Byte[] _secretBytes = password.GetBytes();

            Byte[] _saltBytes = new Byte[4];
            _saltBytes[0] = (byte)(salt >> 24);
            _saltBytes[1] = (byte)(salt >> 16);
            _saltBytes[2] = (byte)(salt >> 8);
            _saltBytes[3] = (byte)(salt);

            // append the two arrays
            Byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            return SHA1.Create().ComputeHash(toHash);
        }
    }
}
