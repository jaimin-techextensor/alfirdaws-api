using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace alfirdawsmanager.Data.Models
{
    public partial class AlfirdawsManagerDbContext : DbContext
    {
        public AlfirdawsManagerDbContext()
        {
        }

        public AlfirdawsManagerDbContext(DbContextOptions<AlfirdawsManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressType> AddressTypes { get; set; } = null!;
        public virtual DbSet<AssignedRole> AssignedRoles { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<CampaignType> CampaignTypes { get; set; } = null!;
        public virtual DbSet<Case> Cases { get; set; } = null!;
        public virtual DbSet<CaseCategory> CaseCategories { get; set; } = null!;
        public virtual DbSet<CaseLog> CaseLogs { get; set; } = null!;
        public virtual DbSet<CaseOrigin> CaseOrigins { get; set; } = null!;
        public virtual DbSet<CasePriority> CasePriorities { get; set; } = null!;
        public virtual DbSet<CaseSeverity> CaseSeverities { get; set; } = null!;
        public virtual DbSet<CaseStatus> CaseStatuses { get; set; } = null!;
        public virtual DbSet<CaseStatusReason> CaseStatusReasons { get; set; } = null!;
        public virtual DbSet<CaseType> CaseTypes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<PeriodType> PeriodTypes { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PricingModel> PricingModels { get; set; } = null!;
        public virtual DbSet<ReachType> ReachTypes { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<SubscriptionModel> SubscriptionModels { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vattype> Vattypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=69.167.148.96,782;Database=alfirdaws_test;User Id=alfirdaws_dev;Password=Tech2023!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("alfirdaws_dev");

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AssignedRole>(entity =>
            {
                entity.ToTable("AssignedRole", "alfirdaws_usr");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AssignedRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AssignedRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignedRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AssignedRole_User");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign", "alfirdaws_usr");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ImpactPosition)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ImpactViews)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NetPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PricePerDay).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Saving).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.CampaignType)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.CampaignTypeId)
                    .HasConstraintName("FK_Campaign_CampaignType");

                entity.HasOne(d => d.PeriodType)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.PeriodTypeId)
                    .HasConstraintName("FK_Campaign_PeriodType");

                entity.HasOne(d => d.ReachType)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.ReachTypeId)
                    .HasConstraintName("FK_Campaign_ReachType");
            });

            modelBuilder.Entity<CampaignType>(entity =>
            {
                entity.ToTable("CampaignType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.ToTable("Case", "alfirdaws_usr");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Duration)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Resolution)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ResolutionDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.CaseCategory)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseCategoryId)
                    .HasConstraintName("FK_Case_CaseCategory");

                entity.HasOne(d => d.CaseOrigin)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseOriginId)
                    .HasConstraintName("FK_Case_CaseOrigin");

                entity.HasOne(d => d.CasePriority)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CasePriorityId)
                    .HasConstraintName("FK_Case_CasePriority");

                entity.HasOne(d => d.CaseSeverity)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseSeverityId)
                    .HasConstraintName("FK_Case_CaseSeverity");

                entity.HasOne(d => d.CaseStatus)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseStatusId)
                    .HasConstraintName("FK_Case_CaseStatus");

                entity.HasOne(d => d.CaseStatusReason)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseStatusReasonId)
                    .HasConstraintName("FK_Case_CaseStatusReason");

                entity.HasOne(d => d.CaseType)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.CaseTypeId)
                    .HasConstraintName("FK_Case_CaseType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cases)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Case_User");
            });

            modelBuilder.Entity<CaseCategory>(entity =>
            {
                entity.ToTable("CaseCategory", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CaseLog>(entity =>
            {
                entity.ToTable("CaseLog", "alfirdaws_usr");

                entity.Property(e => e.CaseLogId).ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.CaseLogNavigation)
                    .WithOne(p => p.CaseLog)
                    .HasForeignKey<CaseLog>(d => d.CaseLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseLog_Case");
            });

            modelBuilder.Entity<CaseOrigin>(entity =>
            {
                entity.ToTable("CaseOrigin", "alfirdaws_usr");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CasePriority>(entity =>
            {
                entity.ToTable("CasePriority", "alfirdaws_usr");

                entity.Property(e => e.Color)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CaseSeverity>(entity =>
            {
                entity.ToTable("CaseSeverity", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CaseStatus>(entity =>
            {
                entity.ToTable("CaseStatus", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CaseStatusReason>(entity =>
            {
                entity.ToTable("CaseStatusReason", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CaseStatus)
                    .WithMany(p => p.CaseStatusReasons)
                    .HasForeignKey(d => d.CaseStatusId)
                    .HasConstraintName("FK_CaseStatusReason_CaseStatus");
            });

            modelBuilder.Entity<CaseType>(entity =>
            {
                entity.ToTable("CaseType", "alfirdaws_usr");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country", "alfirdaws_usr");

                entity.Property(e => e.Flag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceType>(entity =>
            {
                entity.ToTable("InvoiceType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language", "alfirdaws_usr");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TranslationFile).HasColumnType("text");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module", "alfirdaws_usr");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PeriodType>(entity =>
            {
                entity.ToTable("PeriodType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "alfirdaws_usr");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_Permission_Module");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Permission_Role");
            });

            modelBuilder.Entity<PricingModel>(entity =>
            {
                entity.ToTable("PricingModel", "alfirdaws_usr");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.NetPrice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PricePerDay).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Saving).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.PeriodType)
                    .WithMany(p => p.PricingModels)
                    .HasForeignKey(d => d.PeriodTypeId)
                    .HasConstraintName("FK_PricingModel_PeriodType");

                entity.HasOne(d => d.SubscriptionModel)
                    .WithMany(p => p.PricingModels)
                    .HasForeignKey(d => d.SubscriptionModelId)
                    .HasConstraintName("FK_PricingModel_SubscriptionModel");
            });

            modelBuilder.Entity<ReachType>(entity =>
            {
                entity.ToTable("ReachType", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Country");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "alfirdaws_usr");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<SubscriptionModel>(entity =>
            {
                entity.ToTable("SubscriptionModel", "alfirdaws_usr");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubscriptionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "alfirdaws_usr");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vattype>(entity =>
            {
                entity.ToTable("VATType", "alfirdaws_usr");

                entity.Property(e => e.VattypeId).HasColumnName("VATTypeId");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Percentage).HasColumnType("decimal(4, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
