using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class FakultelerMap : EntityTypeConfiguration<Fakulteler>
    {
        public FakultelerMap()
        {
            // Primary Key
            this.HasKey(t => t.FakulteID);

            // Properties
            this.Property(t => t.Fakulte)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Fakulteler");
            this.Property(t => t.FakulteID).HasColumnName("FakulteID");
            this.Property(t => t.Fakulte).HasColumnName("Fakulte");
        }
    }
}
