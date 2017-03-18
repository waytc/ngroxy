#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5Handler.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:21
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion



namespace Ngroxy.Handlers.Socks.V4
{
    using System.Linq;
    using System.Net;
    using System.Text;
    using DotNetty.Buffers;
    using DotNetty.Common.Internal.Logging;
    using DotNetty.Common.Utilities;
    using DotNetty.Transport.Channels;
    using Microsoft.Extensions.Logging;
    using Ngroxy.Modules;
    using NLog.Extensions.Logging;

    public class Socks4ServerHandler : ChannelHandlerAdapter
    {
        private static readonly ILogger Logger = InternalLoggerFactory.DefaultFactory.GetCurrentClassLogger();
        private static readonly IPAddress IPZero = IPAddress.Parse("0.0.0.1");

        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer?.ReadByte() != SocksProtocolVersion.Socks4A) return;
            var command = Socks4CommandType.ValueOf(buffer.ReadByte());
            if (command == null) return;
            if (command == Socks4CommandType.Connect)
            {
                var port = buffer.ReadShort();
                var ipBytes = new byte[4];
                buffer.ReadBytes(ipBytes);
                var ip = new IPAddress(ipBytes);
                if (!IPZero.Equals(ip))
                {

                }
                var verifyLength = buffer.ForEachByte(ByteProcessor.FIND_NUL);
                if (buffer.ReaderIndex < verifyLength)
                {
                    buffer.ToString(buffer.ReaderIndex, verifyLength, Encoding.UTF8);
                }
                buffer.SkipBytes(1);
                var domainLength = buffer.ForEachByte(ByteProcessor.FIND_NUL) - buffer.ReaderIndex;
                var domain = buffer.ToString(buffer.ReaderIndex, domainLength, Encoding.UTF8);
                buffer.SkipBytes(domainLength + 1);
                var bb = DomainNameSystem.Default.Query(domain).FirstOrDefault();
                if (bb == null)
                {
                    context.Channel.CloseAsync();
                    return;
                }

                var useSelfPort = false;
                if (bb.Port == ushort.MaxValue)
                {
                    bb.Port = port;
                    useSelfPort = true;
                }
                Logger.LogInformation("v4 domain：{0}", domain);
                context.Channel.Pipeline.Replace(this, nameof(TcpTransfer),
                    new TcpTransfer(context.Channel, bb));
                var response = context.Allocator.Buffer();
                response.WriteByte(0x00);
                response.WriteByte(Socks4CommandStatus.Success.Value);
                if (useSelfPort)
                {
                    response.WriteUnsignedShort(0);
                    response.WriteBytes(IPAddress.Any.GetAddressBytes());
                }
                else
                {
                    response.WriteUnsignedShort((ushort) bb.Port);
                    response.WriteBytes(bb.Address.GetAddressBytes());
                }
                context.Channel.WriteAndFlushAsync(response);
            }
            else if (command == Socks4CommandType.Bind)
            {
                
            }
            else
            {
                
            }
        }
    }
}