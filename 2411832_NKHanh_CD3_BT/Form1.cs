using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2411832_NKHanh_CD3_BT
{
    public partial class Form1 : Form
    {
        QLSV qlsv;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Title = "Chọn file danh sách sinh viên";

            open.Filter =
                "File sinh viên|*.txt;*.xml;*.json|" +
                "TXT|*.txt|" +
                "XML|*.xml|" +
                "JSON|*.json";

            if (open.ShowDialog() == DialogResult.OK)
            {
                qlsv = new QLSV(open.FileName);

                HienThiDanhSach(qlsv.DanhSach);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn file!");

                this.Close();
            }
        }
        private void HienThiDanhSach(List<SinhVien> ds)
        {
            lvSinhVien.Items.Clear();

            foreach (SinhVien sv in ds)
            {
                ListViewItem item = new ListViewItem(sv.MSSV);

                item.SubItems.Add(sv.HoTenLot);
                item.SubItems.Add(sv.Ten);
                item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sv.Lop);
                item.SubItems.Add(sv.SoCMND);
                item.SubItems.Add(sv.SDT);
                item.SubItems.Add(sv.DiaChiLienLac);

                item.Tag = sv;

                lvSinhVien.Items.Add(item);
            }
        }
        private List<string> LayDanhSachMon()
        {
            List<string> dsMon = new List<string>();

            foreach (object mon in clbMonHocDK.CheckedItems)
            {
                dsMon.Add(mon.ToString());
            }

            return dsMon;
        }
        private SinhVien LaySinhVien()
        {
            SinhVien sv = new SinhVien();

            sv.MSSV = mtbMSSV.Text.Trim();
            sv.HoTenLot = txtHoTen.Text.Trim();
            sv.Ten = txtTen.Text.Trim();
            sv.NgaySinh = dtbNgaySinh.Value;

            sv.GioiTinh = rdNam.Checked;

            sv.Lop = cboLop.Text.Trim();
            sv.SoCMND = mtbSoCMND.Text.Trim();

            sv.SDT = mtbSDT.Text
                .Replace(".", "")
                .Trim();

            sv.DiaChiLienLac = txtDChi.Text.Trim();

            sv.MonHoc = LayDanhSachMon();

            return sv;
        }
        private bool KiemTraDuLieu()
        {
            if (!mtbMSSV.MaskCompleted ||
                txtHoTen.Text.Trim() == "" ||
                txtTen.Text.Trim() == "" ||
                cboLop.Text.Trim() == "" ||
                !mtbSoCMND.MaskCompleted ||
                !mtbSDT.MaskCompleted ||
                txtDChi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (!rdNam.Checked && !rdNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return false;
            }

            if (clbMonHocDK.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một môn học!");
                return false;
            }

            if (!KiemTraMSSV())
                return false;
            return true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            SinhVien sv = LaySinhVien();

            if (qlsv.Them(sv))
            {
                MessageBox.Show("Thêm sinh viên thành công!");

                HienThiDanhSach(qlsv.DanhSach);

                XoaTrang();
            }
        }
        private void XoaTrang()
        {
            mtbMSSV.Clear();
            txtHoTen.Clear();
            txtTen.Clear();

            mtbSoCMND.Clear();
            mtbSDT.Clear();
            txtDChi.Clear();

            rdNam.Checked = false;
            rdNu.Checked = false;

            cboLop.SelectedIndex = -1;

            dtbNgaySinh.Value = DateTime.Now;

            for (int i = 0; i < clbMonHocDK.Items.Count; i++)
            {
                clbMonHocDK.SetItemChecked(i, false);
            }

            mtbMSSV.Focus();
        }


        private void HienThiMonHoc(SinhVien sv)
        {
            for (int i = 0; i < clbMonHocDK.Items.Count; i++)
            {
                string mon = clbMonHocDK.Items[i].ToString();

                if (sv.MonHoc.Contains(mon))
                {
                    clbMonHocDK.SetItemChecked(i, true);
                }
                else
                {
                    clbMonHocDK.SetItemChecked(i, false);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            SinhVien sv = LaySinhVien();

            if (qlsv.CapNhat(sv))
            {
                MessageBox.Show("Cập nhật thành công!");

                HienThiDanhSach(qlsv.DanhSach);
            }
            else
            {
                MessageBox.Show(
                    "Không tìm thấy sinh viên!");
            }
        }

        private void mnuThem_Click(object sender, EventArgs e)
        {
            frmThemMon frm = new frmThemMon();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string mon = frm.TenMon;

                if (clbMonHocDK.Items.Contains(mon))
                {
                    MessageBox.Show("Môn học đã tồn tại!");
                    return;
                }

                clbMonHocDK.Items.Add(mon);

                MessageBox.Show("Thêm môn thành công!");
            }
        }

        private void mnuXoa_Click(object sender, EventArgs e)
        {
            if (clbMonHocDK.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn môn cần xóa!");

                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa môn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                clbMonHocDK.Items.RemoveAt(
                    clbMonHocDK.SelectedIndex);
            }
        }

        private void mnuXoaSV_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn sinh viên cần xóa!");

                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa các sinh viên đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.No)
                return;

            List<string> dsMSSV = new List<string>();

            foreach (ListViewItem item
                in lvSinhVien.CheckedItems)
            {
                dsMSSV.Add(item.Text);
            }

            qlsv.XoaNhieu(dsMSSV);

            HienThiDanhSach(qlsv.DanhSach);

            XoaTrang();

            MessageBox.Show("Xóa thành công!");

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show(
        "Bạn có chắc chắn muốn thoát chương trình không?",
        "Xác nhận thoát",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            frmTimKiem frm = new frmTimKiem(qlsv);

            frm.ShowDialog();
        }

        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count == 0)
                return;

            SinhVien sv =
                lvSinhVien.SelectedItems[0].Tag as SinhVien;

            if (sv == null)
                return;

            mtbMSSV.Text = sv.MSSV;
            txtHoTen.Text = sv.HoTenLot;
            txtTen.Text = sv.Ten;

            dtbNgaySinh.Value = sv.NgaySinh;

            rdNam.Checked = sv.GioiTinh;
            rdNu.Checked = !sv.GioiTinh;

            cboLop.Text = sv.Lop;

            mtbSoCMND.Text = sv.SoCMND;
            mtbSDT.Text = sv.SDT;

            txtDChi.Text = sv.DiaChiLienLac;

            HienThiMonHoc(sv);
        }
        private string LayNamNhapHoc(string lop)
        {
            if (lop.Contains("K42"))
                return "18";

            if (lop.Contains("K43"))
                return "19";

            if (lop.Contains("K44"))
                return "20";

            if (lop.Contains("K45"))
                return "21";

            if (lop.Contains("K46"))
                return "22";

            if (lop.Contains("K47"))
                return "23";

            if (lop.Contains("K48"))
                return "24";

            if (lop.Contains("K49"))
                return "25";

            if (lop.Contains("K50"))
                return "26";

            return "";
        }
        private bool KiemTraMSSV()
        {
            string mssv = mtbMSSV.Text.Trim();
            string namNhapHoc = LayNamNhapHoc(cboLop.Text);

            // MSSV phải đủ 7 số
            if (!mtbMSSV.MaskCompleted)
            {
                MessageBox.Show("MSSV phải gồm đúng 7 chữ số!");
                return false;
            }

            // Kiểm tra lớp
            if (namNhapHoc == "")
            {
                MessageBox.Show("Không xác định được năm nhập học từ lớp!");
                return false;
            }

            // Kiểm tra AA
            if (mssv.Substring(0, 2) != namNhapHoc)
            {
                MessageBox.Show(
                    "2 số đầu MSSV phải là " + namNhapHoc +
                    " theo năm nhập học của lớp!");

                return false;
            }

            // Kiểm tra BB = 10
            if (mssv.Substring(2, 2) != "10")
            {
                MessageBox.Show(
                    "Số thứ 3 và thứ 4 của MSSV phải là 10!");

                return false;
            }

            return true;
        }
    }
}
