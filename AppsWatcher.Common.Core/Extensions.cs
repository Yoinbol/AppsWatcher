using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppsWatcher.Common.Core
{
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ComponentDefinition ResolveComponentDefinition(this string value)
        {
            string[] parts = value.Split(',');

            return new ComponentDefinition
            {
                AssemblyName = parts[1].Trim(),
                ComponentName = parts[0].Trim()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="contractName"></param>
        /// <returns></returns>
        public static Type TryGetForcedComponent(this Configuration.ModuleConfig module, string contractName)
        {
            var config =
                module.Ensure.OfType<Configuration.Component>()
                      .FirstOrDefault(
                          c => c.ContractName.Equals(contractName, StringComparison.InvariantCultureIgnoreCase));

            if (config != null)
            {
                var componentDefinition = config.Type.ResolveComponentDefinition();
                var assembly = Assembly.Load(new AssemblyName(componentDefinition.AssemblyName));
                return assembly.GetType(componentDefinition.ComponentName, true, true);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes)
        {
            return new ASCIIEncoding().GetString(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string text)
        {
            return new ASCIIEncoding().GetBytes(text);
        }
    }
}
