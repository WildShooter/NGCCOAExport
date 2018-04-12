﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using System.Drawing;

namespace GCHeritagePlatform.JCBG.WordCode
{
    public class ExportWord
    {
        public string templateFile { get; set; }
        public string saveFilePath { get; set; }

        public ExportWord()
        {
            //aspose.Words注册
            string licensePath = AppDomain.CurrentDomain.BaseDirectory + @"bin\Aspose.Total.Product.Family.lic";
            License L = new License();
            L.SetLicense(licensePath);
            saveFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\upload\word\";
        }

        public string FillWordData(DataTable dt, string rebookName)
        {
            string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
            fileName = this.saveFilePath + "\\" + fileName;
            Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            DataTable nameList = dt;
            List<double> widthList = new List<double>();
            for (int i = 0; i < nameList.Columns.Count; i++)
            {
                builder.MoveToCell(0, 0, i, 0); //移动单元格
                double width = builder.CellFormat.Width; //获取单元格宽度
                widthList.Add(width);
            }
            builder.StartTable();
            for (var i = 0; i < nameList.Rows.Count; i++)
            {
                for (var j = 0; j < nameList.Columns.Count; j++)
                {
                    builder.InsertCell(); // 添加一个单元格                    
                    builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                    builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                    builder.CellFormat.Width = widthList[j];
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center; //垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center; //水平居中对齐
                    builder.Write(nameList.Rows[i][j].ToString());
                }
                builder.EndRow();
            }
            builder.EndTable();
            doc.Save(fileName);
            return "";
        }
    }
}
