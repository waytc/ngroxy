#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5Handler.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:23
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;

namespace Ngroxy.Handlers.Socks.V5
{
    public class Socks5ServerHandler : ChannelHandlerAdapter
    {
        private State _state;
        private readonly Socks5AuthMethod _authMethod = Socks5AuthMethod.NoAuth;

        /// <inheritdoc />
        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            _state = State.Init;
            base.ChannelRegistered(context);
        }

        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            switch (_state)
            {
                case State.Init:



                    break;
                case State.Success:


                    break;
                case State.Failure:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (buffer?.ReadByte() != SocksProtocolVersion.Socks5)
            {
                var c = StringUtil.ToHexString(buffer.ToArray());
                return;
            }
            var command = Socks5CommandType.ValueOf(buffer.ReadByte());
            if (command == Socks5CommandType.Connect)
            {
                var method = Socks5AuthMethod.ValueOf(buffer.ReadByte());
                if (method == Socks5AuthMethod.NoAuth)
                {
                    if (buffer.ReadableBytes <= 0)
                    {
                        var response = context.Allocator.Buffer();
                        response.WriteByte(SocksProtocolVersion.Socks5);
                        response.WriteByte(method.Value);
                        context.WriteAndFlushAsync(response);
                        return;
                    }
                    var addressType = Socks5AddressType.ValueOf(buffer.ReadByte());
                    if (addressType == Socks5AddressType.Domain)
                    {
                        var length = buffer.ReadByte();
                        var domain = buffer.ToString(buffer.ReaderIndex, length, Encoding.ASCII);
                        buffer.SkipBytes(length);
                        var port = buffer.ReadUnsignedShort();

                        var bb = Dns.GetHostAddresses(domain).FirstOrDefault();
                        if (bb == null)
                        {
                            context.Channel.CloseAsync();
                            return;
                        }

                        Console.WriteLine("v5域名：{0}", domain);
                        context.Channel.Pipeline.Replace(this, nameof(TcpTransfer),
                            new TcpTransfer(context.Channel, new IPEndPoint(bb, port)));

                        var response = context.Allocator.Buffer();
                        response.WriteByte(SocksProtocolVersion.Socks5);
                        response.WriteByte(Socks5CommandStatus.Success.Value);
                        response.WriteByte(0x00);
                        response.WriteByte(Socks5AddressType.ValueOf(bb).Value);
                        response.WriteBytes(bb.GetAddressBytes());
                        response.WriteUnsignedShort(port);
                        var c = response.ToArray();
                        context.WriteAndFlushAsync(response);
                    }
                }
                else if (method== Socks5AuthMethod.Password)
                {
                    if (buffer.ReadableBytes <= 0)
                    {
                        var response = context.Allocator.Buffer();
                        response.WriteByte(SocksProtocolVersion.Socks5);
                        response.WriteByte(method.Value);
                        context.WriteAndFlushAsync(response);
                        return;
                    }

                    var c= StringUtil.ToHexString(buffer.ToArray());
                }
                else
                {
                    
                }
            }
            else if (command == Socks5CommandType.Bind)
            {
                
            }
            else if (command == Socks5CommandType.UdpAssociate)
            {
                
            }
            else
            {
                
            }
        }


        private enum State : byte
        {
            Init,
            Success,
            Failure
        }
    }
}