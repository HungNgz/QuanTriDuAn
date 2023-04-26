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
using QuanLyBanHang.Forms;

namespace QuanLyBanHang
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("select * from dbo.NhanVien where UserNV = '{0}' and PassNV='{1}'", txtUserNV.Text, txtPassNV.Text);
            DataSet ds = kn.LayDuLieu(query, "dbo.NhanVien");
            if (ds.Tables["dbo.NhanVien"].Rows.Count == 1)
            {
                MessageBox.Show("Đăng nhập thành công");
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else if (txtUserNV.Text == "")
            {
                MessageBox.Show("Không được để trống tài khoản");
            }
            else if (txtPassNV.Text == "")
            {
                MessageBox.Show("Không được để trống mật khẩu");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }

        private void btnQuen_Click(object sender, EventArgs e)
        {
            frmQuenMatKhau frmQuenMatKhau = new frmQuenMatKhau();
            frmQuenMatKhau.Show();
            this.Hide();
        }
    }
}
