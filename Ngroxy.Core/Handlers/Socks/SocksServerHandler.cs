#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="SocksServerHandler.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：2:47
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System;
using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using Ngroxy.Handlers.Socks.V4;
using Ngroxy.Handlers.Socks.V5;
using NLog;
using NLog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Ngroxy.Handlers.Socks
{
    public class SocksServerHandler : ChannelHandlerAdapter
    {
        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.GetCurrentClassLogger();
        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer == null) return;

            switch (buffer.GetByte(buffer.ReaderIndex))
            {
                case SocksProtocolVersion.Socks4A:
                    context.Channel.Pipeline.Replace(this, nameof(Socks4ServerHandler), new Socks4ServerHandler());
                    break;
                case SocksProtocolVersion.Socks5:
                    context.Channel.Pipeline.Replace(this, nameof(Socks5ServerHandler), new Socks5ServerHandler());
                    break;
                default:
                    Logger.LogError("Unknow socks verion protocol.");
                    break;
            }
            context.FireChannelRead(message);
        }
    }
}