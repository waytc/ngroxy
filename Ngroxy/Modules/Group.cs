#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Group.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：13:15
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Ngroxy.Modules
{

    public class Group
    {
        public int ID { get; set; }

        [NotNull]
        public string Name { get;}

        public User Owner { get; set; }

        public ICollection<User> Users { get; set; }

        public string GetDomain(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return $"{user.Name}.{Name}";
        }

        public Group(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name is null or white space.");
            Name = name;
        }
    }
}