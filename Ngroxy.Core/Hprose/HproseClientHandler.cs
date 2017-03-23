#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="HproseClientHandler.cs">
//     用户：朱宏飞
//     日期：2017/03/21
//     时间：20:23
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Hprose
{
    using System;
    using System.IO;
    using global::Hprose.Client;

    public class HproseClientHandler :HproseClient
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
    }
}