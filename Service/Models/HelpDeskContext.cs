using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class HelpDeskContext : DbContext
{

    private readonly IConfiguration _config;
    public HelpDeskContext()
    {
    }

    public HelpDeskContext(DbContextOptions<HelpDeskContext> options, IConfiguration configuration)
        : base(options)
    {
        _config = configuration;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Kunden> Kundens { get; set; }

    public virtual DbSet<Supporter> Supporters { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_config["DbConnectionString"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC275AA34FF7");

            entity.HasOne(d => d.Kunden).WithMany(p => p.Accounts).HasConstraintName("FK_Account_Kunde");

            entity.HasOne(d => d.Supporter).WithMany(p => p.Accounts).HasConstraintName("FK_Account_Supporter");
        });

        modelBuilder.Entity<Kunden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kunde__3214EC277C49CB80");
        });

        modelBuilder.Entity<Supporter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supporte__3214EC2740EC88A8");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC27130E2003");

            entity.HasOne(d => d.Kunden).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Kunde");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
