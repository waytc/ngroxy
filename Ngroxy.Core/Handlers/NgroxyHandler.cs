#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：19:47
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Ngroxy.Modules;

namespace Ngroxy.Handlers
{
    public class NgroxyHandler : ChannelHandlerAdapter
    {
        /// <inheritdoc />
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer == null) return;
            var ngroxyContext = new NgroxyContext();
            ngroxyContext.Version = buffer.ReadByte();

            base.ChannelRead(context, message);
        }
    }
}