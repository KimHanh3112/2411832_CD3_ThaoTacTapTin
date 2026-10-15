using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2411832_NKHanh_CD3_BT
{
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMND { get; set; }
        public string DiaChiLienLac { get; set; }
        public bool GioiTinh { get; set; }
        public string Ten { get; set; }
        public string Lop { get; set; }
        public string SDT { get; set; }
        public List<string> MonHoc { get; set; }

        public string HoTen()
        {
            return HoTenLot + " " + Ten;
        }

        public SinhVien()
        {
            MonHoc = new List<string>();
        }

        public SinhVien(string mssv, string hoTenLot, DateTime ngaySinh, string soCMND,
            string diaChiLienLac, bool gioiTinh, string ten, string lop, string sdt, List<string> monHoc)
        {
            MSSV = mssv;
            HoTenLot = hoTenLot;
            NgaySinh = ngaySinh;
            SoCMND = soCMND;
            DiaChiLienLac = diaChiLienLac;
            GioiTinh = gioiTinh;
            Ten = ten;
            Lop = lop;
            SDT = sdt;
            MonHoc = monHoc;
        }
    }
}
