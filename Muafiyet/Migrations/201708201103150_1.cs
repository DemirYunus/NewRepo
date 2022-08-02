namespace Muafiyet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BolumDersleri",
                c => new
                    {
                        DersID = c.Int(nullable: false, identity: true),
                        DersKodu = c.String(maxLength: 50),
                        DersAdi = c.String(maxLength: 100),
                        DersKisaAdi = c.String(maxLength: 50),
                        Drm_Blm_Fakt = c.Int(),
                        Drm_Onkosul = c.Int(),
                        Drm_Derece = c.Int(),
                        Drm_Bahar_Guz = c.Int(),
                        Ders_UniID = c.Int(),
                        Ders_FakulteID = c.Int(),
                        Ders_BolumID = c.Int(),
                        Ders_SinifID = c.Int(),
                        Aktif = c.Int(),
                        TeorikSaat = c.Int(),
                        PratikSaat = c.Int(),
                        LabSaat = c.Int(),
                        Kredi = c.Int(),
                        Drm_SecZrn = c.Int(),
                        Drm_FaktDersTipi = c.Int(),
                    })
                .PrimaryKey(t => t.DersID);
            
            CreateTable(
                "dbo.Bolumler",
                c => new
                    {
                        BolumID = c.Int(nullable: false, identity: true),
                        Bolum = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.BolumID);
            
            CreateTable(
                "dbo.DersIcerikleri",
                c => new
                    {
                        AlinanDersID = c.Int(nullable: false, identity: true),
                        AlinanUniID = c.Int(),
                        AlinanFakülteID = c.Int(),
                        AlinanBolumID = c.Int(),
                        AlinanDersAdi = c.String(maxLength: 200),
                        IcerikLinki = c.String(maxLength: 200),
                        IcerikNotu = c.String(maxLength: 500),
                        IcerikResim = c.String(),
                    })
                .PrimaryKey(t => t.AlinanDersID);
            
            CreateTable(
                "dbo.enmKayitSekli",
                c => new
                    {
                        KayitSekliID = c.Int(nullable: false, identity: true),
                        KayitSekli = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.KayitSekliID);
            
            CreateTable(
                "dbo.enmUnvanlar",
                c => new
                    {
                        UnvanID = c.Int(nullable: false, identity: true),
                        Unvan = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.UnvanID);
            
            CreateTable(
                "dbo.Fakulteler",
                c => new
                    {
                        FakulteID = c.Int(nullable: false, identity: true),
                        Fakulte = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.FakulteID);
            
            CreateTable(
                "dbo.Kullanicilar",
                c => new
                    {
                        KullaniciID = c.Int(nullable: false, identity: true),
                        KullaniciOkulNo = c.String(maxLength: 20),
                        KullaniciAdi = c.String(maxLength: 100),
                        KullaniciSoyadi = c.String(maxLength: 100),
                        KullaniciTlf = c.String(maxLength: 50),
                        KullaniciAdres = c.String(maxLength: 300),
                        KullaniciEMail = c.String(maxLength: 100),
                        KullaniciSifre = c.String(maxLength: 100),
                        KullaniciUnvanID = c.Int(),
                        KullaniciUniID = c.Int(),
                        KullaniciFakulteID = c.Int(),
                        KullaniciBolumID = c.Int(),
                    })
                .PrimaryKey(t => t.KullaniciID);
            
            CreateTable(
                "dbo.MuafiyetBasvurulari",
                c => new
                    {
                        MuafBasvID = c.Int(nullable: false, identity: true),
                        OgrenciID = c.Int(),
                        KayitSekliID = c.Int(),
                        MuafIstDersID = c.Int(),
                        AlinanDersID = c.Int(),
                        AlinanDersNotu = c.String(maxLength: 5),
                        BasvuruTarihi = c.String(maxLength: 50),
                        DanismanUygunluk = c.Int(),
                        IcerikUygunluk = c.Int(),
                        NotUygunluk = c.Int(),
                        KrediUygunluk = c.Int(),
                    })
                .PrimaryKey(t => t.MuafBasvID);
            
            CreateTable(
                "dbo.Roller",
                c => new
                    {
                        RolID = c.Int(nullable: false, identity: true),
                        Rol = c.String(maxLength: 100),
                        RolAciklama = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.RolID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.Universiteler",
                c => new
                    {
                        UniversiteID = c.Int(nullable: false, identity: true),
                        Universite = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.UniversiteID);
            
            CreateTable(
                "dbo.View_Kullanicilar",
                c => new
                    {
                        KullaniciID = c.Int(nullable: false),
                        KullaniciOkulNo = c.String(maxLength: 20),
                        KullaniciAdi = c.String(maxLength: 100),
                        KullaniciSoyadi = c.String(maxLength: 100),
                        KullaniciTlf = c.String(maxLength: 50),
                        KullaniciAdres = c.String(maxLength: 300),
                        KullaniciEMail = c.String(maxLength: 100),
                        KullaniciSifre = c.String(maxLength: 100),
                        Unvan = c.String(maxLength: 100),
                        Universite = c.String(maxLength: 200),
                        Fakulte = c.String(maxLength: 200),
                        Bolum = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.KullaniciID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.View_Kullanicilar");
            DropTable("dbo.Universiteler");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Roller");
            DropTable("dbo.MuafiyetBasvurulari");
            DropTable("dbo.Kullanicilar");
            DropTable("dbo.Fakulteler");
            DropTable("dbo.enmUnvanlar");
            DropTable("dbo.enmKayitSekli");
            DropTable("dbo.DersIcerikleri");
            DropTable("dbo.Bolumler");
            DropTable("dbo.BolumDersleri");
        }
    }
}
