#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Socks4CommandStatus.cs">
//     用户：朱宏飞
//     日期：2017/02/05
//     时间：13:28
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;

namespace Ngroxy.Handlers.Socks.V4
{
    public class Socks4CommandStatus : ByteStatus
    {
        public static readonly Socks4CommandStatus Success = new Socks4CommandStatus(0x5a, nameof(Success));
        public static readonly Socks4CommandStatus RejectedOrFailed = new Socks4CommandStatus(0x5b, nameof(RejectedOrFailed));
        public static readonly Socks4CommandStatus IdentdUnreachable = new Socks4CommandStatus(0x5c, nameof(IdentdUnreachable));
        public static readonly Socks4CommandStatus IdentdAuthFailure = new Socks4CommandStatus(0x5d, nameof(IdentdAuthFailure));
        
        public static Socks4CommandStatus ValueOf(byte value)
        {
            switch (value)
            {
                case 0x5a:
                    return Success;
                case 0x5b:
                    return RejectedOrFailed;
                case 0x5c:
                    return IdentdUnreachable;
                case 0x5d:
                    return IdentdAuthFailure;
                default:
                    return null;
            }
        }
        
        public Socks4CommandStatus(byte value, string name) : base(value, name) { }
        
    }
}