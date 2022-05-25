using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WindowsFormsApp1
{
    class Logika
    {
        SqlConnection veza = new SqlConnection(ConfigurationManager.ConnectionStrings["Cs"].ConnectionString);
        SqlCommand komanda = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet set = new DataSet();
        DataSet vreme = new DataSet();
        DataSet termin = new DataSet();
        DataSet idkor = new DataSet();
        DataSet idprod = new DataSet();
        DataSet search = new DataSet();
        DataSet zak = new DataSet();
        DataSet ostl = new DataSet();
        public int Logovanje(string email, string lozinka)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "KorisnikLogin";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, email));
            komanda.Parameters.Add(new SqlParameter("@lozinka", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, lozinka));
            komanda.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Current, null));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            return (int)komanda.Parameters["@RETURN_VALUE"].Value;
        }
        public int NovNalog(string email, string lozinka)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "KorisnikInsert";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, email));
            komanda.Parameters.Add(new SqlParameter("@sifra", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, lozinka));
            komanda.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Current, null));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            return (int)komanda.Parameters["@RETURN_VALUE"].Value;

        }
        public DataSet Kategorije()
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "Kategorije";
            komanda.Parameters.Clear();


            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(set);
            return set;
        }
        public DataSet Prodavnice(string kategorija)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "PokaziProd";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@kategorija", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, kategorija));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(set);
            return set;
        }
        public DataSet Termini(string prodavnica)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "SviTermini";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@prodavnica", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, prodavnica));
            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(termin);
            return termin;
        }
        public DataSet Vremena()
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "SviVremena";
            komanda.Parameters.Clear();

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(vreme);
            return vreme;
        }
        public int UpisiTermin(int korisnik, int prodavnica, DateTime dan, string vreme, string usluga)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "UpisiTermin";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@korisnik", SqlDbType.Int, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, korisnik));
            komanda.Parameters.Add(new SqlParameter("@prodavnica", SqlDbType.Int, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, prodavnica));
            komanda.Parameters.Add(new SqlParameter("@dan", SqlDbType.Date, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, dan));
            komanda.Parameters.Add(new SqlParameter("@vreme", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, vreme));
            komanda.Parameters.Add(new SqlParameter("@usluga", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, usluga));

            komanda.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Current, null));


            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            return (int)komanda.Parameters["@RETURN_VALUE"].Value;
        }

        public int NadjiKor(string email)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "NadjiKor";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, email));


            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(idkor);
            int rez = (int)idkor.Tables[0].Rows[0]["id"];
            return rez;
        }
        public int NadjiProd(string naziv)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "Nadjiprod";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@naziv", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, naziv));


            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(idprod);
            int rez = (int)idprod.Tables[0].Rows[0]["id"];
            return rez;
        }
        public DataSet Search(string term)
        {
            term = "%" + term + "%";
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "Search";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@term", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, term));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(search);

            return search;
        }
        public DataSet MojaZak(string email)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "MojaZakazivanja";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, email));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(zak);

            return zak;
        }
        public DataSet OstlVreme(string prodavnica, DateTime date)
        {
            komanda.Connection = veza;
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.CommandText = "OstalaVremena";
            komanda.Parameters.Clear();
            komanda.Parameters.Add(new SqlParameter("@prodavnica", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, prodavnica));
            komanda.Parameters.Add(new SqlParameter("@date", SqlDbType.Date, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Current, date));

            veza.Open();
            komanda.ExecuteNonQuery();
            veza.Close();
            adapter.SelectCommand = komanda;
            adapter.Fill(ostl);


            return ostl;
        }


    }
}
