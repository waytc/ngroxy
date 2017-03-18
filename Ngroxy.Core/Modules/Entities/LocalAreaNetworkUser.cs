#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="LocalAreaNetworkUser.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：19:03
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngroxy.Modules.Entities
{
    [Table("local_area_network_user")]
    public class LocalAreaNetworkUser
    {
        [Column("id")]
        [Key]
        public int ID { get; set; }

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column("local_area_network_id")]
        [ForeignKey(nameof(LocalAreaNetwork))]
        public int LocalAreaNetworkId { get; set; }

        public LocalAreaNetwork LocalAreaNetwork { get; set; }

        public User User { get; set; }
    }
}