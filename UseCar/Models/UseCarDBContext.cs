using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UseCar.Models
{
    public partial class UseCarDBContext : DbContext
    {
        public UseCarDBContext()
        {
        }

        public UseCarDBContext(DbContextOptions<UseCarDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=192.168.43.63;Port=3306;Database=use_car;Uid=admin;Pwd=P@ssw0rd!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<department>(entity =>
            {
                entity.Property(e => e.departmentId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.departmentName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.userId).HasColumnType("int(11)");

                entity.Property(e => e.code)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.departmentId).HasColumnType("int(11)");

                entity.Property(e => e.email).HasColumnType("varchar(500)");

                entity.Property(e => e.firstName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.isActive).HasColumnType("bit(1)");

                entity.Property(e => e.isAdmin).HasColumnType("bit(1)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.lastName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.tel).HasColumnType("varchar(20)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");

                entity.Property(e => e.userName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });
        }
    }
}
