using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Models;

public partial class ErptestingContext : DbContext
{
    public ErptestingContext()
    {
    }

    public ErptestingContext(DbContextOptions<ErptestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<BatchMov> BatchMovs { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Iproduct> Iproducts { get; set; }

    public virtual DbSet<Mproduct> Mproducts { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PurchaseLog> PurchaseLogs { get; set; }

    public virtual DbSet<SalesLog> SalesLogs { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Lazzy\\SQLEXPRESS;Database=ERPTesting;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
            entity.Property(e => e.BatchCode).HasColumnName("Batch_Code");
            entity.Property(e => e.ExpDate)
                .HasColumnType("datetime")
                .HasColumnName("exp_date");
            entity.Property(e => e.ManufacturingDate)
                .HasColumnType("datetime")
                .HasColumnName("manufacturing_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
        });

        modelBuilder.Entity<BatchMov>(entity =>
        {
            entity.HasKey(e => e.MovId);

            entity.ToTable("Batch_MOV");

            entity.Property(e => e.MovId).HasColumnName("mov_ID");
            entity.Property(e => e.BatchId).HasColumnName("Batch_Id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Location)
                .HasMaxLength(150)
                .HasColumnName("location");
            entity.Property(e => e.MovementType)
                .HasMaxLength(150)
                .HasColumnName("movement_type");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__MInvento__F5FDE6D388E31A80");

            entity.ToTable("Inventory", "inventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.LastUpdated)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasDefaultValue(0);
            entity.Property(e => e.Wid).HasColumnName("WID");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__MInventor__Produ__07C12930");
        });

        modelBuilder.Entity<Iproduct>(entity =>
        {
            entity.ToTable("IProducts", "inventory");

            entity.Property(e => e.IproductId).HasColumnName("IProductID");
            entity.Property(e => e.Sku).HasColumnName("SKU");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.HasOne(d => d.Product).WithMany(p => p.Iproducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__IProducts__Produ__4183B671");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Iproducts)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__IProducts__Suppl__45544755");
        });

        modelBuilder.Entity<Mproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__MProduct__B40CC6EDD986E7ED");

            entity.ToTable("MProduct");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.MaterialsRequired)
                .HasColumnType("text")
                .HasColumnName("Materials Required");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.ChkId);

            entity.ToTable("OrderStatus");

            entity.Property(e => e.ChkId).HasColumnName("chkID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<PurchaseLog>(entity =>
        {
            entity.HasKey(e => e.PurchaseId);

            entity.ToTable("PurchaseLog", "inventory");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
        });

        modelBuilder.Entity<SalesLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalesLog", "inventory");

            entity.Property(e => e.CreatedDt)
                .HasColumnType("datetime")
                .HasColumnName("created_dt");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasColumnName("payment_status");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.UpdatedDt)
                .HasColumnType("datetime")
                .HasColumnName("updated_dt");
            entity.Property(e => e.Wid).HasColumnName("WID");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__MSupplie__4BE66694CF643220");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderHistory)
                .HasColumnType("datetime")
                .HasColumnName("order_history");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Wid);

            entity.ToTable("Warehouse", "inventory");

            entity.Property(e => e.Wid).HasColumnName("WID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
