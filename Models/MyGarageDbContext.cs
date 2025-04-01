using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyGarageApi.Models;

public partial class MyGarageDbContext : DbContext
{
    public MyGarageDbContext()
    {
    }

    public MyGarageDbContext(DbContextOptions<MyGarageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Cotxe> Cotxes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MaterialReparacio> MaterialReparacios { get; set; }

    public virtual DbSet<Notificacio> Notificacios { get; set; }

    public virtual DbSet<Pressupost> Pressuposts { get; set; }

    public virtual DbSet<Producte> Productes { get; set; }

    public virtual DbSet<Reparacio> Reparacios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=MyGarageDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PK__Client__C035B8DCE62F1F97");

            entity.ToTable("Client");

            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Cognoms)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contrasenya)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DataNaixement).HasColumnName("Data_naixement");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cotxe>(entity =>
        {
            entity.HasKey(e => e.Matricula).HasName("PK__Cotxe__0FB9FB4E34440D2F");

            entity.ToTable("Cotxe");

            entity.HasIndex(e => e.Dni, "idx_Cotxe_DNI");

            entity.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Bastidor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Carburant)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cilindrada)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DataFabricacio).HasColumnName("Data_fabricacio");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.DniNavigation).WithMany(p => p.Cotxes)
                .HasForeignKey(d => d.Dni)
                .HasConstraintName("FK__Cotxe__DNI__4BAC3F29");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__6C08ED53618FC6FD");

            entity.ToTable("Factura");

            entity.HasIndex(e => e.IdReparacio, "UQ__Factura__BE5E165A32A2089E").IsUnique();

            entity.HasIndex(e => e.IdReparacio, "idx_Factura_idReparacio");

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.IdReparacio).HasColumnName("id_reparacio");
            entity.Property(e => e.PreuTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Preu_total");

            entity.HasOne(d => d.IdReparacioNavigation).WithOne(p => p.Factura)
                .HasForeignKey<Factura>(d => d.IdReparacio)
                .HasConstraintName("FK__Factura__id_repa__5629CD9C");
        });

        modelBuilder.Entity<MaterialReparacio>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("PK__Material__81E99B83E3728FA9");

            entity.ToTable("Material_reparacio");

            entity.HasIndex(e => e.IdReparacio, "idx_Material_idReparacio");

            entity.HasIndex(e => e.RefPeca, "idx_Material_refPeca");

            entity.Property(e => e.IdMaterial).HasColumnName("id_material");
            entity.Property(e => e.IdReparacio).HasColumnName("id_reparacio");
            entity.Property(e => e.RefPeca).HasColumnName("ref_peca");

            entity.HasOne(d => d.IdReparacioNavigation).WithMany(p => p.MaterialReparacios)
                .HasForeignKey(d => d.IdReparacio)
                .HasConstraintName("FK__Material___id_re__5AEE82B9");

            entity.HasOne(d => d.RefPecaNavigation).WithMany(p => p.MaterialReparacios)
                .HasForeignKey(d => d.RefPeca)
                .HasConstraintName("FK__Material___ref_p__5BE2A6F2");
        });

        modelBuilder.Entity<Notificacio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC070288FC34");

            entity.ToTable("Notificacio");

            entity.Property(e => e.DataEnvio).HasColumnType("datetime");
            entity.Property(e => e.DniClient)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DNI_Client");
            entity.Property(e => e.Missatge).IsUnicode(false);
            entity.Property(e => e.Titol)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DniClientNavigation).WithMany(p => p.Notificacios)
                .HasForeignKey(d => d.DniClient)
                .HasConstraintName("FK__Notificac__DataE__6FE99F9F");
        });

        modelBuilder.Entity<Pressupost>(entity =>
        {
            entity.HasKey(e => e.IdPressupost).HasName("PK__Pressupo__0555758CEA0363FD");

            entity.ToTable("Pressupost");

            entity.Property(e => e.IdPressupost).HasColumnName("id_pressupost");
            entity.Property(e => e.CostTreballador)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Cost_treballador");
            entity.Property(e => e.DataPressupost).HasColumnName("Data_pressupost");
            entity.Property(e => e.Estat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pendent");
            entity.Property(e => e.MotiuReparacio)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Motiu_reparacio");
            entity.Property(e => e.PreuTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Preu_total");
        });

        modelBuilder.Entity<Producte>(entity =>
        {
            entity.HasKey(e => e.RefPeca).HasName("PK__Producte__F8B07C4BCCBB21A2");

            entity.Property(e => e.RefPeca).HasColumnName("ref_peca");
            entity.Property(e => e.Descripcio)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MarcaCotxe)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Marca_cotxe");
            entity.Property(e => e.PartVehicle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Part_vehicle");
            entity.Property(e => e.PreuCompra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Preu_compra");
            entity.Property(e => e.PreuVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Preu_venta");
        });

        modelBuilder.Entity<Reparacio>(entity =>
        {
            entity.HasKey(e => e.IdReparacio).HasName("PK__Reparaci__BE5E165BB1D45215");

            entity.ToTable("Reparacio", tb =>
                {
                    tb.HasTrigger("trg_DescomptarStock");
                    tb.HasTrigger("trg_GenerarFacturaFinalitzada");
                });

            entity.HasIndex(e => e.IdPressupost, "UQ__Reparaci__0555758D3138EE45").IsUnique();

            entity.HasIndex(e => e.Matricula, "idx_Reparacio_Matricula");

            entity.Property(e => e.IdReparacio).HasColumnName("id_reparacio");
            entity.Property(e => e.CostTreballador)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Cost_treballador");
            entity.Property(e => e.DataEntrada).HasColumnName("Data_entrada");
            entity.Property(e => e.DataSortida).HasColumnName("Data_sortida");
            entity.Property(e => e.Estat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoresTreballades)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Hores_treballades");
            entity.Property(e => e.IdPressupost).HasColumnName("id_pressupost");
            entity.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ObservacionsClient)
                .HasColumnType("text")
                .HasColumnName("Observacions_client");
            entity.Property(e => e.ObservacionsMecanic)
                .HasColumnType("text")
                .HasColumnName("Observacions_mecanic");

            entity.HasOne(d => d.IdPressupostNavigation).WithOne(p => p.Reparacio)
                .HasForeignKey<Reparacio>(d => d.IdPressupost)
                .HasConstraintName("FK__Reparacio__id_pr__5165187F");

            entity.HasOne(d => d.MatriculaNavigation).WithMany(p => p.Reparacios)
                .HasForeignKey(d => d.Matricula)
                .HasConstraintName("FK__Reparacio__Matri__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
