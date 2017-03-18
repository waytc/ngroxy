#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socket4Message.cs">
//     用户：朱宏飞
//     日期：2017/02/28
//     时间：13:21
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System.Net;

namespace Ngroxy.Handlers.Socks.V4
{
    public class Socket4Message
    {
        public SocksProtocolVersion Version { get; set; }
        public Socks4CommandType Command { get; set; }
        public ushort Port { get; set; }
        public IPAddress IPAddress { get; set; }
        public string Domain { get; set; }
    }
}