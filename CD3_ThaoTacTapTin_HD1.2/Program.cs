using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace CD3_ThaoTacTapTin_HD1._2
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string path = Path.Combine(Application.StartupPath, "books.xml");

            if (!File.Exists(path))
            {
                MessageBox.Show("Không tìm thấy file:\n" + path);
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList nodeList =
                xmlDoc.DocumentElement.SelectNodes("/catalog/book");

            foreach (XmlNode node in nodeList)
            {
                string isbn = node.Attributes["ISBN"].Value;
                string title = node.SelectSingleNode("title").InnerText;
                string price = node.SelectSingleNode("price").InnerText;

                string firstName =
                    node.SelectSingleNode("author/first-name").InnerText;

                string lastName =
                    node.SelectSingleNode("author/last-name").InnerText;

                MessageBox.Show(
                    $"ISBN: {isbn}\n" +
                    $"Tên sách: {title}\n" +
                    $"Tác giả: {firstName} {lastName}\n" +
                    $"Giá: {price}"
                );
            }
        }
    }
}