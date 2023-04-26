using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.Forms
{
    public partial class frmLuaChonHang : Form
    {
        public static string IDHoaDon = "";
        public frmLuaChonHang()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True");
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string getdate = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy");
        }
        private void autoIDHoaDon()
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("select count(IDHoaDon) from HoaDon", connect);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connect.Close();
            i++;
            txtIDHoaDon.Text = i.ToString();
            IDHoaDon = txtIDHoaDon.Text;
        }
        private void frmLuaChonHang_Load(object sender, EventArgs e)
        {
            DataTable dt = kn.XemDL("Select TenSP From dbo.NhapKho");
            foreach (DataRow dr in dt.Rows)
            {
                cboMaSP.Items.Add(dr["TenSP"].ToString());
            }
            autoIDHoaDon();
            txtIDHoaDon.Enabled = false; ;
        }
        private void cbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            SanPhamDaChon.Items.Add(cboMaSP.SelectedItem);
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("Select IDKH, TenKH from dbo.KhachHang where SDT = @SDT", connect);
                cmd.Parameters.AddWithValue("@SDT", int.Parse(txtSDT.Text));
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtTenKH.Text = da.GetValue(1).ToString();
                    txtIDKH.Text = da.GetValue(0).ToString();
                }
                connect.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi" + ex.Message);
            }
        }
        private void btnTaoHoaDon_Click_1(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into dbo.HoaDon values('" + txtIDHoaDon.Text.Trim() + "','" + this.dateTimePicker1.Value.ToString() + "','" + txtIDNV.Text + "',N'" + txtTenKH.Text.Trim() + "','" + 0 + "','" + txtIDKH.Text + "')";
                bool KT = kn.ThucThi(query);
                for (int i = 0; i < SanPhamDaChon.Items.Count; i++)
                {
                    string Ma = kn.XemDL("Select MaSP From dbo.NhapKho where TenSP='" + SanPhamDaChon.Items[i].ToString().Trim() + "'").Rows[0][0].ToString().Trim();
                    string Gia = kn.XemDL("Select GiaBan From dbo.NhapKho where MaSP='" + Ma + "'").Rows[0][0].ToString().Trim();
                    string Tien = kn.XemDL("Select GiaBan From dbo.NhapKho where MaSP='" + Ma + "'").Rows[0][0].ToString().Trim();
                    string queryCTHD = "Insert into dbo.ChiTietHoaDon values('" + txtIDHoaDon.Text.Trim() + "','" + Ma + "','" + SanPhamDaChon.Items[i].ToString().Trim() + "','" + 1 + "','" + txtLoai.Text + "','" + Gia + "','" + txtIDKH.Text +"','" + Convert.ToInt32(Tien) + "')";
                    bool KT2 = kn.ThucThi(queryCTHD);
                }
                this.Hide();
                frmChiTietHoaDon.IDHoaDon = txtIDHoaDon.Text.Trim();
                frmChiTietHoaDon frm = new frmChiTietHoaDon();
                frm.Show();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Tạo mới thất bại" + ex.Message);
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            txtIDNV.Clear();
            txtLoai.Clear();
            txtSDT.Clear();
            txtTenKH.Clear();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
