#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Friend.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：19:04
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion



namespace Ngroxy.Modules.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using JetBrains.Annotations;

    [Table("friend")]
    public class Friend
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("mine_id")]
        [ForeignKey(nameof(Me))]
        public int MineId { get; set; }

        [Column("your_id")]
        [ForeignKey(nameof(You))]
        public int YourId { get; set; }

        [Column("friend_group_id")]
        [ForeignKey(nameof(FriendGroup))]
        public int FriendGroupId { get; set; } = -1;

        public User Me { get; set; }

        public User You { get; set; }

        [CanBeNull]
        public FriendGroup FriendGroup { get; set; }
    }
}