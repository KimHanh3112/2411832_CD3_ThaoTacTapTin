using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace _2411832_NKHanh_CD3_BT
{
    public class DocGhiFile
    {
        public static List<SinhVien> DocFile(string tenFile)
        {
            string duoi = Path.GetExtension(tenFile).ToLower();

            if (duoi == ".txt")
                return DocTXT(tenFile);

            if (duoi == ".xml")
                return DocXML(tenFile);

            if (duoi == ".json")
                return DocJSON(tenFile);

            return new List<SinhVien>();
        }

        public static void GhiFile(string tenFile, List<SinhVien> ds)
        {
            string duoi = Path.GetExtension(tenFile).ToLower();

            if (duoi == ".txt")
                GhiTXT(tenFile, ds);
            else if (duoi == ".xml")
                GhiXML(tenFile, ds);
            else if (duoi == ".json")
                GhiJSON(tenFile, ds);
        }

        // ================= TXT =================

        private static List<SinhVien> DocTXT(string tenFile)
        {
            List<SinhVien> ds = new List<SinhVien>();

            if (!File.Exists(tenFile))
                return ds;

            foreach (string line in File.ReadAllLines(tenFile))
            {
                if (line.Trim() == "")
                    continue;

                string[] c = line.Split('|');

                SinhVien sv = new SinhVien();

                sv.MSSV = c[0];
                sv.HoTenLot = c[1];
                sv.Ten = c[2];
                sv.NgaySinh = DateTime.ParseExact(
                    c[3], "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                sv.GioiTinh = c[4] == "Nam";
                sv.Lop = c[5];
                sv.SoCMND = c[6];
                sv.SDT = c[7];
                sv.DiaChiLienLac = c[8];

                sv.MonHoc = new List<string>(c[9].Split(','));

                ds.Add(sv);
            }

            return ds;
        }

        private static void GhiTXT(string tenFile, List<SinhVien> ds)
        {
            List<string> lines = new List<string>();

            foreach (SinhVien sv in ds)
            {
                string dong =
                    sv.MSSV + "|" +
                    sv.HoTenLot + "|" +
                    sv.Ten + "|" +
                    sv.NgaySinh.ToString("dd/MM/yyyy") + "|" +
                    (sv.GioiTinh ? "Nam" : "Nu") + "|" +
                    sv.Lop + "|" +
                    sv.SoCMND + "|" +
                    sv.SDT + "|" +
                    sv.DiaChiLienLac + "|" +
                    string.Join(",", sv.MonHoc);

                lines.Add(dong);
            }

            File.WriteAllLines(tenFile, lines);
        }

        // ================= XML =================

        private static List<SinhVien> DocXML(string tenFile)
        {
            if (!File.Exists(tenFile))
                return new List<SinhVien>();

            XmlSerializer xl =
                new XmlSerializer(typeof(List<SinhVien>));

            using (FileStream fs =
                new FileStream(tenFile, FileMode.Open))
            {
                return (List<SinhVien>)xl.Deserialize(fs);
            }
        }

        private static void GhiXML(string tenFile, List<SinhVien> ds)
        {
            XmlSerializer xl =
                new XmlSerializer(typeof(List<SinhVien>));

            using (FileStream fs =
                new FileStream(tenFile, FileMode.Create))
            {
                xl.Serialize(fs, ds);
            }
        }

        // ================= JSON =================

        private static List<SinhVien> DocJSON(string tenFile)
        {
            if (!File.Exists(tenFile))
                return new List<SinhVien>();

            string json = File.ReadAllText(tenFile);

            List<SinhVien> ds =
                JsonConvert.DeserializeObject<List<SinhVien>>(json);

            if (ds == null)
                return new List<SinhVien>();

            return ds;
        }
        private static void GhiJSON(string tenFile, List<SinhVien> ds)
        {
            string json =
                JsonConvert.SerializeObject(ds, Formatting.Indented);

            File.WriteAllText(tenFile, json);
        }
    }
}