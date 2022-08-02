using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class IcerikKararlariMap : EntityTypeConfiguration<IcerikKararlari>
    {
        public IcerikKararlariMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("IcerikKararlari");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MuafIstDersID).HasColumnName("MuafIstDersID");
            this.Property(t => t.AlinanDersID).HasColumnName("AlinanDersID");
            this.Property(t => t.KararUye1).HasColumnName("KararUye1");
            this.Property(t => t.KararUye2).HasColumnName("KararUye2");
            this.Property(t => t.KararUye3).HasColumnName("KararUye3");
            this.Property(t => t.KararTarihi).HasColumnName("KararTarihi");
            this.Property(t => t.KararUniID).HasColumnName("KararUniID");
            this.Property(t => t.KararFakulteID).HasColumnName("KararFakulteID");
            this.Property(t => t.KararBolumID).HasColumnName("KararBolumID");
            this.Property(t => t.CokluGrupID).HasColumnName("CokluGrupID");

            // Relationships
            this.HasRequired(t => t.DersIcerikleri)
                .WithMany(t => t.IcerikKararlaris)
                .HasForeignKey(d => d.MuafIstDersID);
            this.HasRequired(t => t.DersIcerikleri1)
                .WithMany(t => t.IcerikKararlaris1)
                .HasForeignKey(d => d.AlinanDersID);
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.IcerikKararlaris)
                .HasForeignKey(d => d.KararUniID);

        }
    }
}
