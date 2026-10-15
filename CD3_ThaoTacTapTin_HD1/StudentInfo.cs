using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD3_ThaoTacTapTin_HD1
{
    public class StudentInfo
    {
        public string MSSV { get; set; }
        public string Hoten { get; set; }
        public int Tuoi { get; set; }
        public double Diem { get; set; }
        public bool TonGiao { get; set; }

        public StudentInfo()
        { 
        }

        public StudentInfo(string mssv, string hoten, int tuoi, double diem, bool tongiao)
        {
            MSSV = mssv;
            Hoten = hoten;
            Tuoi = tuoi;
            Diem = diem;
            TonGiao = tongiao;
        }
    }
}
