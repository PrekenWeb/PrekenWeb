using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace PrekenWeb.WebApi.Models
{
    public partial class PrekenWebContext : DbContext
    {
        public PrekenWebContext()
        {
        }

        public PrekenWebContext(DbContextOptions<PrekenWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Afbeelding> Afbeelding { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<Boek> Boek { get; set; }
        public virtual DbSet<BoekHoofdstuk> BoekHoofdstuk { get; set; }
        public virtual DbSet<BoekHoofdstukTekst> BoekHoofdstukTekst { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<Gebeurtenis> Gebeurtenis { get; set; }
        public virtual DbSet<Gebruiker> Gebruiker { get; set; }
        public virtual DbSet<GebruikerMailing> GebruikerMailing { get; set; }
        public virtual DbSet<Gemeente> Gemeente { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Inbox> Inbox { get; set; }
        public virtual DbSet<InboxOpvolging> InboxOpvolging { get; set; }
        public virtual DbSet<InboxType> InboxType { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<LezingCategorie> LezingCategorie { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Mailing> Mailing { get; set; }
        public virtual DbSet<NieuwsbriefInschrijving> NieuwsbriefInschrijving { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<Predikant> Predikant { get; set; }
        public virtual DbSet<Preek> Preek { get; set; }
        public virtual DbSet<PreekCookie> PreekCookie { get; set; }
        public virtual DbSet<PreekLezenEnZingen> PreekLezenEnZingen { get; set; }
        public virtual DbSet<PreekType> PreekType { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<Serie> Serie { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<Spotlight> Spotlight { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Taal> Taal { get; set; }
        public virtual DbSet<Tekst> Tekst { get; set; }
        public virtual DbSet<ZoekOpdracht> ZoekOpdracht { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afbeelding>(entity =>
            {
                entity.Property(e => e.Bestandsnaam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => new { e.Value, e.Key })
                    .HasName("UX_HangFire_CounterAggregated_Key")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.GebruikerId)
                    .HasName("IX_Gebruiker_Id");

                entity.Property(e => e.GebruikerId).HasColumnName("Gebruiker_Id");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.Gebruiker_Gebruiker_Id");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.GebruikerId)
                    .HasName("IX_Gebruiker_Id");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.GebruikerId).HasColumnName("Gebruiker_Id");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.Gebruiker_Gebruiker_Id");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasIndex(e => e.GebruikerId)
                    .HasName("IX_Gebruiker_Id");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RoleId");

                entity.Property(e => e.GebruikerId).HasColumnName("Gebruiker_Id");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.Gebruiker_Gebruiker_Id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");
            });

            modelBuilder.Entity<Boek>(entity =>
            {
                entity.Property(e => e.Afkorting).HasMaxLength(50);

                entity.Property(e => e.Boeknaam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TaalId).HasDefaultValueSql("((1))");

                entity.Property(e => e.ToonHoofdstukNummer)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Boek)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Boek_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<BoekHoofdstuk>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Boek)
                    .WithMany(p => p.BoekHoofdstuk)
                    .HasForeignKey(d => d.BoekId)
                    .HasConstraintName("FK_dbo.BoekHoofdstuk_dbo.Boek_BoekId");
            });

            modelBuilder.Entity<BoekHoofdstukTekst>(entity =>
            {
                entity.Property(e => e.Tekst).IsRequired();

                entity.HasOne(d => d.BoekHoofdstuk)
                    .WithMany(p => p.BoekHoofdstukTekst)
                    .HasForeignKey(d => d.BoekHoofdstukId)
                    .HasConstraintName("FK_dbo.BoekHoofdstukTekst_dbo.BoekHoofdstuk_BoekHoofdstukId");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => new { e.Value, e.Key })
                    .HasName("IX_HangFire_Counter_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Gebeurtenis>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TaalId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Gebeurtenis)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Gebeurtenis_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<Gebruiker>(entity =>
            {
                entity.Property(e => e.LaatstIngelogd).HasColumnType("datetime");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<GebruikerMailing>(entity =>
            {
                entity.HasKey(e => new { e.GebruikerId, e.MailingId })
                    .HasName("PK_dbo.GebruikerMailing");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.GebruikerMailing)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.GebruikerMailing_dbo.Gebruiker_GebruikerId");

                entity.HasOne(d => d.Mailing)
                    .WithMany(p => p.GebruikerMailing)
                    .HasForeignKey(d => d.MailingId)
                    .HasConstraintName("FK_dbo.GebruikerMailing_dbo.Mailing_MailingId");
            });

            modelBuilder.Entity<Gemeente>(entity =>
            {
                entity.Property(e => e.Omschrijving).HasMaxLength(255);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => new { e.ExpireAt, e.Key })
                    .HasName("IX_HangFire_Hash_Key");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Hash_ExpireAt");

                entity.HasIndex(e => new { e.Key, e.Field })
                    .HasName("UX_HangFire_Hash_Key_Field")
                    .IsUnique();

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Inbox>(entity =>
            {
                entity.Property(e => e.AanEmail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.AanNaam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Aangemaakt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Inhoud).IsRequired();

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VanEmail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VanNaam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.Inbox)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.Inbox_dbo.Gebruiker_GebruikerId");

                entity.HasOne(d => d.InboxType)
                    .WithMany(p => p.Inbox)
                    .HasForeignKey(d => d.InboxTypeId)
                    .HasConstraintName("FK_dbo.Inbox_dbo.InboxType_InboxTypeId");

                entity.HasOne(d => d.Preek)
                    .WithMany(p => p.Inbox)
                    .HasForeignKey(d => d.PreekId)
                    .HasConstraintName("FK_dbo.Inbox_dbo.Preek_PreekId");
            });

            modelBuilder.Entity<InboxOpvolging>(entity =>
            {
                entity.Property(e => e.Aangemaakt).HasColumnType("datetime");

                entity.Property(e => e.Verstuurd).HasColumnType("datetime");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.InboxOpvolging)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.InboxOpvolging_dbo.Gebruiker_GebruikerId");

                entity.HasOne(d => d.Inbox)
                    .WithMany(p => p.InboxOpvolging)
                    .HasForeignKey(d => d.InboxId)
                    .HasConstraintName("FK_dbo.InboxOpvolging_dbo.Inbox_InboxId");
            });

            modelBuilder.Entity<InboxType>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.ToTable("JobParameter", "HangFire");

                entity.HasIndex(e => new { e.JobId, e.Name })
                    .HasName("IX_HangFire_JobParameter_JobIdAndName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.ToTable("JobQueue", "HangFire");

                entity.HasIndex(e => new { e.Queue, e.FetchedAt })
                    .HasName("IX_HangFire_JobQueue_QueueAndFetchedAt");

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LezingCategorie>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_List_ExpireAt");

                entity.HasIndex(e => new { e.ExpireAt, e.Value, e.Key })
                    .HasName("IX_HangFire_List_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<Mailing>(entity =>
            {
                entity.Property(e => e.MailChimpId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Mailing)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Mailing_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<NieuwsbriefInschrijving>(entity =>
            {
                entity.Property(e => e.Aangemeld).HasColumnType("datetime");

                entity.Property(e => e.Afgemeld).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.NieuwsbriefInschrijving)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.NieuwsbriefInschrijving_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.Property(e => e.Aangemaakt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AangemaaktDoor).HasDefaultValueSql("((1))");

                entity.Property(e => e.Bijgewerkt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BijgewerktDoor).HasDefaultValueSql("((1))");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.AangemaaktDoorNavigation)
                    .WithMany(p => p.PaginaAangemaaktDoorNavigation)
                    .HasForeignKey(d => d.AangemaaktDoor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Pagina_dbo.Gebruiker_AangemaaktDoor");

                entity.HasOne(d => d.BijgewerktDoorNavigation)
                    .WithMany(p => p.PaginaBijgewerktDoorNavigation)
                    .HasForeignKey(d => d.BijgewerktDoor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Pagina_dbo.Gebruiker_BijgewerktDoor");
            });

            modelBuilder.Entity<Predikant>(entity =>
            {
                entity.Property(e => e.Achternaam)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Gemeente).HasMaxLength(255);

                entity.Property(e => e.LevensPeriode).HasMaxLength(255);

                entity.Property(e => e.OudId).HasColumnName("OudID");

                entity.Property(e => e.Titels).HasMaxLength(50);

                entity.Property(e => e.Tussenvoegsels).HasMaxLength(50);

                entity.Property(e => e.Voorletters).HasMaxLength(50);

                entity.HasOne(d => d.GemeenteNavigation)
                    .WithMany(p => p.Predikant)
                    .HasForeignKey(d => d.GemeenteId)
                    .HasConstraintName("FK_dbo.Predikant_dbo.Gemeente_GemeenteId");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Predikant)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Predikant_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<Preek>(entity =>
            {
                entity.Property(e => e.DatumAangemaakt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DatumBijgewerkt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DatumGepubliceerd).HasColumnType("datetime");

                entity.Property(e => e.DatumPreek).HasColumnType("date");

                entity.Property(e => e.MeditatieTekst).IsUnicode(false);

                entity.Property(e => e.OudId).HasColumnName("OudID");

                entity.Property(e => e.Punt1).HasMaxLength(255);

                entity.Property(e => e.Punt2).HasMaxLength(255);

                entity.Property(e => e.Punt3).HasMaxLength(255);

                entity.Property(e => e.Punt4).HasMaxLength(255);

                entity.Property(e => e.Punt5).HasMaxLength(255);

                entity.Property(e => e.SourceFileName).IsUnicode(false);

                entity.Property(e => e.TotVers).HasMaxLength(255);

                entity.Property(e => e.VanVers).HasMaxLength(255);

                entity.Property(e => e.VersOmschrijving).HasMaxLength(50);

                entity.Property(e => e.Video).IsUnicode(false);

                entity.HasOne(d => d.AangemaaktDoorNavigation)
                    .WithMany(p => p.PreekAangemaaktDoorNavigation)
                    .HasForeignKey(d => d.AangemaaktDoor)
                    .HasConstraintName("FK_dbo.Preek_dbo.Gebruiker_AangemaaktDoor");

                entity.HasOne(d => d.AangepastDoorNavigation)
                    .WithMany(p => p.PreekAangepastDoorNavigation)
                    .HasForeignKey(d => d.AangepastDoor)
                    .HasConstraintName("FK_dbo.Preek_dbo.Gebruiker_AangepastDoor");

                entity.HasOne(d => d.Afbeelding)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.AfbeeldingId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Afbeelding_AfbeeldingId");

                entity.HasOne(d => d.Boekhoofdstuk)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.BoekhoofdstukId)
                    .HasConstraintName("FK_dbo.Preek_dbo.BoekHoofdstuk_BoekhoofdstukId");

                entity.HasOne(d => d.Gebeurtenis)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.GebeurtenisId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Gebeurtenis_GebeurtenisId");

                entity.HasOne(d => d.GedeelteTotVers)
                    .WithMany(p => p.PreekGedeelteTotVers)
                    .HasForeignKey(d => d.GedeelteTotVersId)
                    .HasConstraintName("FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteTotVersId");

                entity.HasOne(d => d.GedeelteVanVers)
                    .WithMany(p => p.PreekGedeelteVanVers)
                    .HasForeignKey(d => d.GedeelteVanVersId)
                    .HasConstraintName("FK_dbo.Preek_dbo.BoekHoofdstukTekst_GedeelteVanVersId");

                entity.HasOne(d => d.Gemeente)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.GemeenteId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Gemeente_GemeenteId");

                entity.HasOne(d => d.LezingCategorie)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.LezingCategorieId)
                    .HasConstraintName("FK_dbo.Preek_dbo.LezingCategorie_LezingCategorieId");

                entity.HasOne(d => d.Predikant)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.PredikantId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Predikant_PredikantId");

                entity.HasOne(d => d.PreekType)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.PreekTypeId)
                    .HasConstraintName("FK_dbo.Preek_dbo.PreekType_PreekTypeId");

                entity.HasOne(d => d.Serie)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.SerieId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Serie_SerieId");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Preek)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Preek_dbo.Taal_TaalId");

                entity.HasOne(d => d.VersTot)
                    .WithMany(p => p.PreekVersTot)
                    .HasForeignKey(d => d.VersTotId)
                    .HasConstraintName("FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersTotId");

                entity.HasOne(d => d.VersVan)
                    .WithMany(p => p.PreekVersVan)
                    .HasForeignKey(d => d.VersVanId)
                    .HasConstraintName("FK_dbo.Preek_dbo.BoekHoofdstukTekst_VersVanId");
            });

            modelBuilder.Entity<PreekCookie>(entity =>
            {
                entity.Property(e => e.BladwijzerGelegdOp).HasColumnType("datetime");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.PreekCookie)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.PreekCookie_dbo.Gebruiker_GebruikerId");

                entity.HasOne(d => d.Preek)
                    .WithMany(p => p.PreekCookie)
                    .HasForeignKey(d => d.PreekId)
                    .HasConstraintName("FK_dbo.PreekCookie_dbo.Preek_PreekId");
            });

            modelBuilder.Entity<PreekLezenEnZingen>(entity =>
            {
                entity.Property(e => e.Omschrijving).HasMaxLength(255);

                entity.Property(e => e.Soort).HasMaxLength(255);

                entity.HasOne(d => d.Preek)
                    .WithMany(p => p.PreekLezenEnZingen)
                    .HasForeignKey(d => d.PreekId)
                    .HasConstraintName("FK_dbo.PreekLezenEnZingen_dbo.Preek_PreekId");
            });

            modelBuilder.Entity<PreekType>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<SchemaVersions>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Serie>(entity =>
            {
                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TaalId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Serie)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Serie_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => new { e.Id, e.ExpireAt })
                    .HasName("IX_HangFire_Set_ExpireAt");

                entity.HasIndex(e => new { e.Key, e.Value })
                    .HasName("UX_HangFire_Set_KeyAndValue")
                    .IsUnique();

                entity.HasIndex(e => new { e.ExpireAt, e.Value, e.Key })
                    .HasName("IX_HangFire_Set_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Spotlight>(entity =>
            {
                entity.Property(e => e.LinkTitel)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Subtitel)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TaalId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Afbeelding)
                    .WithMany(p => p.Spotlight)
                    .HasForeignKey(d => d.AfbeeldingId)
                    .HasConstraintName("FK_dbo.Spotlight_dbo.Afbeelding_AfbeeldingId");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Spotlight)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Spotlight_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "HangFire");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_HangFire_State_JobId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Taal>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Omschrijving)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Tekst>(entity =>
            {
                entity.Property(e => e.Kop)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Tekst1)
                    .IsRequired()
                    .HasColumnName("Tekst")
                    .HasDefaultValueSql("('1')");

                entity.HasOne(d => d.Pagina)
                    .WithMany(p => p.Tekst)
                    .HasForeignKey(d => d.PaginaId)
                    .HasConstraintName("FK_dbo.Tekst_dbo.Pagina_PaginaId");

                entity.HasOne(d => d.Taal)
                    .WithMany(p => p.Tekst)
                    .HasForeignKey(d => d.TaalId)
                    .HasConstraintName("FK_dbo.Tekst_dbo.Taal_TaalId");
            });

            modelBuilder.Entity<ZoekOpdracht>(entity =>
            {
                entity.Property(e => e.PubliekeSleutel).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Gebruiker)
                    .WithMany(p => p.ZoekOpdracht)
                    .HasForeignKey(d => d.GebruikerId)
                    .HasConstraintName("FK_dbo.ZoekOpdracht_dbo.Gebruiker_GebruikerId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
