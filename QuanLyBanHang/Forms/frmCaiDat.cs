using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QuanLyBanHang.Forms
{
    public partial class frmCaiDat : Form
    {
        KetNoi kn = new KetNoi();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True");
        public void getData()
        {
            string query = string.Format("select * from dbo.NhanVien");
            DataSet ds = kn.LayDuLieu(query, "dbo.NhanVien");
            dgvNV.DataSource = ds.Tables["dbo.NhanVien"];
        }
        public frmCaiDat()
        {
            InitializeComponent();
        }
        public static string IDNV = "";
        private void autoIDNV()
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("select count(IDNV) from NhanVien", connect);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connect.Close();
            i++;
            txtIDNV.Text = i.ToString();
            IDNV = txtIDNV.Text;
        }

        private void frmCaiDat_Load(object sender, EventArgs e)
        {
            getData();
            autoIDNV();
            //Đổ dữ liệu vào textbox thông tin cửa hàng on load
            try
            {
                connect.Close();
                connect.Open();
                string sqlquery = "select TenCuaHang, Diachi, SDT, Loichao from dbo.ThongTin where ID='1'";
                SqlCommand command = new SqlCommand(sqlquery, connect);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    txtTenCuaHang.Text = sdr["TenCuaHang"].ToString();
                    txtSDT.Text = sdr["SDT"].ToString();
                    txtDiaChi.Text = sdr["Diachi"].ToString();
                    txtLoiChao.Text = sdr["Loichao"].ToString();
                }
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connect.Close();
            }
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                if (row >= 0)
                {
                    txtIDNV.Text = dgvNV.Rows[row].Cells["IDNV"].Value.ToString();
                    txtTenNV.Text = dgvNV.Rows[row].Cells["TenNV"].Value.ToString();
                    txtUserNV.Text = dgvNV.Rows[row].Cells["UserNV"].Value.ToString();
                    txtPassNV.Text = dgvNV.Rows[row].Cells["PassNV"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = string.Format("insert into dbo.NhanVien(IDNV, TenNV, UserNV, PassNV) values ('{0}','{1}','{2}','{3}')", txtIDNV.Text, txtTenNV.Text, txtUserNV.Text, txtPassNV.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Thêm mới thành công");
                getData();
            }
            else MessageBox.Show("Thêm mới thất bại");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete from dbo.NhanVien where IDNV = '{0}'", txtIDNV.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Xóa thành công");
                getData();
            }
            else MessageBox.Show("Xóa thất bại");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = string.Format("update dbo.NhanVien set TenNV='{1}', UserNV='{2}', PassNV='{3}' where IDNV = '{0}'", txtIDNV.Text, txtTenNV.Text, txtUserNV.Text, txtPassNV.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Sửa mới thành công");
                getData();
            }
            else MessageBox.Show("Sửa mới thất bại");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            getData();
            autoIDNV();
            txtTenNV.Text = "";
            txtUserNV.Text = "";
            txtPassNV.Text = "";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("update dbo.ThongTin set TenCuaHang = @TenCuaHang, DiaChi = @DiaChi, SDT = @SDT, LoiChao = @LoiChao where ID='1'", connect);
                cmd.Parameters.AddWithValue("@TenCuaHang", txtTenCuaHang.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                cmd.Parameters.AddWithValue("@LoiChao", txtLoiChao.Text);
                cmd.ExecuteNonQuery();
                connect.Close();
                Console.WriteLine("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cập nhật thất bại" + ex.Message);
            }
        }
    }
}
