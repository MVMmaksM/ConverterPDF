﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterPDF.Services
{
    public class UnitePdfFilesServices : IUnitePdfFileServices
    {
        public void UnitePdfFiles(List<string> pathFiles, string outputPathFile)
        {
            var pdfDoc = new Document();

            using (FileStream fileStream = new FileStream(outputPathFile, FileMode.Create))
            {
                var pdfWriter = new PdfCopy(pdfDoc, fileStream);
                if (pdfWriter == null)
                {
                    return;
                }

                pdfDoc.Open();

                foreach (string fileName in pathFiles)
                {
                    var pdfReader = new PdfReader(fileName);
                    pdfReader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                    {
                        var page = pdfWriter.GetImportedPage(pdfReader, i);
                        pdfWriter.AddPage(page);
                    }

                    pdfReader.Close();
                }

                pdfWriter.Close();
                pdfDoc.Close();
            }
        }
    }
}
