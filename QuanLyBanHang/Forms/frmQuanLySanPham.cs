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
using System.Configuration;
using System.IO;

namespace QuanLyBanHang.Forms
{
    public partial class frmQuanLySanPham : Form
    {
        public frmQuanLySanPham()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        public void getData()
        {
            string query = string.Format("select * from dbo.NhapKho");
            DataSet ds = kn.LayDuLieu(query, "dbo.NhapKho");
            dgvSanPham.DataSource = ds.Tables["dbo.NhapKho"];
        }
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-KT8MQVO;Initial Catalog=QLBH;Integrated Security=True");
        String imgloc = "";
        public static string MaSP = "";
        private void autoMaSP()
        {
            connect.Open();
            SqlCommand cmd = new SqlCommand("select count(MaSP) from NhapKho", connect);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            connect.Close();
            i++;
            txtMaSP.Text = i.ToString();
            MaSP = txtMaSP.Text;
        }

        private void frmQuanLySanPham_Load(object sender, EventArgs e)
        {
            getData();
            autoMaSP();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    dateTimePicker1.Value = Convert.ToDateTime(dgvSanPham.Rows[row].Cells["NgayNhap"].Value.ToString());
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
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                // đưa ảnh về kiểu byte
                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                SqlCommand cmd = new SqlCommand("insert into dbo.NhapKho (MaSP,TenSP,GiaBan,Loai,AnhSP,NgayNhap,IDNV) values (@MaSp,@TenSP,@GiaBan,@Loai,@AnhSP,@NgayNhap,@IDNV)", connect);
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@Loai", txtLoai.Text);
                cmd.Parameters.AddWithValue("@AnhSP", img);
                cmd.Parameters.AddWithValue("@NgayNhap", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@IDNV", txtIDNV.Text);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Thêm mới sản phẩm thành công");
                getData();
                connect.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Thêm mới sản phẩm thất bại" + ex.Message);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string query = string.Format("delete from dbo.NhapKho where MaSP = '{0}'", txtMaSP.Text);
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
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtGiaBan.Text = "";
            txtIDNV.Text = "";
            txtLoai.Text = "";
            pictureBox1.Image = null;
            autoMaSP();
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("select * from dbo.NhapKho where TenSP='{0}'", txtTenSP.Text);
                DataSet ds = kn.LayDuLieu(query, "dbo.NhapKho");
                dgvSanPham.DataSource = ds.Tables["dbo.NhapKho"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không tìm thấy thông tin sản phẩm" + ex.Message);
            }
        }

        private void btnThemAnh_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofdlg = new OpenFileDialog();
                if (ofdlg.ShowDialog() == DialogResult.OK)
                {
                    imgloc = ofdlg.FileName.ToString();
                    pictureBox1.ImageLocation = imgloc; // hiển thị ảnh lên picturebox
                }
            }
            catch (Exception ex)
            {
                connect.Close();
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }

        private void btnXoaAnh_Click_1(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                connect.Close();
                MessageBox.Show("Lỗi" + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();
                // đưa ảnh về kiểu byte
                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                SqlCommand cmd = new SqlCommand("update dbo.NhapKho SET TenSP = @TenSP, GiaBan = @GiaBan, Loai = @Loai, AnhSP=@AnhSP, NgayNhap = @NgayNhap, IDNV = @IDNV where MaSP=@MaSP", connect);
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@GiaBan", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@Loai", txtLoai.Text);
                cmd.Parameters.AddWithValue("@AnhSP", img);
                cmd.Parameters.AddWithValue("@NgayNhap", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@IDNV", txtIDNV.Text);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sửa mới thành công");
                getData();
                connect.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sửa mới thất bại" + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string getdate = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy");
        }
    }
}
