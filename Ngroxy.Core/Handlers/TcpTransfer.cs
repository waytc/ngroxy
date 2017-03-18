#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="ChannelProxy.cs" company="bda">
//     用户：朱宏飞
//     日期：2016/12/16
//     时间：9:39
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Buffers;
    using DotNetty.Common.Internal;
    using DotNetty.Common.Internal.Logging;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using Microsoft.Extensions.Logging;
    using NLog.Extensions.Logging;

    /// <summary>
    /// Tcp数据代理转发
    /// </summary>
    public class TcpTransfer : ChannelHandlerAdapter
    {
        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.GetCurrentClassLogger();

        private readonly EndPoint _endPoint;
        private readonly IChannel _parentChannel;
        private IChannel _childChannel;

        /// <inheritdoc />
        public override bool IsSharable { get; } = true;

        public TcpTransfer(IChannel parentChannel, EndPoint endPoint)
        {
            if (parentChannel == null) throw new ArgumentNullException(nameof(parentChannel));
            parentChannel.Configuration.SetOption(ChannelOption.SoLinger, 0);
            parentChannel.Configuration.SetOption(ChannelOption.SoRcvbuf, 8192);
            parentChannel.Configuration.SetOption(ChannelOption.SoSndbuf, 8192);
            parentChannel.Configuration.SetOption(ChannelOption.TcpNodelay, true);
            _parentChannel = parentChannel;
            _endPoint = endPoint;
            Connect();
        }

        private async void Connect()
        {
            await Task.Run(() =>
            {
                var group = new MultithreadEventLoopGroup(3);
                var bootstrap = new Bootstrap();
                bootstrap.Group(group);
                bootstrap.ChannelFactory(() => new TcpSocketChannel(_endPoint.AddressFamily));
                bootstrap.Option(ChannelOption.TcpNodelay, true);
                bootstrap.Option(ChannelOption.SoLinger, 0);
                bootstrap.Option(ChannelOption.SoRcvbuf, 8192);
                bootstrap.Option(ChannelOption.SoSndbuf, 8192);
                bootstrap.Handler(this);
                bootstrap.ConnectAsync(_endPoint);
            });
        }

        private void DisConnect() => _childChannel?.CloseAsync();

        /// <inheritdoc />
        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            if (context.Channel.Equals(_parentChannel)) return;
            _childChannel = context.Channel;
        }

        /// <inheritdoc />
        public override void ChannelActive(IChannelHandlerContext context)
        {
            Logger.LogInformation("active：{0}", context.Name);
            if (!context.Channel.Equals(_childChannel)) return;
            if (_messageQueue.IsEmpty) return;
            while (!_messageQueue.IsEmpty)
            {
                if (_messageQueue.TryDequeue(out object message))
                    context.WriteAsync(message);
            }
            context.Flush();
        }

        private readonly ConcurrentQueue<object> _messageQueue = new CompatibleConcurrentQueue<object>();

        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (!(buffer?.ReadableBytes > 0)) return;
            if (_parentChannel.Equals(context.Channel))
            {
                if ((_childChannel != null) && _childChannel.Active)
                    _childChannel.WriteAndFlushAsync(message);
                else
                    _messageQueue.Enqueue(message);
            }
            else
            {
                _parentChannel.WriteAndFlushAsync(message);
            }
        }

        /// <inheritdoc />
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            try
            {
                DisConnect();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}