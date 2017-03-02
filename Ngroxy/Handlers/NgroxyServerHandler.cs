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
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Ngroxy.Handlers.Socks;
using Ngroxy.Handlers.Socks.V4;
using Ngroxy.Handlers.Socks.V5;

namespace Ngroxy.Handlers
{
    public class NgroxyServerHandler : ChannelHandlerAdapter
    {
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

                    // 判断是否是ngroxy协议
                    if (IsNgroxyProtocol(buffer))
                    {
                        context.Channel.Pipeline.Replace(this, nameof(NgroxyHandler), new NgroxyHandler());
                    }
                    break;
            }
            context.FireChannelRead(message);
        }

        private static bool IsNgroxyProtocol(IByteBuffer buffer)
        {
            return false;
        }
    }
}