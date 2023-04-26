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
    public partial class frmChiTietHoaDon : Form
    {
        KetNoi kn = new KetNoi();
        public void getData()
        {
            dgvChiTietHoaDon.DataSource = kn.XemDL("Select a.TenSP,a.Soluong,b.GiaBan,ThanhTien From dbo.ChiTietHoaDon as a,dbo.NhapKho as b,dbo.HoaDon as c where a.MaSP=b.MaSP and a.IDHoaDon=c.IDHoaDon and c.IDHoaDon='" + IDHoaDon + "'");
            txtTongTien.Text = "0";
            for (int i = 0; i < dgvChiTietHoaDon.Rows.Count - 1; i++)
            {
                txtTongTien.Text = Convert.ToString(double.Parse(txtTongTien.Text) + double.Parse(dgvChiTietHoaDon.Rows[i].Cells[3].Value.ToString()));
            }
        }
        public static string IDHoaDon;
        public frmChiTietHoaDon()
        {
            InitializeComponent();
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            getData();
            txtIDHoaDon.Text = kn.XemDL("Select IDHoaDon from dbo.HoaDon Where IDHoaDon='" + IDHoaDon + "'").Rows[0][0].ToString();
        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row >= 0)
            {
                txtTenSP.Text = dgvChiTietHoaDon.Rows[row].Cells["TenSP"].Value.ToString();
                txtSoLuong.Text = dgvChiTietHoaDon.Rows[row].Cells["SoLuong"].Value.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHoaDon frm = new frmHoaDon();
            frm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            //Lấy mã sp để update chứ không thể update mã mà không có trong dbo.NhapKho
            String MaSP = kn.XemDL("Select MaSP from dbo.NhapKho where TenSP = '" + txtTenSP.Text + "'").Rows[0][0].ToString();
            //Cập nhật lại thành tiền khi số lượng thay đổi
            float Tien = Convert.ToInt32(kn.XemDL("Select GiaBan from dbo.NhapKho where MaSP = '" + MaSP + "'").Rows[0][0].ToString()) * Convert.ToInt32(txtSoLuong.Text);
            //update lại dbo.ChiTietHoaDon
            String query = "Update dbo.ChiTietHoaDon set SoLuong = '" + Convert.ToInt32(txtSoLuong.Text) + "' , ThanhTien = '" + Tien + "' where MaSP = '" + MaSP + "' and IDHoaDon = '" + txtIDHoaDon.Text + "'";
            bool kt = kn.ThucThi(query);
            getData();
        }

        private void btnLuuHoaDon_Click_1(object sender, EventArgs e)
        {
            //Cap nhat lai tong so tien trong hoa don truoc khi hien thi
            string query = "Update dbo.HoaDon set ThanhToan='" + txtTongTien.Text + "' where IDHoaDon='" + txtIDHoaDon.Text + "'";
            bool KT = kn.ThucThi(query);
            if (KT == true)
            {
                MessageBox.Show("Thêm hóa đơn thành công");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Thêm hóa đơn thất bại");
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
