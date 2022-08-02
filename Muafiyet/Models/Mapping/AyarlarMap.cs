using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class AyarlarMap : EntityTypeConfiguration<Ayarlar>
    {
        public AyarlarMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Ayarlar");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.NotTipiID).HasColumnName("NotTipiID");
            this.Property(t => t.AyarUniID).HasColumnName("AyarUniID");
            this.Property(t => t.AyarFakulteID).HasColumnName("AyarFakulteID");
            this.Property(t => t.AyarBolumID).HasColumnName("AyarBolumID");
            this.Property(t => t.AKTS1).HasColumnName("AKTS1");
            this.Property(t => t.AKTS2).HasColumnName("AKTS2");
            this.Property(t => t.AKTS3).HasColumnName("AKTS3");
            this.Property(t => t.AKTS14).HasColumnName("AKTS14");
            this.Property(t => t.Kredi1).HasColumnName("Kredi1");
            this.Property(t => t.Kredi2).HasColumnName("Kredi2");
            this.Property(t => t.Kredi3).HasColumnName("Kredi3");
            this.Property(t => t.Kredi4).HasColumnName("Kredi4");
            this.Property(t => t.AKTS_Kredi).HasColumnName("AKTS_Kredi");

            // Relationships
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.Ayarlars)
                .HasForeignKey(d => d.AyarUniID);

        }
    }
}
