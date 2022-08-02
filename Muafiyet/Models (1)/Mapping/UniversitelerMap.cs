using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class UniversitelerMap : EntityTypeConfiguration<Universiteler>
    {
        public UniversitelerMap()
        {
            // Primary Key
            this.HasKey(t => t.UniversiteID);

            // Properties
            this.Property(t => t.Universite)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Universiteler");
            this.Property(t => t.UniversiteID).HasColumnName("UniversiteID");
            this.Property(t => t.Universite).HasColumnName("Universite");
        }
    }
}
