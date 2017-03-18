#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyEngine.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:20
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;

namespace Ngroxy.Modules
{
    public class NgroxyEngine
    {
        public const byte Version = 0x01;
        public User User { get; set; }
        public ICollection<User> Users { get; set; }
        
        public void PipeIn(NgroxyContext context, Packet packet)
        {
        }

        public void PipeOut(NgroxyContext context, Packet packet)
        {
        }
    }
}