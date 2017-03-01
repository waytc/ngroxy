#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="User.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：13:15
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Ngroxy
{
    public class User
    {
        public int ID { get; set; }

        [NotNull]
        public string Name { get;}

        public ICollection<UserGroup> UserGroups { get; set; }

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name is null or white space.");
            Name = name;
        }
    }
}