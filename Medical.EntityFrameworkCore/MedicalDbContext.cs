using Medical.Domain;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string TablePrefix = "medical_";
            modelBuilder.Entity<Admin>(config => {
                config.ToTable(TablePrefix + nameof(Admin));
                config.Property(m => m.UserName).HasMaxLength(50).IsRequired();
                config.Property(m => m.Password).HasMaxLength(50).IsRequired();
                config.Property(m => m.LastLoginIP).HasMaxLength(50);
            });
        }
    }
}
