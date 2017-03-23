#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyChannel.cs">
//     用户：朱宏飞
//     日期：2017/03/23
//     时间：20:23
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Transport
{
    using System.Net;
    using Ngroxy.Modules;

    public class NgroxyChannel : Channel<INgroxyEngine>
    {
        public NgroxyChannel(INgroxyEngine instance, IPEndPoint serverEndPoint)
            : base(instance, serverEndPoint)
        {
        }
    }
}