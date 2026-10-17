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
    public partial class frmTimKiem : Form
    {
        QLSV qlsv;
        public frmTimKiem(QLSV ql)
        {
            InitializeComponent();
            qlsv = ql;
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {

            lvKetQua.View = View.Details;
            lvKetQua.FullRowSelect = true;
            lvKetQua.GridLines = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string mssv = txtTimMSSV.Text.Trim();
            string ten = txtTimTen.Text.Trim();
            string lop = txtTimLop.Text.Trim();

            if (mssv == "" &&
                ten == "" &&
                lop == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập ít nhất một điều kiện tìm kiếm!"
                );

                return;
            }

            List<SinhVien> ketQua =
                qlsv.TimNhieuDieuKien(
                    mssv,
                    ten,
                    lop
                );

            HienThiKetQua(ketQua);
        }
        private void HienThiKetQua(List<SinhVien> ds)
        {
            lvKetQua.Items.Clear();
            foreach (SinhVien sv in ds)
            {
                ListViewItem item =
                    new ListViewItem(sv.MSSV);

                item.SubItems.Add(sv.HoTenLot);
                item.SubItems.Add(sv.Ten);
                item.SubItems.Add(sv.Lop);
                item.SubItems.Add(sv.SDT);

                lvKetQua.Items.Add(item);
            }

            if (ds.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
