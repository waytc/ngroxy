#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="NgroxyEngine.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:20
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

namespace Ngroxy.Modules
{
    using System.Collections.Generic;
    using Hprose.Common;
    using Ngroxy.Handlers.Hprose;

    public class NgroxyEngine:IHproseContextFatory
    {
        public const byte Version = 0x01;
        public User User { get; set; }
        public ICollection<User> Users { get; set; }

        private NgroxyContext GetCurrentContext()
        {
            if (User == null)
                return null;
            return null;
        }

        public void Login(string username, string password)
        {
            var context = GetCurrentContext();
        }

        public void Logout()
        {

        }


        HproseContext IHproseContextFatory.Create()
        {
            return null;
        }

    }
}