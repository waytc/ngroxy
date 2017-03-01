#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks5CommandType.cs">
//     用户：朱宏飞
//     日期：2017/02/28
//     时间：19:15
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

namespace Ngroxy.Handlers.Socks.V5
{
    public class Socks5CommandType : ByteStatus
    {
        public static readonly Socks5CommandType Connect = new Socks5CommandType(0x01, nameof(Connect));
        public static readonly Socks5CommandType Bind = new Socks5CommandType(0x02, nameof(Bind));
        public static readonly Socks5CommandType UdpAssociate = new Socks5CommandType(0x03, nameof(UdpAssociate));
        
        /// <inheritdoc />
        public Socks5CommandType(byte value, string name) : base(value, name)
        {
        }


        public static Socks5CommandType ValueOf(byte value)
        {
            switch (value)
            {
                case 0x01:
                    return Connect;
                case 0x02:
                    return Bind;
                case 0x03:
                    return UdpAssociate;
                default:
                    return null;
            }
        }

    }
}