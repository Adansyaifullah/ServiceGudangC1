using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceGudang
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1

    {
        string constring = "Data Source=LAPTOP-U4FJOL0S;Initial Catalog=Gudang Pakaian;Persist Security Info=True;User ID=sa;Password=123";
        SqlConnection connection;
        SqlCommand com;

        public string databarang(string IDBarang, string NamaBrand, string Ukuran, int JumlahBarang, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Barang values ('" + IDBarang + "', '" + NamaBrand + "', '" + Ukuran + "', "
                    + "" + JumlahBarang + ", '" + IDLokasi + "')";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set Stok = Stok - " + JumlahBarang + " where ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring); //fungsi konek database
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<DataBarang> DataBarang()
        {
            List<DataBarang> databarangs = new List<DataBarang>();
            try
            {
                string sql = " select ID_barang, Brand, Size, " + "Jumlah_barang, Nama_lokasi from dbo.Barang p join dbo.Lokasi l on p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    /*nama class*/
                    DataBarang data = new DataBarang(); // deklarasi data, mengambil 1persatu dari database
                    //bentuk array
                    data.IDBarang = reader.GetString(0);
                    data.NamaBrand = reader.GetString(1);
                    data.Ukuran = reader.GetString(2);
                    data.JumlahBarang = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    databarangs.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return databarangs;
        }

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_Login, Username, Password, Kategori from Login";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return list;
        }

        public string deleteDataBarang(string IDBarang)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Barang where ID_barang = '" + IDBarang + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_Login from dbo.Login where Username = '" + username + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                connection.Close();
                string sql2 = "delete from Login where ID_Login=" + id + "";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses utk mendeclare nama list yg telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Alamat_lengkap, Stok from dbo.Lokasi";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection); //proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();

                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.AlamatLengkap = reader.GetString(2);
                    data.Stok = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string editDataBarang(string IDBarang, string NamaBrand, string Ukuran)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Barang set Brand = '" + NamaBrand + "', Size = '" + Ukuran + "'" + " where ID_barang = '" + IDBarang + "' ";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from Login where Username='" + username + "' and Password='" + password + "'";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }
            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into dbo.Login values('" + username + "','" + password + "','" + kategori + "')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql2 = "update Login SET Username='" + username + "', Password='" + password + "', Kategori='" + kategori + "'" + " where ID_login = " + id + "";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                return "sukses";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
