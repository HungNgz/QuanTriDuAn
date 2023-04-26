using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QuanLyBanHang.Forms
{
    public partial class frmTrangChinh : Form
    {
        public frmTrangChinh()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True");
        
        public void getData()
        {
            string query = string.Format("select * from dbo.NhapKho");
            DataSet ds = kn.LayDuLieu(query, "dbo.NhapKho");
            dgvSanPham.DataSource = ds.Tables["dbo.NhapKho"];
        }
        public static string IDHoaDon = "";
        public static string IDKH = "";
        //private void autoIDHD()
        //{
        //    connect.Open();
        //    SqlCommand cmd = new SqlCommand("select count(IDHoaDon) from HoaDon", connect);
        //    int i = Convert.ToInt32(cmd.ExecuteScalar());
        //    i++;
        //    txtIDHoaDon.Text = i.ToString();
        //    IDHoaDon = txtIDHoaDon.Text;
        //    connect.Close();
        //}
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
        private void frmTrangChinh_Load(object sender, EventArgs e)
        {
            //autoIDHD();
            autoIDKH();
            
        }
        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {
            
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
                    txtIDKH.Text = da.GetValue(0).ToString();
                    txtTenKH.Text = da.GetValue(1).ToString();
                }
                connect.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Loi" + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = string.Format("insert into dbo.KhachHang (IDKH, TenKH, SDT) values ('{7}','{8}','{9}')", txtIDKH.Text, txtTenKH.Text, txtSDT.Text);
            bool kt = kn.ThucThi(query);
            if (kt == true)
            {
                MessageBox.Show("Them moi thanh cong");
                getData();
            }
            else MessageBox.Show("Them moi that bai");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            connect.Close();
            txtIDKH.Text = "";
            txtTenKH.Text = "";
            txtSDT.Text = "";
            autoIDKH();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string query = string.Format("select * from dbo.NhapKho where TenSP like '%{0}%'", txtTimKiemSP.Text);
            DataSet ds = kn.LayDuLieu(query, "dbo.NhapKho");
            dgvSanPham.DataSource = ds.Tables["dbo.NhapKho"];
        }

        private void dgvSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                if (row >= 0)
                {
                    txtMaSP.Text = dgvSanPham.Rows[row].Cells["MaSP"].Value.ToString();
                    txtTenSP.Text = dgvSanPham.Rows[row].Cells["TenSP"].Value.ToString();
                    txtGiaBan.Text = dgvSanPham.Rows[row].Cells["GiaBan"].Value.ToString();
                    txtLoai.Text = dgvSanPham.Rows[row].Cells["Loai"].Value.ToString();
                    txtIDNV.Text = dgvSanPham.Rows[row].Cells["IDNV"].Value.ToString();
                    string query = string.Format("select AnhSP from dbo.NhapKho where MaSp='{0}'", txtMaSP.Text);
                    SqlDataAdapter da = new SqlDataAdapter(query, connect);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "dbo.NhapKho");
                    Byte[] data = new Byte[0];
                    data = (Byte[])(ds.Tables[0].Rows[0]["AnhSP"]);
                    MemoryStream mem = new MemoryStream(data);
                    pictureBox1.Image = Image.FromStream(mem);
                }
            }
            catch
            {

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string getdate = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy");
        }

        private void btnThemAll_Click(object sender, EventArgs e)
        {
            dgvHoaDon.Rows.Add(txtMaSP.Text,txtMaSP.Text, txtTenSP.Text, txtSoLuong.Text, txtGiaBan.Text, txtLoai.Text, dateTimePicker1.Value.ToString(), txtIDNV.Text, txtIDKH.Text, txtTenKH.Text, txtSDT.Text);
            //autoIDHD();
        }

        private void dgvHoaDon_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvHoaDon.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            for (int i =0; i< dgvHoaDon.Rows.Count-1; i++)
            {
                
                
                SqlCommand cmd = new SqlCommand(@"insert into dbo.HoaDon values ('" +dgvHoaDon.Rows[i].Cells[0].Value+ "', '" +dgvHoaDon.Rows[i].Cells[0].Value+ "', '" +dgvHoaDon.Rows[i].Cells[0].Value+ "', '" +dgvHoaDon.Rows[i].Cells[0].Value+ "', '" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"','" +dgvHoaDon.Rows[i].Cells[0].Value+"', '"+label12.Text+"')");
                
                cmd.ExecuteNonQuery();
                
            }
        }

        private void btnTinhTong_Click(object sender, EventArgs e)
        {
            label12.Text = "0";
            for (int i = 0; i < dgvHoaDon.Rows.Count - 1; i++)
            {
                double soluong = double.Parse(dgvHoaDon.Rows[i].Cells[3].Value.ToString());
                double gia = double.Parse(dgvHoaDon.Rows[i].Cells[4].Value.ToString());
                label12.Text = Convert.ToString(double.Parse(label12.Text) + (soluong * gia));
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtIDKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
