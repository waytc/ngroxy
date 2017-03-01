#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="User.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:41
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngroxy.Modules.Entities
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public int Name { get; set; }

        [Column("password")]
        public string Password { get; set; }

        public ICollection<LocalAreaNetwork> LocalAreaNetworks { get; set; }
        public ICollection<FriendGroup> FriendGroups { get; set; }
    }
}