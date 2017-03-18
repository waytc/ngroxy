using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using DotNetty.Common.Internal.Logging;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using Ngroxy.Handlers.Socks;
using NLog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Ngroxy
{
    internal class Program
    {
        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.CreateLogger<Program>();

        static Program()
        {
            var dbFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", dbFile.FullName);
            InternalLoggerFactory.DefaultFactory.AddNLog();
        }

        private static readonly MultithreadEventLoopGroup BossGroup = new MultithreadEventLoopGroup();
        private static readonly MultithreadEventLoopGroup WorkerGroup = new MultithreadEventLoopGroup();

        private static void Main(string[] args)
        {
            var bootstrap = new ServerBootstrap();
            bootstrap.Group(BossGroup, WorkerGroup);
            bootstrap.ChannelFactory(() => new TcpServerSocketChannel(AddressFamily.InterNetwork));
            bootstrap.Option(ChannelOption.SoBacklog, 1024);
            bootstrap.Handler(new LoggingHandler("SRV-LSTN"));
            //                bootstrap.Handler(new CheckHandler());
            bootstrap.ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
            {
                channel.Pipeline.AddLast(new SocksServerHandler());
            }));
            var port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            bootstrap.BindAsync(new IPEndPoint(IPAddress.Any, port)).Wait();
            Logger.LogInformation($"Server has started in port {port}");
            Console.ReadKey();
      }
    }
}
