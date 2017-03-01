using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Ngroxy.Handlers.Socks;

namespace ProxyTest
{
    class Program
    {
        private static readonly MultithreadEventLoopGroup BossGroup = new MultithreadEventLoopGroup();
        private static readonly MultithreadEventLoopGroup WorkerGroup = new MultithreadEventLoopGroup();
        static void Main(string[] args)
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
            bootstrap.BindAsync(new IPEndPoint(IPAddress.Any, 1026)).Wait();
            Console.ReadKey();
        }
    }
}
