using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class MuafiyetBasvurulariMap : EntityTypeConfiguration<MuafiyetBasvurulari>
    {
        public MuafiyetBasvurulariMap()
        {
            // Primary Key
            this.HasKey(t => t.MuafBasvuruID);

            // Properties
            this.Property(t => t.Durum)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MuafiyetBasvurulari");
            this.Property(t => t.MuafBasvuruID).HasColumnName("MuafBasvuruID");
            this.Property(t => t.OgrenciID).HasColumnName("OgrenciID");
            this.Property(t => t.KayitSekliID).HasColumnName("KayitSekliID");
            this.Property(t => t.MuafIsUniID).HasColumnName("MuafIsUniID");
            this.Property(t => t.MuaIstFaktID).HasColumnName("MuaIstFaktID");
            this.Property(t => t.MuafIsBolumID).HasColumnName("MuafIsBolumID");
            this.Property(t => t.BasvuruTarihi).HasColumnName("BasvuruTarihi");
            this.Property(t => t.Durum).HasColumnName("Durum");
            this.Property(t => t.NotTipiID).HasColumnName("NotTipiID");

            // Relationships
            this.HasRequired(t => t.Kullanicilar)
                .WithMany(t => t.MuafiyetBasvurularis)
                .HasForeignKey(d => d.OgrenciID);
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.MuafiyetBasvurularis)
                .HasForeignKey(d => d.MuafIsUniID);

        }
    }
}
