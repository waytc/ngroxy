#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="User.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：13:15
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion


namespace Ngroxy.Modules
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public class User
    {
        public INgroxyEngine NgroxyEngine { get; set; }

        public int ID { get; set; }
        
        public string Name { get;}

        public ICollection<Group> Groups { get; set; }

        public User()
        {
        }

        public ICollection<NetworkResource> NetworkResources { get; set; }
    }
}