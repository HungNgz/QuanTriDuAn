using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.Forms
{
    public partial class frmQuenMatKhau : Form
    {
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        private void iconButton1_Click(object sender, EventArgs e)
        {
            frmDangNhap frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
            this.Hide();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            string query = string.Format("update dbo.NhanVien set PassNV='{1}' where UserNV='{0}'", txtUserNV.Text, txtPassNV.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Mật khẩu đã được cập nhật");
            }
            else MessageBox.Show("Thay đổi mật khẩu thất bại");
        }
    }
}
