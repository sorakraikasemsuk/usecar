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

        public virtual DbSet<branch> branch { get; set; }
        public virtual DbSet<brand> brand { get; set; }
        public virtual DbSet<capacityengine> capacityengine { get; set; }
        public virtual DbSet<car> car { get; set; }
        public virtual DbSet<car_option> car_option { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<categoryshop> categoryshop { get; set; }
        public virtual DbSet<color> color { get; set; }
        public virtual DbSet<contactrepairshop> contactrepairshop { get; set; }
        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<drivesystem> drivesystem { get; set; }
        public virtual DbSet<enginetype> enginetype { get; set; }
        public virtual DbSet<face> face { get; set; }
        public virtual DbSet<gear> gear { get; set; }
        public virtual DbSet<generation> generation { get; set; }
        public virtual DbSet<m_carstatus> m_carstatus { get; set; }
        public virtual DbSet<m_menu> m_menu { get; set; }
        public virtual DbSet<m_menupermission> m_menupermission { get; set; }
        public virtual DbSet<m_receivecare_status> m_receivecare_status { get; set; }
        public virtual DbSet<nature> nature { get; set; }
        public virtual DbSet<option> option { get; set; }
        public virtual DbSet<permission> permission { get; set; }
        public virtual DbSet<repairshop> repairshop { get; set; }
        public virtual DbSet<seat> seat { get; set; }
        public virtual DbSet<subface> subface { get; set; }
        public virtual DbSet<type> type { get; set; }
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
            modelBuilder.Entity<branch>(entity =>
            {
                entity.Property(e => e.branchId).HasColumnType("int(11)");

                entity.Property(e => e.branchAddress)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.branchName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.tel)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<brand>(entity =>
            {
                entity.Property(e => e.brandId).HasColumnType("int(11)");

                entity.Property(e => e.brandName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<capacityengine>(entity =>
            {
                entity.Property(e => e.capacityEngineId).HasColumnType("int(11)");

                entity.Property(e => e.capacityEngineName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<car>(entity =>
            {
                entity.Property(e => e.carId).HasColumnType("int(11)");

                entity.Property(e => e.branchId).HasColumnType("int(11)");

                entity.Property(e => e.brandEngine)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.brandId).HasColumnType("int(11)");

                entity.Property(e => e.capacityEngineId).HasColumnType("int(11)");

                entity.Property(e => e.carProcessId).HasColumnType("int(11)");

                entity.Property(e => e.carStatusId).HasColumnType("int(11)");

                entity.Property(e => e.categoryId).HasColumnType("int(11)");

                entity.Property(e => e.code)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.colorId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.driveSystemId).HasColumnType("int(11)");

                entity.Property(e => e.engineNumber)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.engineTypeId).HasColumnType("int(11)");

                entity.Property(e => e.faceId).HasColumnType("int(11)");

                entity.Property(e => e.gasNumber).HasColumnType("varchar(50)");

                entity.Property(e => e.gearId).HasColumnType("int(11)");

                entity.Property(e => e.generationId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.mileNumber)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.natureId).HasColumnType("int(11)");

                entity.Property(e => e.receiveDate).HasColumnType("datetime");

                entity.Property(e => e.remark).HasColumnType("text");

                entity.Property(e => e.seatId).HasColumnType("int(11)");

                entity.Property(e => e.serialNumber)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.subfaceId).HasColumnType("int(11)");

                entity.Property(e => e.typeId).HasColumnType("int(11)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");

                entity.Property(e => e.vendorAddress).HasColumnType("varchar(500)");

                entity.Property(e => e.vendorId).HasColumnType("int(11)");

                entity.Property(e => e.vendorName).HasColumnType("varchar(250)");

                entity.Property(e => e.vendorNumber).HasColumnType("varchar(50)");

                entity.Property(e => e.vendorTel).HasColumnType("varchar(20)");

                entity.Property(e => e.weight)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.year).HasColumnType("int(11)");
            });

            modelBuilder.Entity<car_option>(entity =>
            {
                entity.HasKey(e => e.carOptionId)
                    .HasName("PRIMARY");

                entity.Property(e => e.carOptionId).HasColumnType("int(11)");

                entity.Property(e => e.carId).HasColumnType("int(11)");

                entity.Property(e => e.optionId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<category>(entity =>
            {
                entity.Property(e => e.categoryId).HasColumnType("int(11)");

                entity.Property(e => e.categoryName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<categoryshop>(entity =>
            {
                entity.Property(e => e.categoryShopId).HasColumnType("int(11)");

                entity.Property(e => e.categoryShopName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<color>(entity =>
            {
                entity.Property(e => e.colorId).HasColumnType("int(11)");

                entity.Property(e => e.colorName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<contactrepairshop>(entity =>
            {
                entity.Property(e => e.contactRepairShopId).HasColumnType("int(11)");

                entity.Property(e => e.contactName)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.remark).HasColumnType("varchar(500)");

                entity.Property(e => e.repairShopId).HasColumnType("int(11)");

                entity.Property(e => e.tel)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

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

            modelBuilder.Entity<drivesystem>(entity =>
            {
                entity.Property(e => e.driveSystemId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.driveSystemName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<enginetype>(entity =>
            {
                entity.Property(e => e.engineTypeId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.engineTypeName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<face>(entity =>
            {
                entity.Property(e => e.faceId).HasColumnType("int(11)");

                entity.Property(e => e.brandId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.faceName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.generationId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<gear>(entity =>
            {
                entity.Property(e => e.gearId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.gearName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<generation>(entity =>
            {
                entity.Property(e => e.generationId).HasColumnType("int(11)");

                entity.Property(e => e.brandId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.generationName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<m_carstatus>(entity =>
            {
                entity.HasKey(e => e.carStatusId)
                    .HasName("PRIMARY");

                entity.Property(e => e.carStatusId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.statusName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<m_menu>(entity =>
            {
                entity.HasKey(e => e.menuId)
                    .HasName("PRIMARY");

                entity.Property(e => e.menuId).HasColumnType("int(11)");

                entity.Property(e => e.icon)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.menuControllerName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.menuName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ord).HasColumnType("int(11)");

                entity.Property(e => e.parentId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<m_menupermission>(entity =>
            {
                entity.HasKey(e => e.menuPermissionId)
                    .HasName("PRIMARY");

                entity.Property(e => e.menuPermissionId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.menuId).HasColumnType("int(11)");

                entity.Property(e => e.ord).HasColumnType("int(11)");

                entity.Property(e => e.permission)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.permissionName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<m_receivecare_status>(entity =>
            {
                entity.HasKey(e => e.receiveCarStatusId)
                    .HasName("PRIMARY");

                entity.Property(e => e.receiveCarStatusId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.statusName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<nature>(entity =>
            {
                entity.Property(e => e.natureId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.natureName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.typeId).HasColumnType("int(11)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<option>(entity =>
            {
                entity.Property(e => e.optionId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.optionName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<permission>(entity =>
            {
                entity.Property(e => e.permissionId).HasColumnType("int(11)");

                entity.Property(e => e.departmentId).HasColumnType("int(11)");

                entity.Property(e => e.menuPermissionId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<repairshop>(entity =>
            {
                entity.Property(e => e.repairShopId).HasColumnType("int(11)");

                entity.Property(e => e.categoryShopId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.fax).HasColumnType("varchar(50)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.repairShopAddress)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.repairShopName)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.tel)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<seat>(entity =>
            {
                entity.Property(e => e.seatId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.seatName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<subface>(entity =>
            {
                entity.Property(e => e.subfaceId).HasColumnType("int(11)");

                entity.Property(e => e.brandId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.faceId).HasColumnType("int(11)");

                entity.Property(e => e.generationId).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.subfaceName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.updateDate).HasColumnType("datetime");

                entity.Property(e => e.updateUser).HasColumnType("int(11)");
            });

            modelBuilder.Entity<type>(entity =>
            {
                entity.Property(e => e.typeId).HasColumnType("int(11)");

                entity.Property(e => e.createDate).HasColumnType("datetime");

                entity.Property(e => e.createUser).HasColumnType("int(11)");

                entity.Property(e => e.isEnable).HasColumnType("bit(1)");

                entity.Property(e => e.typeName)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

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

                entity.Property(e => e.salt)
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
