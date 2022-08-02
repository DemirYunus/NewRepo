using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Muafiyet.Models.Mapping;

namespace Muafiyet.Models
{
    public partial class MuafContext : DbContext
    {
        static MuafContext()
        {
            Database.SetInitializer<MuafContext>(null);
        }

        public MuafContext()
            : base("Name=MuafContext")
        {
        }

        public DbSet<Ayarlar> Ayarlars { get; set; }
        public DbSet<DersIcerikleri> DersIcerikleris { get; set; }
        public DbSet<IcerikKararlari> IcerikKararlaris { get; set; }
        public DbSet<Kullanicilar> Kullanicilars { get; set; }
        public DbSet<MuafIstDersler> MuafIstDerslers { get; set; }
        public DbSet<MuafiyetBasvurulari> MuafiyetBasvurularis { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Universiteler> Universitelers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AyarlarMap());
            modelBuilder.Configurations.Add(new DersIcerikleriMap());
            modelBuilder.Configurations.Add(new IcerikKararlariMap());
            modelBuilder.Configurations.Add(new KullanicilarMap());
            modelBuilder.Configurations.Add(new MuafIstDerslerMap());
            modelBuilder.Configurations.Add(new MuafiyetBasvurulariMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UniversitelerMap());
        }
    }
}
