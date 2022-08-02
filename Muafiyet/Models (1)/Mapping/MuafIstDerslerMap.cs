using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class MuafIstDerslerMap : EntityTypeConfiguration<MuafIstDersler>
    {
        public MuafIstDerslerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.AlinanDersNotu)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("MuafIstDersler");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MuafBasvID).HasColumnName("MuafBasvID");
            this.Property(t => t.MuafIstDersID).HasColumnName("MuafIstDersID");
            this.Property(t => t.AlinanDersID).HasColumnName("AlinanDersID");
            this.Property(t => t.AlinanDersNotu).HasColumnName("AlinanDersNotu");
            this.Property(t => t.DanismanUygunluk).HasColumnName("DanismanUygunluk");
            this.Property(t => t.IcerikUygunluk).HasColumnName("IcerikUygunluk");
            this.Property(t => t.NotUygunluk).HasColumnName("NotUygunluk");
            this.Property(t => t.KrediUygunluk).HasColumnName("KrediUygunluk");

            // Relationships
            this.HasRequired(t => t.DersIcerikleri)
                .WithMany(t => t.MuafIstDerslers)
                .HasForeignKey(d => d.MuafIstDersID);
            this.HasRequired(t => t.DersIcerikleri1)
                .WithMany(t => t.MuafIstDerslers1)
                .HasForeignKey(d => d.AlinanDersID);
            this.HasRequired(t => t.MuafiyetBasvurulari)
                .WithMany(t => t.MuafIstDerslers)
                .HasForeignKey(d => d.MuafBasvID);

        }
    }
}
