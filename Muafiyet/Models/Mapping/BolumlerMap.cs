using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class BolumlerMap : EntityTypeConfiguration<Bolumler>
    {
        public BolumlerMap()
        {
            // Primary Key
            this.HasKey(t => t.BolumID);

            // Properties
            this.Property(t => t.Bolum)
                .HasMaxLength(350);

            // Table & Column Mappings
            this.ToTable("Bolumler");
            this.Property(t => t.BolumID).HasColumnName("BolumID");
            this.Property(t => t.Bolum).HasColumnName("Bolum");
        }
    }
}
