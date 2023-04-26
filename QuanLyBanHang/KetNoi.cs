using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyBanHang
{
    class KetNoi
    {
        private string conn_str = "Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True";

        public DataSet LayDuLieu(string query, string table_name)
        {
            try
            {
                SqlConnection conn = new SqlConnection(conn_str);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, table_name);
                return ds;
            }
            catch
            {

            }
            return null;
        }

        public bool ThucThi(string query)
        {
            try
            {
                SqlConnection conn = new SqlConnection(conn_str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int soluong = cmd.ExecuteNonQuery();
                conn.Close();
                return soluong > 0;
            }
            catch
            {

            }
            return false;
        }

        public DataTable XemDL(string sql)
        {
            SqlConnection conn = new SqlConnection(conn_str);
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
    }
}
