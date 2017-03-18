

// ReSharper disable once CheckNamespace
namespace NLog.Extensions.Logging
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Logging;
    using NLog.Config;

    /// <summary>
    /// Helpers for .NET Core
    /// </summary>
    public static class ConfigureExtensions
    {
        private static string GetClassFullName()
        {
            var skipFrames = 3;
            Type declaringType;
            string str;
            do
            {
                var method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == (Type)null)
                {
                    str = method.Name;
                    break;
                }
                ++skipFrames;
                str = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));
            return str;
        }

        public static Microsoft.Extensions.Logging.ILogger GetCurrentClassLogger(this ILoggerFactory factory)
        {
            return factory.CreateLogger(GetClassFullName());
        }

        /// <summary>
        /// Enable NLog as logging provider in .NET Core.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns>ILoggerFactory for chaining</returns>
        public static ILoggerFactory AddNLog(this ILoggerFactory factory)
        {
            return AddNLog(factory, null);
        }

        /// <summary>
        /// Enable NLog as logging provider in .NET Core.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="options">NLog options</param>
        /// <returns>ILoggerFactory for chaining</returns>
        public static ILoggerFactory AddNLog(this ILoggerFactory factory, NLogProviderOptions options)
        {
            //ignore this
            LogManager.AddHiddenAssembly(Assembly.Load(new AssemblyName("Microsoft.Extensions.Logging")));
            LogManager.AddHiddenAssembly(Assembly.Load(new AssemblyName("Microsoft.Extensions.Logging.Abstractions")));

            try
            {
                //try the Filter ext
                var filterAssembly = Assembly.Load(new AssemblyName("Microsoft.Extensions.Logging.Filter"));
                LogManager.AddHiddenAssembly(filterAssembly);
            }
            catch (Exception)
            {
                //ignore
            }
          
            LogManager.AddHiddenAssembly(typeof(ConfigureExtensions).GetTypeInfo().Assembly);

            using (var provider = new NLogLoggerProvider(options))
            {
                factory.AddProvider(provider);
            }
            return factory;
        }

        /// <summary>
        /// Apply NLog configuration from XML config.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configFileRelativePath">relative path to NLog configuration file.</param>
          /// <returns>Current configuration for chaining.</returns>
        public static LoggingConfiguration ConfigureNLog(this ILoggerFactory env, string configFileRelativePath)
        {
#if NETCORE
            var rootPath = System.AppContext.BaseDirectory;
#else
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
#endif

            var fileName = Path.Combine(rootPath, configFileRelativePath);
            return ConfigureNLog(fileName);
        }

      /// <summary>
       /// Apply NLog configuration from config object.
       /// </summary>
       /// <param name="env"></param>
       /// <param name="config">New NLog config.</param>
       /// <returns>Current configuration for chaining.</returns>
       public static LoggingConfiguration ConfigureNLog(this ILoggerFactory env, LoggingConfiguration config)
       {
           LogManager.Configuration = config;

           return config;
       }

        /// <summary>
        /// Apply NLog configuration from XML config.
        /// </summary>
        /// <param name="fileName">absolute path  NLog configuration file.</param>
          /// <returns>Current configuration for chaining.</returns>
        private static LoggingConfiguration ConfigureNLog(string fileName)
        {
            var config = new XmlLoggingConfiguration(fileName, true);
            LogManager.Configuration = config;
            return config;
        }
    }
}
