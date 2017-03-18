#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyMessageEncoder.cs">
//     用户：朱宏飞
//     日期：2017/03/03
//     时间：19:03
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion



namespace Ngroxy.Codes
{
    using System.Collections.Generic;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;
    using Ngroxy.Modules;

    public class NgroxyMessageEncoder : MessageToMessageEncoder<NgroxyMessage>
    {
        /// <inheritdoc />
        protected override void Encode(IChannelHandlerContext context, NgroxyMessage message, List<object> output)
        {
            throw new System.NotImplementedException();
        }
    }
}