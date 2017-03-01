#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5CommandStatus.cs">
//     用户：朱宏飞
//     日期：2017/02/28
//     时间：19:58
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers.Socks.V5
{
    public class Socks5CommandStatus : ByteStatus
    {
        public static readonly Socks5CommandStatus Success = new Socks5CommandStatus(0x00, nameof(Success));
        public static readonly Socks5CommandStatus Failure = new Socks5CommandStatus(0x01, nameof(Failure));
        public static readonly Socks5CommandStatus Forbidden = new Socks5CommandStatus(0x02, nameof(Failure));
        public static readonly Socks5CommandStatus NetworkUnreachable = new Socks5CommandStatus(0x03, nameof(Failure));
        public static readonly Socks5CommandStatus HostUnreachable = new Socks5CommandStatus(0x04, nameof(Failure));
        public static readonly Socks5CommandStatus ConnectionRefused = new Socks5CommandStatus(0x05, nameof(Failure));
        public static readonly Socks5CommandStatus TtlExpired = new Socks5CommandStatus(0x06, nameof(Failure));
        public static readonly Socks5CommandStatus CommandUnsupported = new Socks5CommandStatus(0x07, nameof(CommandUnsupported));
        public static readonly Socks5CommandStatus AddressUnsupported = new Socks5CommandStatus(0x08, nameof(CommandUnsupported));

        /// <inheritdoc />
        public Socks5CommandStatus(byte value, string name) : base(value, name)
        {
        }

        public static Socks5CommandStatus ValueOf(byte value)
        {
            switch (value)
            {
                case 0x00:
                    return Success;
                case 0x01:
                    return Failure;
                case 0x02:
                    return Forbidden;
                case 0x03:
                    return NetworkUnreachable;
                case 0x04:
                    return HostUnreachable;
                case 0x05:
                    return ConnectionRefused;
                case 0x06:
                    return TtlExpired;
                case 0x07:
                    return CommandUnsupported;
                case 0x08:
                    return AddressUnsupported;
                default:
                    return null;
            }
        }
    }
}