#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：19:47
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion


namespace Ngroxy.Handlers
{
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using Ngroxy.Modules;

    public class NgroxyHandler : ChannelHandlerAdapter
    {
        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer == null) return;
            
            base.ChannelRead(context, message);
        }
    }
}