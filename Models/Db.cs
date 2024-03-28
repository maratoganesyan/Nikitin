using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Nikitin.Models
{
    public partial class Db : DbContext
    {
        public Db()
        {
        }

        public Db(DbContextOptions<Db> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarModel> CarModels { get; set; } = null!;
        public virtual DbSet<CarStatus> CarStatuses { get; set; } = null!;
        public virtual DbSet<DriveType> DriveTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<FuelType> FuelTypes { get; set; } = null!;
        public virtual DbSet<PurityStatus> PurityStatuses { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Transmission> Transmissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Nikitin;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.IdBrand)
                    .HasName("Brands_pkey");

                entity.Property(e => e.IdBrand).UseIdentityAlwaysColumn();

                entity.Property(e => e.BrandName).HasMaxLength(30);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.IdCar)
                    .HasName("Cars_pkey");

                entity.Property(e => e.IdCar).UseIdentityAlwaysColumn();

                entity.Property(e => e.StateNumber).HasMaxLength(10);

                entity.HasOne(d => d.IdCarModelNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdCarModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarModel_Cars");

                entity.HasOne(d => d.IdCarStatusNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdCarStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarStatus_Cars");

                entity.HasOne(d => d.IdDriveTypeNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdDriveType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriveType_Cars");

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdDriver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Driver_Cars");

                entity.HasOne(d => d.IdFuelTypeNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdFuelType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FuelType_Cars");

                entity.HasOne(d => d.IdTransmissionNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.IdTransmission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transmission_Cars");
            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.HasKey(e => e.IdCarModel)
                    .HasName("CarModels_pkey");

                entity.Property(e => e.IdCarModel).UseIdentityAlwaysColumn();

                entity.Property(e => e.ModelName).HasMaxLength(50);

                entity.HasOne(d => d.IdBrandNavigation)
                    .WithMany(p => p.CarModels)
                    .HasForeignKey(d => d.IdBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brands_Models");
            });

            modelBuilder.Entity<CarStatus>(entity =>
            {
                entity.HasKey(e => e.IdCarStatus)
                    .HasName("CarStatuses_pkey");

                entity.Property(e => e.IdCarStatus).UseIdentityAlwaysColumn();

                entity.Property(e => e.CarStatusName).HasMaxLength(30);
            });

            modelBuilder.Entity<DriveType>(entity =>
            {
                entity.HasKey(e => e.IdDriveType)
                    .HasName("DriveType_pkey");

                entity.ToTable("DriveType");

                entity.Property(e => e.IdDriveType).UseIdentityAlwaysColumn();

                entity.Property(e => e.DriveTypeName).HasMaxLength(30);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("Employees_pkey");

                entity.Property(e => e.IdEmployee).UseIdentityAlwaysColumn();

                entity.Property(e => e.Login).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Users");
            });

            modelBuilder.Entity<FuelType>(entity =>
            {
                entity.HasKey(e => e.IdFuelType)
                    .HasName("FuelTypes_pkey");

                entity.Property(e => e.IdFuelType).UseIdentityAlwaysColumn();

                entity.Property(e => e.FuelTypeName).HasMaxLength(15);
            });

            modelBuilder.Entity<PurityStatus>(entity =>
            {
                entity.HasKey(e => e.IdPurityStatus)
                    .HasName("PurityStatus_pkey");

                entity.ToTable("PurityStatus");

                entity.Property(e => e.IdPurityStatus).UseIdentityAlwaysColumn();

                entity.Property(e => e.PurityStatusName).HasMaxLength(30);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("Request_pkey");

                entity.ToTable("Request");

                entity.Property(e => e.IdRequest).UseIdentityAlwaysColumn();

                entity.Property(e => e.FirstPointLat).HasPrecision(9, 6);

                entity.Property(e => e.FirstPointLng).HasPrecision(9, 6);

                entity.Property(e => e.RequestDate).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.SecondPintLat).HasPrecision(9, 6);

                entity.Property(e => e.SecondPointLng).HasPrecision(9, 6);

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdCar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Requests");

                entity.HasOne(d => d.IdPurityStatusNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdPurityStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurityStatus_Requests");

                entity.HasOne(d => d.IdRequestStatusNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdRequestStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestStatus_Requests");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.HasKey(e => e.IdRequestStatus)
                    .HasName("RequestStatuses_pkey");

                entity.Property(e => e.IdRequestStatus).UseIdentityAlwaysColumn();

                entity.Property(e => e.RequestStatusName).HasMaxLength(30);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("Roles_pkey");

                entity.Property(e => e.IdRole).UseIdentityAlwaysColumn();

                entity.Property(e => e.RoleName).HasMaxLength(30);
            });

            modelBuilder.Entity<Transmission>(entity =>
            {
                entity.HasKey(e => e.IdTransmission)
                    .HasName("Transmissions_pkey");

                entity.Property(e => e.IdTransmission).UseIdentityAlwaysColumn();

                entity.Property(e => e.TransmissionName).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
