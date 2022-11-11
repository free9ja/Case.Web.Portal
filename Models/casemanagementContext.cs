using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Case.Web.Portal.ViewModels;

namespace Case.Web.Portal.Models
{
    public partial class casemanagementContext : DbContext
    {
        public casemanagementContext()
        {
        }

        public casemanagementContext(DbContextOptions<casemanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Cmcase> Cmcases { get; set; } = null!;
        public virtual DbSet<CmcaseType> CmcaseTypes { get; set; } = null!;
        public virtual DbSet<Cmcustomer> Cmcustomers { get; set; } = null!;
        public virtual DbSet<CmcustomerCare> CmcustomerCares { get; set; } = null!;
        public virtual DbSet<Cmuser> Cmusers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=casemanagementdb.cmt8630tspsi.us-east-1.rds.amazonaws.com;Port=5432;Username=postgres;Password=password;Database=casemanagement;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Cmcase>(entity =>
            {
                entity.ToTable("CMCase");

                entity.HasIndex(e => e.CMcaseTypeId, "IX_CMCase_cMCaseTypeID");

                entity.HasIndex(e => e.CMcustomerId, "IX_CMCase_cMCustomerID");

                entity.HasIndex(e => e.CustomerCareId, "IX_CMCase_customerCareId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CMcaseTypeId).HasColumnName("cMCaseTypeID");

                entity.Property(e => e.CMcustomerId).HasColumnName("cMCustomerID");

                entity.Property(e => e.CaseTypeId).HasColumnName("caseTypeId");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.CustomerCareId).HasColumnName("customerCareId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ResolvedAt).HasColumnName("resolvedAt");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CMcaseType)
                    .WithMany(p => p.Cmcases)
                    .HasForeignKey(d => d.CMcaseTypeId);

                entity.HasOne(d => d.CMcustomer)
                    .WithMany(p => p.Cmcases)
                    .HasForeignKey(d => d.CMcustomerId);

                entity.HasOne(d => d.CustomerCare)
                    .WithMany(p => p.Cmcases)
                    .HasForeignKey(d => d.CustomerCareId);
            });

            modelBuilder.Entity<CmcaseType>(entity =>
            {
                entity.ToTable("CMCaseType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Cmcustomer>(entity =>
            {
                entity.ToTable("CMCustomer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            });

            modelBuilder.Entity<CmcustomerCare>(entity =>
            {
                entity.ToTable("CMCustomerCare");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.UpdatedAt).HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Cmuser>(entity =>
            {
                entity.ToTable("CMUser");

                entity.HasIndex(e => e.RoleId, "IX_CMUser_RoleId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Cmusers)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Case.Web.Portal.ViewModels.AdminCase> AdminCase { get; set; }
    }
}
