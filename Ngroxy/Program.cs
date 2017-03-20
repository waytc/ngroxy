

namespace Ngroxy
{
    using System;
    using System.IO;
    using DotNetty.Common.Internal.Logging;
    using NLog.Extensions.Logging;

    internal class Program
    {
        static Program()
        {
            var dbFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", dbFile.FullName);
            InternalLoggerFactory.DefaultFactory.AddNLog();
        }
        
        private static void Main(string[] args)
        {
            var bootstrap = new CustomBootstrap();
            bootstrap.Run();
            Console.ReadKey();
      }
    }
}
