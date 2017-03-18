#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyMessageDecoder.cs">
//     用户：朱宏飞
//     日期：2017/03/03
//     时间：19:02
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion



namespace Ngroxy.Codes
{
    using System.Collections.Generic;
    using DotNetty.Codecs;
    using DotNetty.Transport.Channels;
    using Ngroxy.Modules;

    public class NgroxyMessageDecoder : MessageToMessageDecoder<NgroxyMessage>
    {
        /// <inheritdoc />
        protected override void Decode(IChannelHandlerContext context, NgroxyMessage message, List<object> output)
        {
            throw new System.NotImplementedException();
        }
    }
}