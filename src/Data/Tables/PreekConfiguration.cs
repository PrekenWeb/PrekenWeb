using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class PreekConfiguration : EntityTypeConfiguration<Preek>
    {
        public PreekConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Preek");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BoekhoofdstukId).HasColumnName("BoekhoofdstukId").IsOptional();
            Property(x => x.BijbeltekstOmschrijving).HasColumnName("BijbeltekstOmschrijving").IsOptional();
            Property(x => x.SerieId).HasColumnName("SerieId").IsOptional();
            Property(x => x.GebeurtenisId).HasColumnName("GebeurtenisId").IsOptional();
            Property(x => x.DatumAangemaakt).HasColumnName("DatumAangemaakt").IsOptional();
            Property(x => x.DatumBijgewerkt).HasColumnName("DatumBijgewerkt").IsOptional();
            Property(x => x.DatumGepubliceerd).HasColumnName("DatumGepubliceerd").IsOptional();
            Property(x => x.Bestandsnaam).HasColumnName("Bestandsnaam").IsOptional();
            Property(x => x.AantalKeerGedownload).HasColumnName("AantalKeerGedownload").IsRequired();
            Property(x => x.OudId).HasColumnName("OudID").IsOptional();
            Property(x => x.PredikantId).HasColumnName("PredikantId").IsOptional();
            Property(x => x.Hoofdstuk).HasColumnName("Hoofdstuk").IsOptional();
            Property(x => x.VanVers).HasColumnName("VanVers").IsOptional().HasMaxLength(255);
            Property(x => x.TotVers).HasColumnName("TotVers").IsOptional().HasMaxLength(255);
            Property(x => x.Punt1).HasColumnName("Punt1").IsOptional().HasMaxLength(255);
            Property(x => x.Punt2).HasColumnName("Punt2").IsOptional().HasMaxLength(255);
            Property(x => x.Punt3).HasColumnName("Punt3").IsOptional().HasMaxLength(255);
            Property(x => x.Punt4).HasColumnName("Punt4").IsOptional().HasMaxLength(255);
            Property(x => x.Punt5).HasColumnName("Punt5").IsOptional().HasMaxLength(255);
            Property(x => x.GemeenteId).HasColumnName("GemeenteId").IsOptional();
            Property(x => x.DatumPreek).HasColumnName("DatumPreek").IsOptional();
            Property(x => x.Informatie).HasColumnName("Informatie").IsOptional().HasMaxLength(2147483647);
            Property(x => x.ThemaOmschrijving).HasColumnName("ThemaOmschrijving").IsOptional();
            Property(x => x.AfbeeldingId).HasColumnName("AfbeeldingId").IsOptional();
            Property(x => x.PreekTypeId).HasColumnName("PreekTypeId").IsRequired();
            Property(x => x.LezingCategorieId).HasColumnName("LezingCategorieId").IsOptional();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();
            Property(x => x.Gepubliceerd).HasColumnName("Gepubliceerd").IsRequired();
            Property(x => x.LezingOmschrijving).HasColumnName("LezingOmschrijving").IsOptional();
            Property(x => x.Duur).HasColumnName("Duur").IsOptional();
            Property(x => x.Bestandsgrootte).HasColumnName("Bestandsgrootte").IsOptional();
            Property(x => x.VersVanId).HasColumnName("VersVanId").IsOptional();
            Property(x => x.VersTotId).HasColumnName("VersTotId").IsOptional();
            Property(x => x.GedeelteVanVersId).HasColumnName("GedeelteVanVersId").IsOptional();
            Property(x => x.GedeelteTotVersId).HasColumnName("GedeelteTotVersId").IsOptional();
            Property(x => x.VersOmschrijving).HasColumnName("VersOmschrijving").IsOptional().HasMaxLength(50);
            Property(x => x.AutomatischeTeksten).HasColumnName("AutomatischeTeksten").IsRequired();
            Property(x => x.AangemaaktDoor).HasColumnName("AangemaaktDoor").IsOptional();
            Property(x => x.AangepastDoor).HasColumnName("AangepastDoor").IsOptional();
            Property(x => x.LeesPreekTekst).HasColumnName("LeesPreekTekst").IsOptional();
            Property(x => x.MeditatieTekst).HasColumnName("MeditatieTekst").IsOptional();

            
            HasOptional(a => a.BoekHoofdstuk).WithMany(b => b.Preeks).HasForeignKey(c => c.BoekhoofdstukId); // FK_Preek_To_Boekhoofdstuk
            HasOptional(a => a.Serie).WithMany(b => b.Preeks).HasForeignKey(c => c.SerieId); // FK_Preek_To_Serie
            HasOptional(a => a.Gebeurtenis).WithMany(b => b.Preeks).HasForeignKey(c => c.GebeurtenisId); // FK_Preek_To_Gebeurtenis
            HasOptional(a => a.Predikant).WithMany(b => b.Preeks).HasForeignKey(c => c.PredikantId); // FK_Preek_To_Predikant
            HasOptional(a => a.Gemeente).WithMany(b => b.Preeks).HasForeignKey(c => c.GemeenteId); // FK_Preek_To_Gemeente
            HasOptional(a => a.Afbeelding).WithMany(b => b.Preeks).HasForeignKey(c => c.AfbeeldingId); // FK_Preek_To_Afbeelding
            HasRequired(a => a.PreekType).WithMany(b => b.Preeks).HasForeignKey(c => c.PreekTypeId); // FK_Preek_To_PreekType
            HasOptional(a => a.LezingCategorie).WithMany(b => b.Preeks).HasForeignKey(c => c.LezingCategorieId); // FK_Preek_To_LezingCategorie
            HasRequired(a => a.Taal).WithMany(b => b.Preeks).HasForeignKey(c => c.TaalId); // FK_Preek_To_Taal
            HasOptional(a => a.BoekHoofdstukTekst_VersVanId).WithMany(b => b.Preeks_VersVanId).HasForeignKey(c => c.VersVanId); // FK_Preek_To_VanVers
            HasOptional(a => a.BoekHoofdstukTekst_VersTotId).WithMany(b => b.Preeks_VersTotId).HasForeignKey(c => c.VersTotId); // FK_Preek_To_TotVers
            HasOptional(a => a.BoekHoofdstukTekst_GedeelteVanVersId).WithMany(b => b.Preeks_GedeelteVanVersId).HasForeignKey(c => c.GedeelteVanVersId); // FK_Preek_To_GedeelteVanVers
            HasOptional(a => a.BoekHoofdstukTekst_GedeelteTotVersId).WithMany(b => b.Preeks_GedeelteTotVersId).HasForeignKey(c => c.GedeelteTotVersId); // FK_Preek_To_GedeelteTotVers
            HasOptional(a => a.Gebruiker_AangemaaktDoor).WithMany(b => b.Preeks_AangemaaktDoor).HasForeignKey(c => c.AangemaaktDoor); // FkAangemaaktDoor
            HasOptional(a => a.Gebruiker_AangepastDoor).WithMany(b => b.Preeks_AangepastDoor).HasForeignKey(c => c.AangepastDoor); // FkAangepastDoor
            
        }
        
    }

}
