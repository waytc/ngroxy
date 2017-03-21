#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="HproseHandler.cs" company="bda">
//     用户：朱宏飞
//     日期：2016/12/22
//     时间：12:57
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers.Hprose
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using global::Hprose.Common;
    using global::Hprose.Server;

    /// <summary>
    /// High Performance Remote Object Service Engine For DotNetty.
    /// </summary>
    public class HproseHandler : HproseService, IChannelHandler
    {
        private readonly IHproseContextFatory _fatory;
        public HproseHandler()
        {
        }
        public HproseHandler(IHproseContextFatory fatory)
        {
            _fatory = fatory;
        }

        /// <inheritdoc />
        public override HproseMethods GlobalMethods => gMethods ?? (gMethods = new HproseTcpListenerMethods());

        /// <inheritdoc />
        public void ChannelRegistered(IChannelHandlerContext context) => context.FireChannelRegistered();

        /// <inheritdoc />
        public void ChannelUnregistered(IChannelHandlerContext context) => context.FireChannelUnregistered();

        /// <inheritdoc />
        public void ChannelActive(IChannelHandlerContext context) => context.FireChannelActive();

        /// <inheritdoc />
        public void ChannelInactive(IChannelHandlerContext context) => context.FireChannelInactive();

        private HproseContext CreateContext() => _fatory?.Create() ?? new HproseContext();

        /// <inheritdoc />
        public void ChannelRead(IChannelHandlerContext context, object message)
        {
            var input = message as IByteBuffer;
            if (input == null) return;
            var istream = new MemoryStream(input.ToArray());
            input.Release();
            using (istream)
            {
                var hproseContext = CreateContext();

                var oStream = Handle(istream, null, hproseContext);
                using (oStream)
                {
                    var bytes = oStream.ToArray();
                    context.WriteAndFlushAsync(context.Allocator.Buffer(bytes.Length).WriteBytes(bytes));
                }
            }
        }

        /// <inheritdoc />
        public void ChannelReadComplete(IChannelHandlerContext context) => context.FireChannelReadComplete();

        /// <inheritdoc />
        public void ChannelWritabilityChanged(IChannelHandlerContext context) => context.FireChannelWritabilityChanged();

        /// <inheritdoc />
        [Skip]
        public void HandlerAdded(IChannelHandlerContext context)
        {
        }

        /// <inheritdoc />
        [Skip]
        public void HandlerRemoved(IChannelHandlerContext context)
        {
        }

        /// <inheritdoc />
        public Task WriteAsync(IChannelHandlerContext context, object message) => context.WriteAsync(message);

        /// <inheritdoc />
        public void Flush(IChannelHandlerContext context) => context.Flush();

        /// <inheritdoc />
        public Task BindAsync(IChannelHandlerContext context, EndPoint localAddress) => context.BindAsync(localAddress);

        /// <inheritdoc />
        public Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
            => context.ConnectAsync(remoteAddress, localAddress);

        /// <inheritdoc />
        public Task DisconnectAsync(IChannelHandlerContext context) => context.DisconnectAsync();

        /// <inheritdoc />
        public Task CloseAsync(IChannelHandlerContext context) => context.CloseAsync();

        /// <inheritdoc />
        public void ExceptionCaught(IChannelHandlerContext context, Exception exception)
            => context.FireExceptionCaught(exception);

        /// <inheritdoc />
        public Task DeregisterAsync(IChannelHandlerContext context) => context.DeregisterAsync();

        /// <inheritdoc />
        public void Read(IChannelHandlerContext context) => context.Read();

        /// <inheritdoc />
        public void UserEventTriggered(IChannelHandlerContext context, object evt)
            => context.FireUserEventTriggered(evt);
    }
}