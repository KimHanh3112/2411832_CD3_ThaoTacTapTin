using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ThaoTacTapTin_HD1._2._1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                // Đường dẫn đến file books.xml
                string path = Path.Combine(
                    Application.StartupPath,
                    "books.xml"
                );

                // Kiểm tra file có tồn tại không
                if (!File.Exists(path))
                {
                    MessageBox.Show(
                        "Không tìm thấy file books.xml!\n\n" +
                        "Đường dẫn đang tìm:\n" + path
                    );
                    return;
                }

                // ==============================
                // ĐỌC FILE XML
                // ==============================
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(path);

                // Lấy tất cả thẻ <book>
                XmlNodeList nodeList =
                    xmlDoc.SelectNodes("/catalog/book");

                StringBuilder result = new StringBuilder();

                // Duyệt từng cuốn sách
                foreach (XmlNode node in nodeList)
                {
                    // Đọc thuộc tính
                    string isbn =
                        node.Attributes["ISBN"].Value;

                    string year =
                        node.Attributes["yearpublished"].Value;

                    // Đọc các thẻ con
                    string author =
                        node.SelectSingleNode("author").InnerText;

                    string title =
                        node.SelectSingleNode("title").InnerText;

                    string price =
                        node.SelectSingleNode("price").InnerText;

                    // Ghép kết quả
                    result.AppendLine("ISBN: " + isbn);
                    result.AppendLine("Tên sách: " + title);
                    result.AppendLine("Tác giả: " + author);
                    result.AppendLine("Năm xuất bản: " + year);
                    result.AppendLine("Giá: " + price);

                    result.AppendLine(
                        "------------------------------"
                    );
                }

                // Hiển thị dữ liệu đọc được
                MessageBox.Show(
                    result.ToString(),
                    "Danh sách sách",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi đọc XML:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}