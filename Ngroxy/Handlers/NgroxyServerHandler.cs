#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyServerHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：20:25
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System;
using System.Linq;
using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using Ngroxy.Handlers.Socks;
using Ngroxy.Handlers.Socks.V4;
using Ngroxy.Handlers.Socks.V5;

namespace Ngroxy.Handlers
{
    public class NgroxyServerHandler : ChannelHandlerAdapter
    {

        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.CreateLogger<SocksServerHandler>();

        private static readonly byte[] Ngroxy = {0x2};

        private IByteBuffer _cumulation;
        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer == null) return;
            if (_cumulation != null) buffer.WriteBytes(_cumulation);

            switch (buffer.GetByte(buffer.ReaderIndex))
            {
                case SocksProtocolVersion.Socks4A:
                    context.Channel.Pipeline.Replace(this, nameof(Socks4ServerHandler), new Socks4ServerHandler());
                    break;
                case SocksProtocolVersion.Socks5:
                    context.Channel.Pipeline.Replace(this, nameof(Socks5ServerHandler), new Socks5ServerHandler());
                    break;
                default:

                    // 判断是否是ngroxy协议

                    if (buffer.ReadableBytes < Ngroxy.Length)
                    {
                        if (_cumulation == null) _cumulation = buffer;
                        return;
                    }

                    if (IsNgroxyProtocol(buffer))
                    {
                        buffer.SkipBytes(Ngroxy.Length);
                        context.Channel.Pipeline.Replace(this, nameof(NgroxyHandler), new NgroxyHandler());
                    }
                    else
                    {
                        Logger.LogWarning("未知协议");
                    }
                    
                    break;
            }
            context.FireChannelRead(message);
        }

        private static bool IsNgroxyProtocol(IByteBuffer buffer)
            => !Ngroxy.Where((t, i) => t == buffer.GetByte(buffer.ReaderIndex + i)).Any();
    }
}