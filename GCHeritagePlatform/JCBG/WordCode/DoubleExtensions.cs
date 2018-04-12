// ***********************************************************************
// Assembly         : GasWebMap.Core
// Author           : liuyh
// Created          : 04-22-2013
//
// Last Modified By : liuyh
// Last Modified On : 04-22-2013
// ***********************************************************************
// <copyright file="DoubleExtensions.cs" company="Tecocity">
//     Copyright (c) Tecocity. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    ///     Class DoubleExtensions
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        ///     由弧度转换为角度
        /// </summary>
        /// <param name="radian">弧度值</param>
        /// <returns>角度格式为dd°mm′ss″.</returns>
        public static string ToAngle(this double radian)
        {
            double d = radian*180/Math.PI;
            var dd = (int) Math.Floor(d);
            //计算分
            d = (d - dd)*60;
            var mm = (int) Math.Floor(d);
            //计算秒
            d = (d - mm)*60;
            var ss = (int) Math.Floor(d);

            return string.Format("{0}°{1}′{2}″", dd, mm, ss);
        }

        /// <summary>
        /// 四舍五入 后，返回字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="r">保留的小数位</param>
        /// <returns></returns>
        public static string RoundString(this double s,int r=2)
        {
            return s.Round(r).ToString();
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="s"></param>
        /// <param name="r">保留的小数位</param>
        /// <returns></returns>
        public static  double Round(this double s, int r=2)
        {
            return Math.Round(s, r);
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fangcha">返回 方差</param>
        /// <returns>返回标准差</returns>
        public static double StandardDeviation(this IEnumerable<double> data, out double fangcha)
        {

            var count = data.Count();
            if (count <= 0)
            {
                fangcha = 0;
                return 0;
            }

            var avg = data.Average().Round(4);

            double mSum = data.Sum(t => Math.Pow(t - avg, 2));
            fangcha = (mSum / count).Round();
            return mSum==0?0: Math.Sqrt(mSum / count).Round();
        }


        /// <summary>
        /// 区分度
        /// </summary>
        /// <param name="data"></param>
        /// <param name="markSum">总分</param>
        /// <returns></returns>
        public static double DiscriminativePower(this IEnumerable<double> data,double markSum)
        {
           if (markSum<1)
           {
               return 0;
           }

            var count = data.Count();
            if (count <2)
            {
                return 0;
            }
            var maxc = (int) Math.Ceiling(count*0.27);
            var m = data.OrderBy(t => t).Take(maxc).Average().Round();
            var n = data.OrderByDescending(t => t).Take(maxc).Average().Round();

           double mm = (m - n)/markSum;


           return mm.Round();
        }
        public static double DiscriminativePower(this IEnumerable<int> data, int markSum)
        {
            if (markSum < 1)
            {
                return 0;
            }

            var count = data.Count();
            if (count < 2)
            {
                return 0;
            }
            var maxc = (int)Math.Ceiling(count * 0.27);
            var m = data.OrderBy(t => t).Take(maxc).Average().Round();
            var n = data.OrderByDescending(t => t).Take(maxc).Average().Round();

            double mm = (m - n) / markSum;


            return mm.Round();
        }

        /// <summary>
        /// 获得四级等地成绩
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="marksum"></param>
        /// <returns></returns>
        public static string MarkLevel4(this double mark, double marksum)
        {
            if (mark >= 0.85 * marksum)
            {
                return "优秀";
            }
            if (mark >= 0.75 * marksum)
            {
                return "良好";
            }

            if (mark >= 0.6 * marksum)
            {
                return "达标";
            }

            return "待达标";


        }

        /// <summary>
        /// 获得六级等地成绩
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="marksum"></param>
        /// <returns></returns>
        public static string MarkLevel6(this double mark, double marksum)
        {
            if (mark >=0.98* marksum)
            {
                return "非常优秀";
            }
            if (mark >= 0.95 * marksum)
            {
                return "很优秀";
            }

            if (mark >= 0.85 * marksum)
            {
                return "优秀";
            }
            if (mark >= 0.75 * marksum)
            {
                return "良好";
            }

            if (mark >= 0.6 * marksum)
            {
                return "达标";
            }

            return "待达标";


        }
    }
}