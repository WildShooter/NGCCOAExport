using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using Bookmark = Aspose.Words.Bookmark;
using Cell = Aspose.Words.Tables.Cell;
using DataTable = System.Data.DataTable;
using License = Aspose.Words.License;
using Table = Aspose.Words.Tables.Table;
using FrameworkCore.Utils;

//12312
namespace GCHeritagePlatform.JCBG.WordCode
{
    public class ColorCell
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
    }

    public class MCell
    {
        public MCell()
        {
        }

        public MCell(bool IsColumn, int C2RIndex, int fromR2C, int toR2C)
        {
            this.IsColumn = IsColumn;
            this.C2RIndex = C2RIndex;
            this.fromR2C = fromR2C;
            this.toR2C = toR2C;
        }

        /// <summary>
        /// 从哪一行或者哪一列开始
        /// </summary>
        public int fromR2C { get; set; }
        /// <summary>
        /// 从到一行或者到一列结束
        /// </summary>
        public int toR2C { get; set; }

        /// <summary>
        /// 某行 或者 某列
        /// </summary>
        public int C2RIndex { get; set; }

        /// <summary>
        /// 是列不
        /// </summary>
        public bool IsColumn { get; set; }
    }

    public class BaseWord
    {
        public string WordTemplate { get; set; }
        public Aspose.Words.Document doc = null;
        public Aspose.Words.DocumentBuilder builder = null;

        public string SavePath { get; set; }

        public void Init()
        {
            string licensePath = AppDomain.CurrentDomain.BaseDirectory + @"bin\Aspose.Total.Product.Family.lic";
            License L = new License();
            L.SetLicense(licensePath);
        }
        /// <summary>
        /// 构造函数初始化，初始化许可、保存路径、临时文档（模板）、
        /// </summary>
        /// <param name="tempdoc"></param>
        public BaseWord(string tempdoc = "附件2-收文登记--带书签")
        {
            //this.Init();
            string fileName = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".docx";
            this.SavePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            this.WordTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JCBG","WordModel",tempdoc+".docx");

            this.doc = new Aspose.Words.Document(this.WordTemplate);
            this.builder = new Aspose.Words.DocumentBuilder(doc);
        }
        /// <summary>
        /// 根据xml获取路径
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            return ConfigurationManager.AppSettings["ReportPath"];
            //return AppDomain.CurrentDomain.BaseDirectory + "\\ReportC";
        }
        /// <summary>
        /// 保存在ReportC目录下的文档
        /// </summary>
        /// <param name="saveName"></param>
        public void Save(string saveName)
        {
            var dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JCBG","WordExport");
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            //删除文件
            try
            {
                DirectoryInfo di = new DirectoryInfo(dic);
                FileInfo[] files = di.GetFiles();

                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                SystemLogger.getLogger().Error(ex.ToString());
                throw ex;
            }

            string fileName = saveName + ".doc";
            this.SavePath = Path.Combine(dic, fileName);
            
            builder.Document.Save(this.SavePath);
            this.SavePath = $@"JCBG/{fileName}";
        }
        /// <summary>
        /// 保存在ReportC目录下，再创建子目录，文档保存在子目录下
        /// </summary>
        /// <param name="subfolder"></param>
        /// <param name="saveName"></param>
        public void Save(string subfolder, string saveName)
        {
            var dic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportC", subfolder);
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            string fileName = saveName + ".doc";
            this.SavePath = dic + "\\" + fileName;
            builder.Document.Save(this.SavePath);
        }
        /// <summary>
        /// 关闭builder和doc，释放内存
        /// </summary>
        public void Close()
        {
            this.builder = null;
            this.doc = null;
            GC.Collect();
        }


        public void AddTablesOfContents()
        {
            builder.MoveToBookmark("Remark");
            builder.InsertTableOfContents("\\o \"1-3\" \\h \\z \\u");
            this.doc.UpdateFields();

            // object fileName = this.SavePath;
            // Object oMissing = System.Reflection.Missing.Value;
            // Object oTrue = true;
            // Object oFalse = false;

            // if (oWord == null)
            // {
            //     oWord = new Microsoft.Office.Interop.Word.Application();
            //     oWord.Visible = false;
            // }else
            // {

            // }
            // //Microsoft.Office.Interop.Word.Application oWord = new Microsoft.Office.Interop.Word.Application();


            // word.Document doc = null;
            // try
            // {
            //     doc = oWord.Documents.Open(ref fileName,
            // ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            // ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            // ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // }
            // catch (Exception exx)
            // {
            //     oWord = new Microsoft.Office.Interop.Word.Application();
            //     oWord.Visible = false;
            //     doc = oWord.Documents.Open(ref fileName,
            //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            // }

            // //---------------------------------------------------------------------------------------------------------------------  
            // oWord.Selection.Paragraphs.OutlineLevel = word.WdOutlineLevel.wdOutlineLevel2;
            // oWord.Selection.Paragraphs.OutlineLevel = word.WdOutlineLevel.wdOutlineLevel3;
            // oWord.Selection.Paragraphs.OutlineLevel = word.WdOutlineLevel.wdOutlineLevelBodyText;

            // object x = 0;
            // word.Range bookRange = null;
            // foreach (word.Bookmark bookmark in doc.Bookmarks)
            // {
            //     if (bookmark.Name == "Remark")
            //     {
            //         bookRange = bookmark.Range;
            //         break;
            //     }
            // }

            // Object oUpperHeadingLevel = "1";
            // Object oLowerHeadingLevel = "2";
            // Object oTOCTableID = "TableOfContents";
            // doc.TablesOfContents.Add(bookRange, ref oTrue, ref oUpperHeadingLevel,
            //     ref oLowerHeadingLevel, ref oMissing, ref oTOCTableID, ref oTrue,
            //     ref oTrue, ref oMissing, ref oTrue, ref oTrue, ref oTrue);
            // doc.Save();
            // doc.Close();

        }
    }

    public static class BusinessWord
    {
        /// <summary>
        /// 第一种字写法 例如 一、分组学校名单
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="text"></param>
        public static void WriteHead1(Aspose.Words.DocumentBuilder builder, string text)
        {
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.Heading1;
            builder.Write(text);
            // builder.AAWriteText("一、分组学校名单", 16, true);
            builder.InsertBreak(BreakType.ParagraphBreak);
        }

        /// <summary>
        /// 第二种字写法 例如 （一）成绩统计
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="text"></param>
        public static void WriteHead2(Aspose.Words.DocumentBuilder builder, string text)
        {
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.Heading2;
            builder.Write(text);
            // builder.AAWriteText("一、分组学校名单", 16, true);
            builder.InsertBreak(BreakType.ParagraphBreak);
        }
        public static void WriteHead3(Aspose.Words.DocumentBuilder builder, string text)
        {
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.Heading3;
            builder.Write(text);
            // builder.AAWriteText("一、分组学校名单", 16, true);
            builder.InsertBreak(BreakType.ParagraphBreak);
        }
        /// <summary>
        /// 第三种字写法 例如 表2. XXXX中学考试成绩统计
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="text"></param>
        public static void WriteBodyText(Aspose.Words.DocumentBuilder builder, string text)
        {
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            builder.ParagraphFormat.StyleName = "正文";
            builder.Write(text);
            // builder.AAWriteText("一、分组学校名单", 16, true);
            builder.InsertBreak(BreakType.ParagraphBreak);
        }
        /// <summary>
        /// 写表头标注
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="text"></param>
        public static void WriteTableMark(Aspose.Words.DocumentBuilder builder, string text)
        {
            if (text == null)
            {
                text = "";
            }
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Write(text);
            // builder.AAWriteText("一、分组学校名单", 16, true);
            builder.InsertBreak(BreakType.ParagraphBreak);
        }

        /// <summary>
        /// 写表格下面图片
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        public static void WriteImage(Aspose.Words.DocumentBuilder builder, string path)
        {
            string image = path;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                var w = ConfigurationManager.AppSettings["chartWidth1"].ToInt();
                var h = ConfigurationManager.AppSettings["chartHeight1"].ToInt();
                builder.AAInsertImage(image, w, h, 0, 0);
                builder.InsertBreak(BreakType.ParagraphBreak);
            }

        }
        /// <summary>
        /// 写表格下面图片
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        public static void WriteImage(Aspose.Words.DocumentBuilder builder, string path, double width, double height)
        {
            string image = path;
            if (File.Exists(path))
            {
                builder.AAInsertImage(image, width, height, 0, 0);
                builder.InsertBreak(BreakType.ParagraphBreak);
            }

        }
        /// <summary>
        /// 写表格 无合并
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="colWidthList"></param>
        /// <param name="rHeignt"></param>
        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> colWidthList, double rHeignt = 25)
        {
            var hList = new List<double>() { rHeignt };
            return WriteTable(builder, dt, hList, colWidthList, new List<MCell>());
        }
        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> colWidthList, IList<MCell> mCelllist, double rHeignt = 25)
        {
            var hList = new List<double>() { rHeignt };
            return WriteTable(builder, dt, hList, colWidthList, mCelllist);
        }
        /// <summary>
        /// 写表格 列宽 行高
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="rowHeightList"></param>
        /// <param name="colWidthList"></param>
        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> rowHeightList, List<double> colWidthList)
        {
            return WriteTable(builder, dt, rowHeightList, colWidthList, new List<MCell>());
        }

        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, int startRow,
            IList<double> colwidth)
        {

            var tableW = ConfigurationManager.AppSettings["tableWidth"].ToInt();
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            //table.AllowAutoFit = false;
            builder.RowFormat.Height = 25.0;
            builder.CellFormat.FitText = false;

            var lstW = new List<double>();
            var ww = colwidth.Sum();
            foreach (var v in colwidth)
            {
                lstW.Add((tableW * v / ww).Round());
            }

            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(191, 191, 191);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 11;
            builder.Font.Name = "宋体";

            for (int i = 0; i < dt.Columns.Count; i++)
            {

                builder.InsertCell();
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPoints(lstW[i]);
                builder.Write(dt.Columns[i].ColumnName);
            }

            builder.EndRow();
            builder.Font.Size = 10.5;
            builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            for (int j = startRow; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    builder.InsertCell();
                    //builder.CellFormat.Width = lstW[i];
                    builder.CellFormat.PreferredWidth = PreferredWidth.FromPoints(lstW[i]);
                    //builder.CellFormat.WrapText = true ;
                    // builder.CellFormat.FitText = false;
                    builder.Write(dt.Rows[j][i].ToString().Trim());
                }

                builder.EndRow();
            }
            builder.EndTable();
            table.AllowAutoFit = false;
            //table.AutoFit(AutoFitBehavior.FixedColumnWidths);
            //MergeCell(builder, table, mcell);
            return table;

        }

        /// <summary>
        /// 写表格 列宽 行高
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="rowHeightList"></param>
        /// <param name="colWidthList"></param>
        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, int startRow = 1, params int[] colIndexs)
        {
            var tableW = ConfigurationManager.AppSettings["tableWidth"].ToInt();
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            builder.RowFormat.Height = 25.0;

            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(191, 191, 191);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 10.5;
            builder.Font.Name = "宋体";
            if (colIndexs.Length == 0)
            {
                var lst = new List<int>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    lst.Add(i);
                }
                colIndexs = lst.ToArray();
            }
            //表宽除以列数=列宽
            var cellW = ((1.00 * tableW) / colIndexs.Length).Round(0);
            builder.CellFormat.Width = cellW;
            builder.CellFormat.LeftPadding = 0;

            foreach (var i in colIndexs)
            {
                var cell = builder.InsertCell();

                builder.Write(dt.Columns[i].ColumnName);
            }

            builder.EndRow();
            builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            for (int j = startRow; j < dt.Rows.Count; j++)
            {
                foreach (var i in colIndexs)
                {

                    builder.InsertCell();
                    builder.Write(dt.Rows[j][i].ToString().Trim());

                }

                builder.EndRow();
            }
            builder.EndTable();

            //MergeCell(builder, table, mcell);
            return table;

        }
        /// <summary>
        /// 写表格 列宽 行高 有合并
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="rowHeightList"></param>
        /// <param name="colWidthList"></param>
        /// <param name="mcell"></param>
        public static Table WriteTable(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> rowHeightList, List<double> colWidthList, IList<MCell> mcell)
        {
            ;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            builder.RowFormat.Height = 25.0;
            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(191, 191, 191);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 10.5;
            builder.Font.Name = "宋体";
            builder.Font.Bold = true;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                builder.CellFormat.Width = colWidthList[i];
                builder.InsertCell();
                builder.Write(dt.Columns[i].ColumnName);
            }
            builder.EndRow();
            builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
            builder.Font.Bold = false;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            var cellM = mcell.FirstOrDefault(e => e.IsColumn);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    builder.CellFormat.Width = colWidthList[i];
                    builder.InsertCell();
                    builder.Write(dt.Rows[j][i].ToString().Trim());

                }
                builder.EndRow();
            }
            builder.EndTable();
            MergeCell(builder, table, mcell);
            return table;
        }
        /// <summary>
        /// 插入的表格中不包括dt的表头，builder、dt、列宽3个参数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="colWidthList"></param>
        /// <param name="rHeignt"></param>
        /// <returns></returns>
        public static Table WriteTableEx(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> colWidthList, double rHeignt = 25)
        {
            var hList = new List<double>() { rHeignt };
            return WriteTableEx(builder, dt, hList, colWidthList, new List<MCell>());
        }

        /// <summary>
        /// 插入的表格中不包括dt的表头的基方法
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="dt"></param>
        /// <param name="rowHeightList">行高</param>
        /// <param name="colWidthList">列宽</param>
        /// <param name="mcell">合并</param>
        /// <returns></returns>
        public static Table WriteTableEx(Aspose.Words.DocumentBuilder builder, DataTable dt, List<double> rowHeightList, List<double> colWidthList, IList<MCell> mcell)
        {
            ;
            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            builder.RowFormat.Height = 25.0;
            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 10.5;
            builder.Font.Name = "宋体";
            // builder.CellFormat.Width = 200;
            builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            var cellM = mcell.FirstOrDefault(e => e.IsColumn);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    builder.CellFormat.Width = colWidthList[i];
                    builder.InsertCell();
                    builder.Write(dt.Rows[j][i].ToString().Trim());

                }
                builder.EndRow();
            }
            builder.EndTable();
            return table;
        }
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="table"></param>
        /// <param name="mcell"></param>
        public static void MergeCell(Aspose.Words.DocumentBuilder builder, Table table, IList<MCell> mcell)
        {
            try
            {


                var rCount = table.Rows.Count;
                var cCount = table.Rows[0].Cells.Count;
                for (int i = 0; i < mcell.Count; i++)
                {
                    var m = mcell[i];
                    if (m.IsColumn)
                    {
                        //某一列进行合并
                        //到达的行大于总行数、或者起始的某一列所以大于总列数
                        if (m.toR2C > rCount || cCount < m.C2RIndex) continue;
                        //开始位置，起始行的开始列
                        Cell sCell = table.Rows[m.fromR2C].Cells[m.C2RIndex];
                        //结束位置，结束行的开始列
                        Cell eCell = table.Rows[m.toR2C].Cells[m.C2RIndex];
                        builder.AAMergeCells(sCell, eCell);
                    }
                    else
                    {
                        //某一行进行合并
                        //到达列大于总列数或者起始的某一行大于总行数
                        if (m.toR2C > cCount || rCount < m.C2RIndex) continue;
                        Cell sCell = table.Rows[m.C2RIndex].Cells[m.fromR2C];
                        Cell eCell = table.Rows[m.C2RIndex].Cells[m.toR2C];
                        builder.AAMergeCells(sCell, eCell);

                    }


                }
            }
            catch (Exception ex)
            {

            }
        }



        public static Table WriteHeadGroupTable(Aspose.Words.DocumentBuilder builder, DataTable dt, IList<MCell> mcell, int StartRow, IList<int> colIndexs = null, double firstColWidth = 0.0)
        {
            var cols = colIndexs == null ? dt.Columns.Count : colIndexs.Count;
            var dw = ConfigurationManager.AppSettings["tableWidth"].ToInt();
            var colw = ((1.00 * dw) / cols).Round(0);
            if (firstColWidth > 0)
            {
                colw = ((1.00 * dw - firstColWidth) / (cols - 1)).Round(0);
            }
            else
            {
                firstColWidth = colw;
            }
            builder.CellFormat.Width = colw;

            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            builder.RowFormat.Height = 25.0;
            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(191, 191, 191);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 10.5;
            builder.Font.Name = "宋体";
            for (int i = 0; i < StartRow; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (colIndexs != null)
                    {
                        if (!colIndexs.Contains(j)) continue;
                        if (colIndexs[0] == j)
                        {
                            builder.CellFormat.Width = firstColWidth;
                        }
                        else
                        {
                            builder.CellFormat.Width = colw;
                        }
                    }

                    var cell = builder.InsertCell();
                    builder.Write(dt.Rows[i][j].ToString().Trim());
                }
                builder.EndRow();
            }

            builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            var cellM = mcell.FirstOrDefault(e => e.IsColumn);
            for (int j = StartRow; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (colIndexs != null)
                    {
                        if (!colIndexs.Contains(i)) continue;
                    }
                    builder.InsertCell();
                    builder.Write(dt.Rows[j][i].ToString());

                }
                builder.EndRow();
            }
            builder.EndTable();
            MergeCell(builder, table, mcell);
            return table;
        }


        public static Table WriteHeadGroupTable(Aspose.Words.DocumentBuilder builder, DataTable dt, IList<MCell> mcell, int StartRow, IList<ColorCell> listCell)
        {

            builder.ParagraphFormat.StyleIdentifier = Aspose.Words.StyleIdentifier.BodyText;
            var table = builder.StartTable();
            //table.LeftIndent = 20.0;
            builder.RowFormat.Height = 25.0;
            builder.RowFormat.HeightRule = HeightRule.Auto;// HeightRule.AtLeast
            builder.CellFormat.Shading.BackgroundPatternColor = Color.FromArgb(191, 191, 191);
            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;

            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.Font.Size = 10.5;
            builder.Font.Name = "宋体";
            for (int i = 0; i < StartRow; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    builder.InsertCell();
                    builder.Write(dt.Rows[i][j].ToString());
                }
                builder.EndRow();
            }


            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            var cellM = mcell.FirstOrDefault(e => e.IsColumn);
            for (int j = StartRow; j < dt.Rows.Count; j++)
            {
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var cell = builder.InsertCell();
                    var rowColor = listCell.FirstOrDefault(e => e.ColumnIndex == i && e.RowIndex == j);
                    if (rowColor != null)
                    {
                        cell.CellFormat.Shading.BackgroundPatternColor = Color.Yellow;
                    }
                    else
                    {
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;
                    }
                    builder.Write(dt.Rows[j][i].ToString());

                }
                builder.EndRow();
            }
            builder.EndTable();
            MergeCell(builder, table, mcell);
            return table;
        }

        public static void ReplaceBookmark(Document doc, string bookmarkName, string newbookmarkcontent)
        {
            var bookmark = doc.Range.Bookmarks[bookmarkName];
            bookmark.Text = "";
            
            bookmark.Text = newbookmarkcontent;
        }
    }
}
