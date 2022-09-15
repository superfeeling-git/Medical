using Medical.Domain.Admins;
using Medical.Domain.Menus;
using Medical.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Medical.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class MedicalDbContext : AbpDbContext<MedicalDbContext>
    {
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Room> Rooms { get; set; }


        /// <summary>
        /// FluentAPI
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string TablePrefix = "medical_";
            modelBuilder.Entity<Admin>(config => {
                config.ToTable(TablePrefix + nameof(Admin));
                config.Property(m => m.UserName).HasMaxLength(50).IsRequired();
                config.Property(m => m.Password).HasMaxLength(50).IsRequired();
                config.Property(m => m.LastLoginIP).HasMaxLength(50);
                config.Ignore(m => m.ExtraProperties);
            });

            modelBuilder.Entity<Menu>(config => {
                config.ToTable(TablePrefix + nameof(Menu));
                config.Property(m => m.MenuName).HasMaxLength(50).IsRequired();
                config.Property(m => m.MenuPath).HasMaxLength(50).IsRequired();
                config.Property(m => m.MenuNameEn).HasMaxLength(50);
                config.Property(m => m.ComponentPath).HasMaxLength(50);
                config.Ignore(m => m.ExtraProperties);
            });

            modelBuilder.Entity<Room>(config => {
                config.ToTable(TablePrefix + nameof(Room));
                config.Property(m => m.RoomName).HasMaxLength(50).IsRequired();
                config.Property(m => m.BedNum).HasMaxLength(50).IsRequired();
                //外键
                config.HasOne<Menu>().WithMany().HasForeignKey(m => m.RegionId);
            });
        }
    }
}
