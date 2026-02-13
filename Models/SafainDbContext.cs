using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineShoppingStore.Models;

public partial class SafainDbContext : DbContext
{
    public SafainDbContext()
    {
    }

    public SafainDbContext(DbContextOptions<SafainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

    public virtual DbSet<TableCartStatus> TableCartStatuses { get; set; }

    public virtual DbSet<TblCart> TblCarts { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblMember> TblMembers { get; set; }

    public virtual DbSet<TblMemberRole> TblMemberRoles { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSlideImage> TblSlideImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VTQVDQ0;Database=Safain;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShippingDetail>(entity =>
        {
            entity.HasKey(e => e.ShippingDetailId).HasName("PK__Shipping__FBB36851868911C9");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Member).WithMany(p => p.ShippingDetails)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingD__Membe__440B1D61");
        });

        modelBuilder.Entity<TableCartStatus>(entity =>
        {
            entity.HasKey(e => e.CartStatusId).HasName("PK__Table_Ca__031908A82520955C");

            entity.ToTable("Table_CartStatus");

            entity.Property(e => e.CartStatus)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Tbl_Cart__51BCD7B74045FA62");

            entity.ToTable("Tbl_Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.TblCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Tbl_Cart__Produc__48CFD27E");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Cate__19093A0B23E3BBFD");

            entity.ToTable("Tbl_Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Tbl_Memb__0CF04B18983019A9");

            entity.ToTable("Tbl_Members");

            entity.HasIndex(e => e.EmailId, "UQ__Tbl_Memb__7ED91ACE119FB044").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<TblMemberRole>(entity =>
        {
            entity.HasKey(e => e.MemberRoleId).HasName("PK__Tbl_Memb__673C22CB00F896BC");

            entity.ToTable("Tbl_MemberRole");

            entity.Property(e => e.MemberRoleId).HasColumnName("MemberRoleID");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_prod__B40CC6CD97F85696");

            entity.ToTable("Tbl_product");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductImage).IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Tbl_produ__Categ__3F466844");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Tbl_Role__8AFACE1A790A62E5");

            entity.ToTable("Tbl_Roles");

            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblSlideImage>(entity =>
        {
            entity.HasKey(e => e.SlideId).HasName("PK__Tbl_Slid__9E7CB650413B8557");

            entity.ToTable("Tbl_SlideImage");

            entity.Property(e => e.SlideImage).IsUnicode(false);
            entity.Property(e => e.SlideTitle)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
