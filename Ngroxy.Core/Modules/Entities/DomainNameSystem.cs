#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="Class1.cs">
//     用户：朱宏飞
//     日期：2017/03/02
//     时间：18:31
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Ngroxy.Modules.Entities
{
    [Table("domain_name_system"),DebuggerDisplay("{Domain},{IP}")]
    public class DomainNameSystem
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("domain")]
        public string Domain { get; set; }

        [Column("ip")]
        public string IP { get; set; }

        [Column("port")]
        public int Port { get; set; } = -1;

        [Column("local_area_network_id")]
        [ForeignKey(nameof(LocalAreaNetwork))]
        public int LocalAreaNetworkId { get; set; } = -1;

        public LocalAreaNetwork LocalAreaNetwork { get; set; }
    }
}