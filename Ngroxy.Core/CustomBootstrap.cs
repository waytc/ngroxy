#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="CustomBootstrap.cs">
//     用户：朱宏飞
//     日期：2017/03/20
//     时间：19:12
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy
{
    using System.Configuration;
    using System.Net;
    using System.Net.Sockets;
    using DotNetty.Common.Internal.Logging;
    using DotNetty.Handlers.Logging;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using Microsoft.Extensions.Logging;
    using Ngroxy.Handlers;
    using Ngroxy.Handlers.Hprose;
    using Ngroxy.Modules;
    using NLog.Extensions.Logging;

    public class CustomBootstrap : ServerBootstrap
    {
        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.GetCurrentClassLogger();

        private readonly MultithreadEventLoopGroup _bossGroup = new MultithreadEventLoopGroup();
        private readonly MultithreadEventLoopGroup _workerGroup = new MultithreadEventLoopGroup();

        public CustomBootstrap()
        {
            var ngroxyEngine = new NgroxyEngine();
            var hproseHandler = new HproseHandler(ngroxyEngine);
            hproseHandler.Add(ngroxyEngine);
            Group(_bossGroup, _workerGroup);
            ChannelFactory(() => new TcpServerSocketChannel(AddressFamily.InterNetwork));
            Option(ChannelOption.SoBacklog, 1024);
            Handler(new LoggingHandler("Service-listen"));
            ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel => { channel.Pipeline.AddLast(new NgroxyServerHandler(hproseHandler)); }));
        }

        public void Run()
        {
            var port = int.Parse(ConfigurationManager.AppSettings.Get("port"));
            BindAsync(new IPEndPoint(IPAddress.Any, port)).Wait();
            Logger.LogInformation($"Server has started in port {port}");
        }
    }
}