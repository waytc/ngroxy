#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NetworkResource.cs">
//     用户：朱宏飞
//     日期：2017/03/04
//     时间：10:27
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System.Net;

namespace Ngroxy.Modules
{
    public class NetworkResource
    {
        public User User { get; set; }
        public string Domain { get; set; }
        public IPEndPoint IPEndPoint { get; set; }
    }
}