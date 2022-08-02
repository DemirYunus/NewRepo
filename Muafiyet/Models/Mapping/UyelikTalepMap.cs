using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class UyelikTalepMap : EntityTypeConfiguration<UyelikTalep>
    {
        public UyelikTalepMap()
        {
            // Primary Key
            this.HasKey(t => t.UyelikTalepID);

            // Properties
            this.Property(t => t.TalepEdenAdi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TalepEdenSoyadi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TalepEdenMail)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UyelikTalep");
            this.Property(t => t.UyelikTalepID).HasColumnName("UyelikTalepID");
            this.Property(t => t.TalepEdenAdi).HasColumnName("TalepEdenAdi");
            this.Property(t => t.TalepEdenSoyadi).HasColumnName("TalepEdenSoyadi");
            this.Property(t => t.TalepEdenMail).HasColumnName("TalepEdenMail");
            this.Property(t => t.TalepEdenUniID).HasColumnName("TalepEdenUniID");
            this.Property(t => t.TalepEdenFakulteID).HasColumnName("TalepEdenFakulteID");
            this.Property(t => t.TalepEdenBolumID).HasColumnName("TalepEdenBolumID");
            this.Property(t => t.TalepTarihi).HasColumnName("TalepTarihi");
            this.Property(t => t.TalepCevap).HasColumnName("TalepCevap");

            // Relationships
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.UyelikTaleps)
                .HasForeignKey(d => d.TalepEdenUniID);

        }
    }
}
