#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyServerHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：20:25
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers
{
    using System.Linq;
    using DotNetty.Buffers;
    using DotNetty.Codecs;
    using DotNetty.Common.Internal.Logging;
    using DotNetty.Transport.Channels;
    using Microsoft.Extensions.Logging;
    using Ngroxy.Codes;
    using Ngroxy.Handlers.Hprose;
    using Ngroxy.Handlers.Socks;
    using Ngroxy.Handlers.Socks.V4;
    using Ngroxy.Handlers.Socks.V5;
    using Ngroxy.Modules;

    public class NgroxyServerHandler : ChannelHandlerAdapter
    {

        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.CreateLogger<SocksServerHandler>();

        private static readonly byte[] Ngroxy = {0x4E, 0x67, 0x72, 0x6F, 0x78, 0x79};

        private readonly HproseHandler _hproseHandler;

        public NgroxyServerHandler(HproseHandler hproseHandler)
        {
            _hproseHandler = hproseHandler;
        }

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
                        context.Channel.Pipeline.Remove(this);
                        context.Channel.Pipeline.AddLast(new LengthFieldPrepender(4));
                        context.Channel.Pipeline.AddLast(new LengthFieldBasedFrameDecoder(int.MaxValue, 0, sizeof(int), 0, 4));
                        context.Channel.Pipeline.AddLast(_hproseHandler);
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