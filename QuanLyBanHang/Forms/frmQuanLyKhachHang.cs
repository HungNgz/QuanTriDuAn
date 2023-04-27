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
    public partial class frmQuanLyKhachHang : Form
    {
        KetNoi kn = new KetNoi();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True");
        public void getData()
        {
            string query = string.Format("select * from dbo.KhachHang");
            DataSet ds = kn.LayDuLieu(query, "dbo.KhachHang");
            dgvQuanLyKhachHang.DataSource = ds.Tables["dbo.KhachHang"];
        }
        public static string IDKH = "";
        private void autoIDKH()
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("select count(IDKH) from KhachHang", connect);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connect.Close();
            i++;
            txtIDKH.Text = i.ToString();
            IDKH = txtIDKH.Text;
        }
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            getData();
            autoIDKH();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("insert into dbo.KhachHang(IDKH, TenKH, SDT) values ('{0}','{1}','{2}')", txtIDKH.Text, txtTenKH.Text, txtSDT.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Thêm mới thành công");
                getData();
            }
            else MessageBox.Show("Thêm mới thất bại");
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("update dbo.KhachHang set TenKH='{1}',SDT='{2}' where IDKH = '{0}'",txtIDKH.Text,txtTenKH.Text,txtSDT.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Sửa mới thành công");
                getData();
            }
            else MessageBox.Show("Sửa mới thất bại");
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("delete from dbo.KhachHang where SDT = '{0}'", txtSDT.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Xóa thành công");
                getData();
            }
            else MessageBox.Show("Xóa thất bại");
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            getData();
            autoIDKH();
            txtTenKH.Text = "";
            txtSDT.Text = "";


        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("select * from dbo.KhachHang where SDT = '{0}'", txtTimKiem.Text);
            DataSet ds = kn.LayDuLieu(query, "dbo.KhachHang");
            dgvQuanLyKhachHang.DataSource = ds.Tables["dbo.KhachHang"];
        }

        private void dgvQuanLyKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                if (row >= 0)
                {
                    txtIDKH.Text = dgvQuanLyKhachHang.Rows[row].Cells["IDKH"].Value.ToString();
                    txtTenKH.Text = dgvQuanLyKhachHang.Rows[row].Cells["TenKH"].Value.ToString();
                    txtSDT.Text = dgvQuanLyKhachHang.Rows[row].Cells["SDT"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
        }
    }
}
