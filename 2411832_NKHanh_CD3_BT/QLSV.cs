using System;
using System.Collections.Generic;
using System.IO;

namespace _2411832_NKHanh_CD3_BT
{
    public class QLSV
    {
        private List<SinhVien> danhSach;

        private string tenFile;

        public List<SinhVien> DanhSach
        {
            get { return danhSach; }
        }

        // Constructor nhận đường dẫn file
        public QLSV(string file)
        {
            tenFile = file;

            danhSach = DocGhiFile.DocFile(tenFile);
        }

        public bool Them(SinhVien sv)
        {
            SinhVien sinhVienTimThay = TimTheoMSSV(sv.MSSV);

            if (sinhVienTimThay != null)
            {
                return false;
            }

            danhSach.Add(sv);

            LuuFile();

            return true;
        }

        public bool CapNhat(SinhVien svMoi)
        {
            SinhVien svCu = TimTheoMSSV(svMoi.MSSV);

            if (svCu == null)
            {
                return false;
            }

            svCu.HoTenLot = svMoi.HoTenLot;
            svCu.Ten = svMoi.Ten;
            svCu.NgaySinh = svMoi.NgaySinh;
            svCu.GioiTinh = svMoi.GioiTinh;
            svCu.Lop = svMoi.Lop;
            svCu.SoCMND = svMoi.SoCMND;
            svCu.SDT = svMoi.SDT;
            svCu.DiaChiLienLac = svMoi.DiaChiLienLac;
            svCu.MonHoc = svMoi.MonHoc;

            LuuFile();

            return true;
        }

        public SinhVien TimTheoMSSV(string mssv)
        {
            foreach (SinhVien sv in danhSach)
            {
                if (sv.MSSV == mssv)
                {
                    return sv;
                }
            }

            return null;
        }

        public List<SinhVien> TimTheoTen(string ten)
        {
            List<SinhVien> ketQua = new List<SinhVien>();

            foreach (SinhVien sv in danhSach)
            {
                if (sv.Ten.ToLower().Contains(ten.ToLower()))
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }

        public List<SinhVien> TimTheoLop(string lop)
        {
            List<SinhVien> ketQua = new List<SinhVien>();

            foreach (SinhVien sv in danhSach)
            {
                if (sv.Lop.ToLower().Contains(lop.ToLower()))
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }

        public bool Xoa(string mssv)
        {
            SinhVien sv = TimTheoMSSV(mssv);

            if (sv == null)
            {
                return false;
            }

            danhSach.Remove(sv);

            LuuFile();

            return true;
        }

        public void XoaNhieu(List<string> danhSachMSSV)
        {
            foreach (string mssv in danhSachMSSV)
            {
                SinhVien sv = TimTheoMSSV(mssv);

                if (sv != null)
                {
                    danhSach.Remove(sv);
                }
            }

            LuuFile();
        }

        public bool KiemTraMSSVTonTai(string mssv)
        {
            SinhVien sv = TimTheoMSSV(mssv);

            if (sv != null)
            {
                return true;
            }

            return false;
        }

        public void LuuFile()
        {
            DocGhiFile.GhiFile(tenFile, danhSach);
        }

        public void DocLaiFile()
        {
            danhSach = DocGhiFile.DocFile(tenFile);
        }
        public List<SinhVien> TimNhieuDieuKien(
           string mssv,
           string ten,
           string lop)
        {
            List<SinhVien> ketQua = new List<SinhVien>();

            foreach (SinhVien sv in danhSach)
            {
                bool dungMSSV = true;
                bool dungTen = true;
                bool dungLop = true;

                if (mssv != "")
                {
                    dungMSSV =
                        sv.MSSV.Contains(mssv);
                }

                if (ten != "")
                {
                    string hoTen =
                        sv.HoTenLot + " " + sv.Ten;

                    dungTen =
                        hoTen.ToLower()
                        .Contains(ten.ToLower());
                }

                if (lop != "")
                {
                    dungLop =
                        sv.Lop.ToLower()
                        .Contains(lop.ToLower());
                }

                if (dungMSSV && dungTen && dungLop)
                {
                    ketQua.Add(sv);
                }
            }

            return ketQua;
        }
    }
}