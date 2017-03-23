#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="HproseClientHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/21
//     时间：20:04
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers.Hprose
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using DotNetty.Transport.Channels;
    using global::Hprose.Client;

    public class HproseClientHandler : HproseClient, IChannelHandler
    {
        
        /// <inheritdoc />
        protected override MemoryStream SendAndReceive(MemoryStream data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IAsyncResult BeginSendAndReceive(MemoryStream data, AsyncCallback callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override MemoryStream EndSendAndReceive(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }




        /// <inheritdoc />
        public void ChannelRegistered(IChannelHandlerContext context) => context.FireChannelRegistered();

        /// <inheritdoc />
        public void ChannelUnregistered(IChannelHandlerContext context) => context.FireChannelUnregistered();

        /// <inheritdoc />
        public void ChannelActive(IChannelHandlerContext context) => context.FireChannelActive();

        /// <inheritdoc />
        public void ChannelInactive(IChannelHandlerContext context) => context.FireChannelInactive();
        
        /// <inheritdoc />
        public void ChannelRead(IChannelHandlerContext context, object message)
        {

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