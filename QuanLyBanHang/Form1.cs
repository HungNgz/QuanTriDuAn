using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using QuanLyBanHang.Forms;

namespace QuanLyBanHang
{
    public partial class Form1 : Form
    {
        private Form currentChildForm;
        private void OpenChildForm(Form childForm)
        {
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTrangChinh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHome());
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLySanPham());
        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyKhachHang());
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCaiDat());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHome());
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDon());
        }
    }
}
