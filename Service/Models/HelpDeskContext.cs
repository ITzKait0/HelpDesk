using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class HelpDeskContext : DbContext
{
    public HelpDeskContext()
    {
    }

    public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Mail> Mail { get; set; }

    public virtual DbSet<Supporter> Supporters { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<VwLastTicketNr> VwLastTicketNrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=HelpDesk;User Id=sa;Password=Hd123456798!;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC27775332F2");

            entity.HasOne(d => d.Supporter).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Supporter");
        });

        modelBuilder.Entity<Mail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mail__3214EC074C13D354");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Mail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mail_Ticket");
        });

        modelBuilder.Entity<Supporter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supporte__3214EC2740EC88A8");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC278DCB0CA2");

            entity.HasOne(d => d.Supporter).WithMany(p => p.Tickets).HasConstraintName("FK_Ticket_Supporter");
        });

        modelBuilder.Entity<VwLastTicketNr>(entity =>
        {
            entity.ToView("vw_LastTicketNr");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
