using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using OfficeManagement.Models;

namespace OfficeManagement
{
    public class ConvertToPdf
    {
        public void PdfConverter(List<FormModel> leaveList)
        {
            Document doc = new Document();
            string path = @"C:\\Users\ith\Downloads\" + leaveList[0].CreatedNameOfUser + "_LeaveRecord.pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            string imageUrl=leaveList[0].userImage;
            imageUrl=imageUrl.Trim(new Char[] { '~' });
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(@"C:\Users\ith\Documents\Visual Studio 2013\Projects\OfficeManagement\OfficeManagement" + imageUrl);
            image.ScaleAbsolute(80f, 80f);
            image.Alignment = Image.ALIGN_RIGHT;
            PdfPTable tblPersonalDetail = new PdfPTable(2);
            tblPersonalDetail.TotalWidth = 400f;
            float[] widths = new float[] { 0.47f, 2f };
            tblPersonalDetail.SetWidths(widths);
           // tblPersonalDetail.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cell = new PdfPCell( image, false);
            cell.Rowspan = 3;
            tblPersonalDetail.AddCell(cell);
            tblPersonalDetail.AddCell("Created Date");
            tblPersonalDetail.AddCell("Subject");
            tblPersonalDetail.AddCell("Description");
            doc.Add(tblPersonalDetail);
           
            
            //doc.Add(image);
            Paragraph name = new Paragraph(leaveList[0].CreatedNameOfUser + "\n\n");
            doc.Add(name);
            int length = leaveList.Count();
            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = 400f;
            table.LockedWidth = true;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            table.AddCell("Created Date");
            table.AddCell("Subject");
            table.AddCell("Description");
            foreach (var a in leaveList)
            {
                table.AddCell(a.CreatedDate.ToString());
                table.AddCell(a.Name);
                table.AddCell(a.Description);
            }
           
            
            doc.Add(table);
            doc.Close();

            //Document doc = new Document();
            //string path = @"C:\\Users\ith\Downloads\" + leaveList[0].CreatedNameOfUser + "_LeaveRecord.pdf";
            //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            //doc.Open();

            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(@"C:\Users\ith\Documents\Visual Studio 2013\Projects\OfficeManagement\OfficeManagement\images\UserImage\aarati.jpg");
            //doc.Add(image);
            //Paragraph name = new Paragraph(leaveList[0].CreatedNameOfUser + "\n\n");
            //doc.Add(name);
            //foreach (var a in leaveList)
            //{
            //    Paragraph createdDate = new Paragraph("Created Date:" + a.CreatedDate);
            //    Paragraph subject = new Paragraph("Subject:" + a.Name);
            //    Paragraph description = new Paragraph("Description:" + a.Description + "\n");
            //    doc.Add(createdDate);
            //    doc.Add(subject);
            //    doc.Add(description);
            //}

            //doc.Close();
        }
    }
}