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
    public partial class frmThemMon : Form
    {
        public string TenMon
        {
            get { return txtTenMon.Text.Trim(); }
        }
        public frmThemMon()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenMon.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên môn học!");
                txtTenMon.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
