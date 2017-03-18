#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5AuthMethod.cs">
//     用户：朱宏飞
//     日期：2017/02/28
//     时间：18:29
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers.Socks.V5
{
    public class Socks5AuthMethod : ByteStatus
    {
        public static readonly Socks5AuthMethod NoAuth = new Socks5AuthMethod(0x00, nameof(NoAuth));

        public static readonly Socks5AuthMethod GssApi = new Socks5AuthMethod(0x01, nameof(GssApi));

        public static readonly Socks5AuthMethod Password = new Socks5AuthMethod(0x02, nameof(Password));

        public static readonly Socks5AuthMethod UnAccepted = new Socks5AuthMethod(0xff, nameof(UnAccepted));

        /// <inheritdoc />
        public Socks5AuthMethod(byte value, string name) : base(value, name)
        {
        }

        public static Socks5AuthMethod ValueOf(byte value)
        {
            switch (value)
            {
                case 0x00:
                    return NoAuth;
                case 0x01:
                    return GssApi;
                case 0x02:
                    return Password;
                case 0xff:
                    return UnAccepted;
                default:
                    return null;
            }
        }
    }
}