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
using Ngroxy.Handlers.Socks.V4;
using Ngroxy.Handlers.Socks.V5;

namespace Ngroxy.Handlers.Socks
{
    public class SocksServerHandler: ChannelHandlerAdapter
    {
        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer == null) return;
            var socksProtocolVersion = buffer.GetByte(buffer.ReaderIndex);
            switch (socksProtocolVersion)
            {
                case SocksProtocolVersion.Socks4A:
                    context.Channel.Pipeline.AddLast(new Socks4ServerHandler());
                    break;
                case SocksProtocolVersion.Socks5:
                    context.Channel.Pipeline.AddLast(new Socks5ServerHandler());
                    break;
                default:
                    Console.WriteLine("未知协议");
                    break;
            }
            context.Channel.Pipeline.Remove(this);
            context.FireChannelRead(message);
        }
    }
}