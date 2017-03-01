#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="SocksProtocolVersion.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:05
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion
namespace Ngroxy.Handlers.Socks
{
    public class SocksProtocolVersion
    {
        public const byte Socks4A = 0x04;
        public const byte Socks5 = 0x05;
        public const byte Unknown = 0xff;
    }
}