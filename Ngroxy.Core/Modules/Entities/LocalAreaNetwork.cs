#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="LocalAreaNetwork.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：19:02
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ngroxy.Modules.Entities
{
    [Table("local_area_network")]
    public class LocalAreaNetwork
    {
        [Column("id")]
        [Key]
        public int ID { get; set; }

        [Column("name")]
        public int Name { get; set; }

        [Column("owner_id")]
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<LocalAreaNetworkUser> LocalAreaNetworkUsers { get; set; }
    }
}