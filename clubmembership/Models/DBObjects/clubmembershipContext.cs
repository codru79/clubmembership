using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace clubmembership.Models.DBObjects
{
    public partial class clubmembershipContext : DbContext
    {
        public clubmembershipContext()
        {
        }

        public clubmembershipContext(DbContextOptions<clubmembershipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcemment> Announcemments { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<CodeSnippet> CodeSnippets { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipType> MembershipTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=npetro36365809\\SQLEXPRESS;Database=clubmembership;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcemment>(entity =>
            {
                entity.HasKey(e => e.Idannouncemment)
                    .HasName("PK__Announce__D1C765E788C752CD");

                entity.Property(e => e.Idannouncemment)
                    .ValueGeneratedNever()
                    .HasColumnName("IDAnnouncemment");

                entity.Property(e => e.EventDateTime).HasColumnType("datetime");

                entity.Property(e => e.Tags)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

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
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

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

            modelBuilder.Entity<CodeSnippet>(entity =>
            {
                entity.HasKey(e => e.IdcodeSnippet)
                    .HasName("PK__CodeSnip__39AC8821AEE3F271");

                entity.Property(e => e.IdcodeSnippet)
                    .ValueGeneratedNever()
                    .HasColumnName("IDCodeSnippet");

                entity.Property(e => e.ContentCode).IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.Idmember).HasColumnName("IDmember");

                entity.Property(e => e.IdsnippetPreviousVersions).HasColumnName("IDSnippetPreviousVersions");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.CodeSnippets)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeSnippets_Members");

                entity.HasOne(d => d.IdsnippetPreviousVersionsNavigation)
                    .WithMany(p => p.InverseIdsnippetPreviousVersionsNavigation)
                    .HasForeignKey(d => d.IdsnippetPreviousVersions)
                    .HasConstraintName("FK_CodeSnippets_CodeSnippets");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.IdMember)
                    .HasName("PK__Members__570E7FF0386A03D4");

                entity.Property(e => e.IdMember).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Resume).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.Idmembership)
                    .HasName("PK__Membersh__4AB9863221DF931C");

                entity.Property(e => e.Idmembership)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMembership");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Idmember).HasColumnName("IDMember");

                entity.Property(e => e.IdmembershipType).HasColumnName("IDMembershipType");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdmemberNavigation)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.Idmember)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Memberships_Members");

                entity.HasOne(d => d.IdmembershipTypeNavigation)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.IdmembershipType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Memberships_MembershipTypes");
            });

            modelBuilder.Entity<MembershipType>(entity =>
            {
                entity.HasKey(e => e.IdmembershipType)
                    .HasName("PK__Membersh__20589C44D55960DF");

                entity.Property(e => e.IdmembershipType)
                    .ValueGeneratedNever()
                    .HasColumnName("IDMembershipType");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
