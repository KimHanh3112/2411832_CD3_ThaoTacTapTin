using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ThaoTacTapTin_CD3_HD1._2._2
{
    [XmlRoot("book")]
    public class Book
    {
        // ISBN nằm trong thuộc tính của <book>
        [XmlAttribute("ISBN")]
        public string ISBN { get; set; }

        // yearpublished cũng là thuộc tính
        [XmlAttribute("yearpublished")]
        public int YearPublished { get; set; }

        // Các thẻ con
        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                // Nếu books.xml nằm trong thư mục project
                string path = Path.GetFullPath(@"..\..\books.xml");

                if (!File.Exists(path))
                {
                    MessageBox.Show(
                        "Không tìm thấy file XML!\n\n" +
                        "Đường dẫn:\n" + path
                    );

                    return;
                }

                // Vì XML chỉ có 1 <book>
                XmlSerializer serializer =
                    new XmlSerializer(typeof(Book));

                Book book;

                using (StreamReader reader =
                    new StreamReader(path))
                {
                    book =
                        (Book)serializer.Deserialize(reader);
                }

                // Hiển thị dữ liệu đọc được
                string result =
                    "ISBN: " + book.ISBN + "\n" +
                    "Tên sách: " + book.Title + "\n" +
                    "Tác giả: " + book.Author + "\n" +
                    "Giá: " + book.Price + "\n" +
                    "Năm xuất bản: " + book.YearPublished;

                MessageBox.Show(
                    result,
                    "Thông tin sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\nChi tiết:\n" +
                               ex.InnerException.Message;
                }

                MessageBox.Show(
                    "Lỗi đọc XML:\n" + message
                );
            }
        }
    }
}