using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Muafiyet.Models.Mapping
{
    public class KullanicilarMap : EntityTypeConfiguration<Kullanicilar>
    {
        public KullanicilarMap()
        {
            // Primary Key
            this.HasKey(t => t.KullaniciID);

            // Properties
            this.Property(t => t.KullaniciOkulNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.KullaniciAdi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.KullaniciSoyadi)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.KullaniciTlf)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.KullaniciDahili)
                .HasMaxLength(15);

            this.Property(t => t.KullaniciAdres)
                .HasMaxLength(300);

            this.Property(t => t.KullaniciEMail)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.KullaniciSifre)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Kullanicilar");
            this.Property(t => t.KullaniciID).HasColumnName("KullaniciID");
            this.Property(t => t.KullaniciOkulNo).HasColumnName("KullaniciOkulNo");
            this.Property(t => t.KullaniciAdi).HasColumnName("KullaniciAdi");
            this.Property(t => t.KullaniciSoyadi).HasColumnName("KullaniciSoyadi");
            this.Property(t => t.KullaniciTlf).HasColumnName("KullaniciTlf");
            this.Property(t => t.KullaniciDahili).HasColumnName("KullaniciDahili");
            this.Property(t => t.KullaniciAdres).HasColumnName("KullaniciAdres");
            this.Property(t => t.KullaniciEMail).HasColumnName("KullaniciEMail");
            this.Property(t => t.KullaniciSifre).HasColumnName("KullaniciSifre");
            this.Property(t => t.KullaniciUnvanID).HasColumnName("KullaniciUnvanID");
            this.Property(t => t.KullaniciUniID).HasColumnName("KullaniciUniID");
            this.Property(t => t.KullaniciFakulteID).HasColumnName("KullaniciFakulteID");
            this.Property(t => t.KullaniciBolumID).HasColumnName("KullaniciBolumID");
            this.Property(t => t.KullaniciRolID).HasColumnName("KullaniciRolID");
            this.Property(t => t.AktifMi).HasColumnName("AktifMi");

            // Relationships
            this.HasRequired(t => t.Universiteler)
                .WithMany(t => t.Kullanicilars)
                .HasForeignKey(d => d.KullaniciUniID);

        }
    }
}
