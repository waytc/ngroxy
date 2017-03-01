#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5AddressType.cs">
//     用户：朱宏飞
//     日期：2017/02/28
//     时间：18:55
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace Ngroxy.Handlers.Socks.V5
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Socks5AddressType: ByteStatus
    {
        public static readonly Socks5AddressType IPv4 = new Socks5AddressType(0x01, nameof(IPv4));
        public static readonly Socks5AddressType Domain = new Socks5AddressType(0x03, nameof(Domain));
        public static readonly Socks5AddressType IPv6 = new Socks5AddressType(0x04, nameof(IPv6));

        /// <inheritdoc />
        public Socks5AddressType(byte value, string name) : base(value, name)
        {
        }

        public static Socks5AddressType ValueOf(byte value)
        {
            switch (value)
            {
                case 0x01:
                    return IPv4;
                case 0x03:
                    return Domain;
                case 0x04:
                    return IPv6;
                default:
                    return null;
            }
        }


        public static Socks5AddressType ValueOf(IPAddress value)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (value.AddressFamily)
            {
                case AddressFamily.InterNetwork:
                    return IPv4;
                case AddressFamily.InterNetworkV6:
                    return IPv6;
                default:
                    throw new NotSupportedException();
            }
        }


    }
}