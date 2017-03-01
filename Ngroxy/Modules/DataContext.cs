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
        public DbSet<User> Users { get; set; }
        public DbSet<FriendGroup> FriendGroups { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<LocalAreaNetwork> LocalAreaNetworks { get; set; }
        public DbSet<LocalAreaNetworkUser> LocalAreaNetworkUsers { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");
        }
    }
}