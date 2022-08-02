using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class DersIcerikleriMap : EntityTypeConfiguration<DersIcerikleri>
    {
        public DersIcerikleriMap()
        {
            // Primary Key
            this.HasKey(t => t.AlinanDersID);

            // Properties
            this.Property(t => t.AlinanDersKodu)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AlinanDersAdi)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.IcerikLinki)
                .HasMaxLength(500);

            this.Property(t => t.IcerikNotu)
                .HasMaxLength(500);

            this.Property(t => t.IcerikResimYolu)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DersIcerikleri");
            this.Property(t => t.AlinanDersID).HasColumnName("AlinanDersID");
            this.Property(t => t.AlinanUniID).HasColumnName("AlinanUniID");
            this.Property(t => t.AlinanFakulteID).HasColumnName("AlinanFakulteID");
            this.Property(t => t.AlinanBolumID).HasColumnName("AlinanBolumID");
            this.Property(t => t.AlinanDersKodu).HasColumnName("AlinanDersKodu");
            this.Property(t => t.AlinanDersAdi).HasColumnName("AlinanDersAdi");
            this.Property(t => t.AlinanKredisi).HasColumnName("AlinanKredisi");
            this.Property(t => t.AlinanAKTS).HasColumnName("AlinanAKTS");
            this.Property(t => t.IcerikLinki).HasColumnName("IcerikLinki");
            this.Property(t => t.IcerikNotu).HasColumnName("IcerikNotu");
            this.Property(t => t.IcerikResimYolu).HasColumnName("IcerikResimYolu");

            // Relationships
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.DersIcerikleris)
                .HasForeignKey(d => d.AlinanUniID);

        }
    }
}
