#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks4CommandType.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:44
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion



namespace Ngroxy.Handlers.Socks.V4
{
    public class Socks4CommandType : ByteStatus
    {
        public static readonly Socks4CommandType Connect = new Socks4CommandType(0x01, nameof(Connect));
        public static readonly Socks4CommandType Bind = new Socks4CommandType(0x02, nameof(Bind));

        public static Socks4CommandType ValueOf(byte value)
        {
            switch (value)
            {
                case 0x01:
                    return Connect;
                case 0x02:
                    return Bind;
                default:
                    return null;
            }
        }

        public Socks4CommandType(byte value, string name) : base(value, name) { }
    }
}