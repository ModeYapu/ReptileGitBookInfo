using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.IO;

namespace Spire
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now-startTime).TotalMilliseconds; // 相差毫秒数
            Console.WriteLine(timeStamp);
            DirectoryDetail();
            Console.ReadKey();
        }
        private static void ToWord()
        {
            //Configure path.
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = desktopPath + @"\test.docx";
            string picPath = desktopPath + @"\wang.jpg";
            //Create a word document.
            Document doc = new Document();
            //Add a section.
            Section section = doc.AddSection();
            //Add a paragraph.
            Paragraph paragraph = section.AddParagraph();
            paragraph.AppendText("Spire is me.");
            //Add a comment.
            string content = "CNblog:http://www.cnblogs.com/LanTianYou/";
            Comment comment = paragraph.AppendComment(content);
            comment.Format.Author = "Tylan";
            //Set font style for the paragraph.
            ParagraphStyle style = new ParagraphStyle(doc);
            style.Name = "TylanFontStyle";
            style.CharacterFormat.FontName = "Batang";
            style.CharacterFormat.FontSize = 36;
            style.CharacterFormat.TextColor = Color.Green;
            doc.Styles.Add(style);
            paragraph.ApplyStyle(style.Name);
            //Insert a picture.
            DocPicture pic = paragraph.AppendPicture(Image.FromFile(picPath));
            pic.Width = 500;
            pic.Height = 500;
            //Add header.
            HeaderFooter header = doc.Sections[0].HeadersFooters.Header;
            Paragraph headerParagraph = header.AddParagraph();
            TextRange headerText = headerParagraph.AppendText("Spire header");
            headerText.CharacterFormat.FontSize = 18;
            headerText.CharacterFormat.TextColor = Color.Tomato;
            headerParagraph.Format.Borders.Bottom.BorderType = BorderStyle.ThinThinSmallGap;
            headerParagraph.Format.Borders.Bottom.Space = 0.15f;
            headerParagraph.Format.Borders.Color = Color.DarkGray;
            //Add footer.
            HeaderFooter footer = doc.Sections[0].HeadersFooters.Footer;
            Paragraph footerParagraph = footer.AddParagraph();
            TextRange footerText = footerParagraph.AppendText("Spire footer");
            footerText.CharacterFormat.FontSize = 18;
            footerText.CharacterFormat.TextColor = Color.Tomato;
            footerParagraph.Format.Borders.Top.BorderType = BorderStyle.ThinThinSmallGap;
            footerParagraph.Format.Borders.Top.Space = 0.15f;
            footerParagraph.Format.Borders.Color = Color.DarkGray;
            //Save the file.
            doc.SaveToFile(filePath, FileFormat.Docx);
        }
        private static void ToExcel()
        {

        }
        private static void DirectoryDetail()
        {
            string path = @"C:\Users\qsc\Desktop\MyDir";
            string target = @"C:\Users\qsc\Desktop\TestDir";

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(path);
                }

                if (Directory.Exists(target))
                {
                    // Delete the target to ensure it is not there.
                    Directory.Delete(target, true);
                }

                // Move the directory.
                Directory.Move(path, target);

                // Create a file in the directory.
                File.CreateText(target + @"\myfile.txt");

                // Count the files in the target directory.
                Console.WriteLine("The number of files in {0} is {1}",
                    target, Directory.GetFiles(target).Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
    }
}
