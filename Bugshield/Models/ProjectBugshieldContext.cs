using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bugshield.Models;

public partial class ProjectBugshieldContext : DbContext
{
    public ProjectBugshieldContext()
    {
    }

    public ProjectBugshieldContext(DbContextOptions<ProjectBugshieldContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllotedQuery> AllotedQueries { get; set; }

    public virtual DbSet<ComputerInfo> ComputerInfos { get; set; }

    public virtual DbSet<ComputerInfoBackup> ComputerInfoBackups { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Query> Queries { get; set; }

    public virtual DbSet<ResignedUser> ResignedUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllotedQuery>(entity =>
        {
            entity.HasKey(e => e.Alotid).HasName("PK__AllotedQ__DBFAA3B96BC7AE2C");

            entity.Property(e => e.Alotid).HasColumnName("alotid");
            entity.Property(e => e.AllotedQueries).IsUnicode(false);
            entity.Property(e => e.Progress).IsUnicode(false);
            entity.Property(e => e.RaisedTime).HasColumnType("datetime");
            entity.Property(e => e.RaisedUser).HasColumnName("raisedUser");
            entity.Property(e => e.Remarks).IsUnicode(false);
            entity.Property(e => e.Sauser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SAUser");
            entity.Property(e => e.SolvedTime).HasColumnType("datetime");
            entity.Property(e => e.Status).IsUnicode(false);
        });

        modelBuilder.Entity<ComputerInfo>(entity =>
        {
            entity.HasKey(e => e.ComputerId).HasName("PK__Computer__A6BE3C54CBA43B4E");

            entity.ToTable("ComputerInfo");

            entity.Property(e => e.ComputerId)
                .ValueGeneratedNever()
                .HasColumnName("ComputerID");
            entity.Property(e => e.ComputerName).HasMaxLength(255);
            entity.Property(e => e.InstalledRamgb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InstalledRAMGB");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(15)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Macaddress)
                .HasMaxLength(17)
                .HasColumnName("MACAddress");
            entity.Property(e => e.Manufacturer).HasMaxLength(255);
            entity.Property(e => e.Model).HasMaxLength(255);
            entity.Property(e => e.OperatingSystem).HasMaxLength(255);
            entity.Property(e => e.Processor).HasMaxLength(255);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ComputerInfoBackup>(entity =>
        {
            entity.HasKey(e => e.ComputerId).HasName("PK_ComputerInfo_Backup_ComputerID");

            entity.ToTable("ComputerInfo_Backup");

            entity.Property(e => e.ComputerId)
                .ValueGeneratedNever()
                .HasColumnName("ComputerID");
            entity.Property(e => e.ComputerName).HasMaxLength(255);
            entity.Property(e => e.InstalledRamgb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("InstalledRAMGB");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(15)
                .HasColumnName("IPAddress");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Macaddress)
                .HasMaxLength(17)
                .HasColumnName("MACAddress");
            entity.Property(e => e.Manufacturer).HasMaxLength(255);
            entity.Property(e => e.Model).HasMaxLength(255);
            entity.Property(e => e.OperatingSystem).HasMaxLength(255);
            entity.Property(e => e.Processor).HasMaxLength(255);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK__ErrorLog__D65247C236D6C083");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.Query).IsUnicode(false);
            entity.Property(e => e.Solvedby)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("solvedby");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ErrorLog_User");
        });

        modelBuilder.Entity<Query>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__Query__5967F7DB2A1D9B2F");

            entity.ToTable("Query");

            entity.Property(e => e.IsSolved).HasDefaultValueSql("((0))");
            entity.Property(e => e.RaisedTime).HasColumnType("datetime");
            entity.Property(e => e.SolvedTime).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Queries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Query_User");
        });

        modelBuilder.Entity<ResignedUser>(entity =>
        {
            entity.HasKey(e => e.Resno).HasName("PK__Resigned__297E53C9A8C6F68F");

            entity.ToTable("ResignedUser");

            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(17)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__Roles__8AF5CA322141F241");

            entity.Property(e => e.Roleid).ValueGeneratedNever();
            entity.Property(e => e.Rolename)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__Users__1797D0248EA76999");

            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Computer).WithMany(p => p.Users)
                .HasForeignKey(d => d.Computerid)
                .HasConstraintName("FK__Users__Computeri__3B75D760");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK__Users__Roleid__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
