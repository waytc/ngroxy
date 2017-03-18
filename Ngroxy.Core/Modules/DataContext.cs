#region summary

//   ------------------------------------------------------------------------------------------------
//   <copyright file="DataContext.cs">
//     用户：朱宏飞
//     日期：2017/03/01
//     时间：19:32
//   </copyright>
//   ------------------------------------------------------------------------------------------------

#endregion

using System.Data.Entity;
using Ngroxy.Modules.Entities;

namespace Ngroxy.Modules
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.FriendGroup> FriendGroups { get; set; }
        public DbSet<Entities.Friend> Friends { get; set; }
        public DbSet<Entities.LocalAreaNetwork> LocalAreaNetworks { get; set; }
        public DbSet<Entities.LocalAreaNetworkUser> LocalAreaNetworkUsers { get; set; }
        public DbSet<Entities.DomainNameSystem> DomainNameSystems { get; set; }

        public DataContext() : base("meta") { }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");
        }
    }
}