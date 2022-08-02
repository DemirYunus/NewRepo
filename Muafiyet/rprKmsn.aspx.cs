using Microsoft.Reporting.WebForms;
using Muafiyet.App_DataSet;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Muafiyet
{
    public partial class rprKmsn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MuafiyetBasvurulari ctx = new MuafContext();
            //List<View_Rapor_Komisyon> rpr = ctx.View_Rapor_Komisyon.Where(x => x.MuafBasvuruID == 37).ToList();


            //dsKmsnRaporu ds = new dsKmsnRaporu();
            //DataTable dt = ds.Tables.Add("dtKmsnRaporu");

            //dt.Columns.Add("KullaniciOkulNo");
            //dt.Columns.Add("KullaniciAdi");
            //dt.Columns.Add("KullaniciSoyadi");
            //dt.Columns.Add("BasvuruTarihi");
            //dt.Columns.Add("Universite");
            //dt.Columns.Add("Fakulte");
            //dt.Columns.Add("Bolum");
            //dt.Columns.Add("DersAdi");
            //dt.Columns.Add("Kredi");
            //dt.Columns.Add("AlinanDersAdi");
            //dt.Columns.Add("Kredisi");
            //dt.Columns.Add("AlinanDersNotu");
            //dt.Columns.Add("MuafDers");
            //dt.Columns.Add("Gerekce");

            //int sayac = 0;
            //foreach (View_Rapor_Komisyon r in rpr)
            //{
            //    DataRow myRow = dt.NewRow();
            //    dt.Rows.Add(myRow);
            //    dt.Rows[sayac][0] = r.KullaniciOkulNo;
            //    dt.Rows[sayac][1] = r.KullaniciAdi;
            //    dt.Rows[sayac][2] = r.KullaniciSoyadi;
            //    dt.Rows[sayac][3] = r.BasvuruTarihi;
            //    dt.Rows[sayac][4] = r.Universite;
            //    dt.Rows[sayac][5] = r.Fakulte;
            //    dt.Rows[sayac][6] = r.Bolum;
            //    dt.Rows[sayac][7] = r.DersAdi;
            //    dt.Rows[sayac][8] = r.Kredi;
            //    dt.Rows[sayac][9] = r.AlinanDersAdi;
            //    dt.Rows[sayac][10] = r.Kredisi;
            //    dt.Rows[sayac][11] = r.AlinanDersNotu;

            //    if (r.DanismanUygunluk==0 || r.KrediUygunluk==0 || r.NotUygunluk==0 || r.IcerikUygunluk==0)
            //    {
            //        dt.Rows[sayac][12] = "";   

            //        dt.Rows[sayac][13] = "";
            //    }
            //    else
            //    {
            //        if (r.KrediUygunluk == -1 || r.NotUygunluk == -1 || r.IcerikUygunluk == -1 || r.DanismanUygunluk == -1)
            //        {
            //            dt.Rows[sayac][12] = "";
                        

            //            string durum = "";
            //            if (r.KrediUygunluk == -1)
            //            {
            //                durum = "Kredi yetersiz";
            //            }
            //            if (r.NotUygunluk == -1)
            //            {
            //                durum = "Notu yetersiz";
            //            }
            //            if (r.IcerikUygunluk == -1)
            //            {
            //                durum = "İçerik uygun değil";
            //            }
            //            if (r.DanismanUygunluk == -1)
            //            {
            //                durum = "Danışmanca uygun değil";
            //            }

            //            dt.Rows[sayac][13] = durum;                       
            //        }
            //        else
            //        {
            //            dt.Rows[sayac][12] = r.DersAdi;                       

            //            dt.Rows[sayac][13] = "";                       
            //        }
            //    }
            //    sayac = sayac + 1;
            //}

            //ReportDataSource rds = new ReportDataSource("dsKmsnRaporu", ds.Tables[0]); //
            //ReportViewer1.LocalReport.ReportPath=Server.MapPath("/Reports/KmsnRaporu.rdlc");
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.Refresh();

            //ReportParameter[] parms = new ReportParameter[4];
            //parms[0] = new ReportParameter("Intibak", "3.Yarıyıla intibak olacak");
            //parms[1] = new ReportParameter("KmsnBaskani", "Yrd.Doç.Dr. Burak ERKAYMAN");
            //parms[2] = new ReportParameter("KmsnUye1", "Yrd.Doç.Dr. M.Emre KESKİN");
            //parms[3] = new ReportParameter("KmsnUye2", "Arş.Gör. Yunus DEMİR");
            //ReportViewer1.LocalReport.SetParameters(parms);
            //ReportViewer1.LocalReport.Refresh();
        }
    }
}