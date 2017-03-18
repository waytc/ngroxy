#region summary
//   ------------------------------------------------------------------------------------------------
//   <copyright file="Meta_Friend.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：18:42
//   </copyright>
//   ------------------------------------------------------------------------------------------------
#endregion


namespace Ngroxy.Modules.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("friend_group")]
    public class FriendGroup
    {
        [Column("id"), Key]
        public int ID { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        [Column("owner_id"), ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        
        public User Owner { get; set; }

        public ICollection<Friend> Friends { get; set; }
    }
}