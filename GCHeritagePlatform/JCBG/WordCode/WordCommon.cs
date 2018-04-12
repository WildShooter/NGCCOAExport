using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

namespace GCHeritagePlatform.JCBG.WordCode
{
    /// <summary>
    /// 自定义 方法全部以AA开头 方便找到
    /// </summary>
   public static class WordCommon
    {
        //字号‘八号’对应磅值5
        //字号‘七号’对应磅值5.5
        //字号‘小六’对应磅值6.5
        //字号‘六号’对应磅值7.5
        //字号‘小五’对应磅值9
        //字号‘五号’对应磅值10.5
        //字号‘小四’对应磅值12
        //字号‘四号’对应磅值14
        //字号‘小三’对应磅值15
        //字号‘三号’对应磅值16
        //字号‘小二’对应磅值18
        //字号‘二号’对应磅值22
        //字号‘小一’对应磅值24
        //字号‘一号’对应磅值26
        //字号‘小初’对应磅值36
        //字号‘初号’对应磅值42
       /// <summary>
       /// 插入字体
       /// </summary>
       /// <param name="bulid"></param>
       /// <param name="text"></param>
       public static void AAWriteText(this DocumentBuilder bulider, string text, double fontSize=10.5,bool isBold=false)
       {
           bulider.Bold = false;
           bulider.Font.Name = "宋体";
           bulider.Font.Size = fontSize; 
           bulider.Write(text);
       }

       public static void AAWriteText(this DocumentBuilder oWordApplic, string strText, double conSize, string conAlign, bool conBold = false)
       {
           oWordApplic.Bold = conBold;
           oWordApplic.Font.Size = conSize;
           switch (conAlign)
           {
               case "left":
                   oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                   break;
               case "center":
                   oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                   break;
               case "right":
                   oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Right;
                   break;
               default:
                   oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                   break;
           }
           oWordApplic.Writeln(strText);

       }  
       /// <summary>
       /// 插入字体的时候可以设置对齐方式
       /// </summary>
       /// <param name="bulider"></param>
       /// <param name="type"></param>
       public static void AAWriteTextAlignment(this DocumentBuilder bulider, ParagraphAlignment type)
       {
           bulider.ParagraphFormat.Alignment = type; 
       }
       /// <summary>  
       /// 换行  
       /// </summary>  
       public static void AALineBreak(this DocumentBuilder bulider, int lineCount = 1)
       {
           for (int i = 0; i < lineCount; i++)
           {
               bulider.InsertBreak(BreakType.LineBreak);
           }
       }

        public static void AAInsertImage1(this DocumentBuilder bulider, string path)
        {
            bulider.InsertImage(path);
        }

       /// <summary>
       /// 插入图片
       /// </summary>
       /// <param name="bulider"></param>
       /// <param name="path">路径</param>
       /// <param name="width">宽</param>
       /// <param name="height">高</param>
       /// <param name="hLeft">水平方向的距离左上角</param>
       /// <param name="vTop">垂直方向的距离中心</param>
        public static void AAInsertImage(this DocumentBuilder bulider,string path,double width,double height,double hLeft,double vTop)
        {
            if (!File.Exists(path))
            {
                return;
            }
            bulider.InsertImage(path, RelativeHorizontalPosition.Margin, hLeft, RelativeVerticalPosition.Margin, vTop, width, height, WrapType.Inline);
        }

       /// <summary>
       /// 合并表格
       /// </summary>
       /// <param name="startCell"></param>
       /// <param name="endCell"></param>
        public static void AAMergeCells(this DocumentBuilder builder,Cell startCell, Cell endCell)
        {
            Table parentTable = startCell.ParentRow.ParentTable;
            Point startCellPos = new Point(startCell.ParentRow.IndexOf(startCell), parentTable.IndexOf(startCell.ParentRow));
            Point endCellPos = new Point(endCell.ParentRow.IndexOf(endCell), parentTable.IndexOf(endCell.ParentRow));
            Rectangle mergeRange = new Rectangle(Math.Min(startCellPos.X, endCellPos.X), Math.Min(startCellPos.Y, endCellPos.Y),
                Math.Abs(endCellPos.X - startCellPos.X) + 1, Math.Abs(endCellPos.Y - startCellPos.Y) + 1);

            foreach (Row row in parentTable.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    Point currentPos = new Point(row.IndexOf(cell), parentTable.IndexOf(row));
                    if (mergeRange.Contains(currentPos))
                    {
                        if (currentPos.X == mergeRange.X)
                            cell.CellFormat.HorizontalMerge = CellMerge.First;
                        else
                            cell.CellFormat.HorizontalMerge = CellMerge.Previous;

                        if (currentPos.Y == mergeRange.Y)
                            cell.CellFormat.VerticalMerge = CellMerge.First;
                        else
                            cell.CellFormat.VerticalMerge = CellMerge.Previous;
                    }
                }
            }
        }
    }
}
