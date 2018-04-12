using Aspose.Words;
using Aspose.Words.Tables;
using FrameworkCore.Utils;
using GCHeritagePlatform.JCBG.WordCode;
using GCHeritagePlatform.Models;
using GCHeritagePlatform.Utils;
using Hprose.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GCHeritagePlatform.Services
{
    public class MarkNode
    {
        public MarkNode() { }
        public MarkNode(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// 类别
        /// </summary>
        public int MType { get; set; }
        /// <summary>
        /// 原始书签的名字
        /// </summary>
        public string BookMarkText { get; set; }
        public string SourceBookMarkName { get; set; }
        /// <summary>
        /// 备用名字，用于对象+对象的时候，第一个对象用Name，第二个对象用ObjName
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List集合的名字
        /// </summary>
        public string ListName { get; set; }
        /// <summary>
        /// 对象的名字
        /// </summary>
        public string ObjName { get; set; }
        /// <summary>
        /// 无条件时对应集合的索引
        /// </summary>
        public string IndexNum { get; set; }
        /// <summary>
        /// 所需要的值的名字
        /// </summary>
        public string ValueName { get; set; }
        public string ValueNameAdd1 { get; set; }
        public string ValueNameAdd2 { get; set; }
        /// <summary>
        /// 书签名字为了不重复的唯一标识，：之后的值
        /// </summary>
        public string OnlySign { get; set; }
        public List<string> SerializableNames { get; set; }
        /// <summary>
        /// 需要插入的行的标题的行所对应的索引
        /// </summary>
        public string TitleIndex { get; set; }
        /// <summary>
        /// 默认存在的空白行的行数
        /// </summary>
        public string DefaultRowsCount { get; set; }
        /// <summary>
        /// 存放要插入行对应的列的名字，中间以
        /// </summary>
        public string ListColNamesStr { get; set; }
        /// <summary>
        /// 有些相关的字段需要通过ID调用其他服务获取，这里存放名字
        /// </summary>
        public string MethodNameForValue { get; set; }
        /// <summary>
        /// 调用其他服务需要返回的字段名
        /// </summary>
        public string ReturnFieldName { get; set; }
        public string TrueIsMatchValue { get; set; }
        public string FalseIsMatchValue { get; set; }
    }
    public class ExportConfig
    {
        public ExportConfig(string modelName, string serviceName, string listColsStr = "")
        {
            this.MethodName = serviceName;
            try
            {
                this.baseWord = GetWordFile(modelName);
                this.BookMarkCollection = this.baseWord.doc.Range.Bookmarks;
                this.ListColNameStr = listColsStr;
            }
            catch (Exception e)
            {
                SystemLogger.getLogger().Error($"未能找到对应的模板文件，文件名为{modelName}");
            }

        }

        /// <summary>
        /// 获得word中的所有书签
        /// </summary>
        /// <param name="modelName">word模板名称</param>
        /// <returns></returns>
        public BaseWord GetWordFile(string modelName)
        {
            //开始插入
            var baseWord = new GCHeritagePlatform.JCBG.WordCode.BaseWord(modelName);
            return baseWord;
        }
        /// <summary>
        /// 存储word中所有书签的属性
        /// </summary>
        public BookmarkCollection BookMarkCollection { get; set; }
        /// <summary>
        /// 存储模板这个word文档
        /// </summary>
        public BaseWord baseWord { get; set; }
        /// <summary>
        /// 存储服务方法的名称
        /// </summary>
        public string MethodName { get; set; }
        public string ListColNameStr { get; set; }
    }
    public class BookMarkBase
    {
        public List<MarkNode> MarkNodes { get; set; }
        public string MethodName { get; set; }
        public BookMarkBase() { }
        public BookMarkBase(string methodName)
        {
            this.MarkNodes = new List<MarkNode>();
            this.MethodName = methodName;
        }
        /// <summary>
        /// 根据书签集合获取书签的规则和特征
        /// </summary>
        /// <param name="marks">书签集合</param>
        public void GetBookMarkRules(BookmarkCollection marks)
        {
            //var markNodeList = new List<MarkNode>();
            foreach (Bookmark mark in marks)
            {
                var markNode = new MarkNode();
                if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+……[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//object￥Object：valueName……index（√的代码）
                    markNode.SerializableNames = mark.Name.Split(new string[] { "￥", "：", "……" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.Name = markNode.SerializableNames[0];
                    markNode.ObjName = markNode.SerializableNames[1];
                    markNode.ValueName = markNode.SerializableNames[2];
                    markNode.IndexNum = markNode.SerializableNames[3];
                    markNode.MType = 14;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+！[a-zA-Z0-9\u4e00-\u9fa5]+￥[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//PXJH{PXJHSHList[{SHJB==部门领导审核}]}  Object￥List！Object￥value  PXJH￥PXJHSHList！SHR￥REALNAME
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！", "￥", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.Name = markNode.SerializableNames[0];
                    markNode.ListName = markNode.SerializableNames[1];
                    markNode.ObjName = markNode.SerializableNames[2];
                    markNode.ValueName = markNode.SerializableNames[3];
                    markNode.MType = 13;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+！[a-zA-Z0-9\u4e00-\u9fa5]+……[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//因为7包含12，所以，先过滤7，再过滤12，
                    //分层表示一个obj￥list！value……索引---TXRY[0]XM
                    markNode.SerializableNames = mark.Name.Split(new string[] { "￥", "！", "……" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.Name = markNode.SerializableNames[0];
                    markNode.ListName = markNode.SerializableNames[1];
                    markNode.ValueName = markNode.SerializableNames[2];
                    markNode.IndexNum = markNode.SerializableNames[3];
                    markNode.MType = 7;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+……[a-zA-Z0-9\u4e00-\u9fa5]+（[a-zA-Z0-9\u4e00-\u9fa5]+）+$") || Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+……[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//ListName!ValueName……Index 表示集合中的第几个对象的哪个值  List！ValueName……Index（returnFieldName）第二种情况也是远程调用服务
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！", "……", "（", "）" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.IndexNum = markNode.SerializableNames[2];
                    if (markNode.SerializableNames.Count > 3)
                    {
                        markNode.MethodNameForValue = mark.Text;
                        markNode.ReturnFieldName = markNode.SerializableNames[3];
                    }
                    markNode.MType = 12;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+、[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//ObjectName……01    其中01分别代表true、false的对应字段
                    markNode.SerializableNames = Regex.Split(mark.Name, "、", RegexOptions.IgnoreCase).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.ObjName = markNode.SerializableNames[0];
                    markNode.TrueIsMatchValue = markNode.SerializableNames[1].Substring(0, 1);
                    markNode.FalseIsMatchValue = markNode.SerializableNames[1].Substring(1);
                    markNode.MType = 11;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+……[a-zA-Z0-9\u4e00-\u9fa5]+……[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//选中对勾的情况☑，objectName存对应对象的名字,add1保存FID对应的值，add2保存ID对应的值，HTFL……1……4
                    markNode.SerializableNames = Regex.Split(mark.Name, "……", RegexOptions.IgnoreCase).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ObjName = markNode.SerializableNames[0];
                    markNode.ValueNameAdd1 = markNode.SerializableNames[1];
                    markNode.ValueNameAdd2 = markNode.SerializableNames[2];
                    markNode.MType = 10;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+？[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {//嵌套调用远程服务的  Object？ValueName：ReturnFieldName   服务方法名保存在内容text中
                    markNode.SerializableNames = mark.Name.Split(new string[] { "？", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.MethodNameForValue = mark.Text;
                    markNode.ObjName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.ReturnFieldName = markNode.SerializableNames[2];
                    markNode.MType = 9;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+——[0-9]+——[0-9]+$"))
                {
                    markNode.SerializableNames = Regex.Split(mark.Name, "——", RegexOptions.IgnoreCase).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.TitleIndex = markNode.SerializableNames[1];
                    markNode.DefaultRowsCount = markNode.SerializableNames[2];
                    markNode.MType = 8;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+！[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //分层表示一个obj￥list！value：FGLDSH(区分唯一)---SPYJ{LY[SHSM]}
                    markNode.SerializableNames = mark.Name.Split(new string[] { "￥", "！", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.Name = markNode.SerializableNames[0];
                    markNode.ListName = markNode.SerializableNames[1];
                    markNode.ValueName = markNode.SerializableNames[2];
                    markNode.MType = 6;
                }
                else
                if (Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+（[a-zA-Z0-9\u4e00-\u9fa5]+，[a-zA-Z0-9\u4e00-\u9fa5]+）：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //List下面，需要返回好几个属性的字段
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！", "（", "，", "）", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.ValueNameAdd1 = markNode.SerializableNames[2];
                    markNode.ValueNameAdd2 = markNode.SerializableNames[3];
                    markNode.MType = 5;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+￥[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //分层(List)mtype=4表示list+obj对象
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！", "￥", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    //markNode.SerializableNames = Regex.Split(mark.Name, "____", RegexOptions.IgnoreCase).ToList();//这是正则的分割
                    //markNode.Name = markNode.SerializableNames[0];
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.ObjName = markNode.SerializableNames[1];
                    markNode.ValueName = markNode.SerializableNames[2];
                    markNode.OnlySign = markNode.SerializableNames[3];
                    markNode.MType = 4;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+￥[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //分层(List)mtype=3表示对象obj+obj对象
                    markNode.SerializableNames = mark.Name.Split(new string[] { "￥" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    //markNode.Name中保存的是第一个对象的名字
                    markNode.Name = markNode.SerializableNames[0];
                    //markNode.ObjName中保存的是第二个对象的名字
                    markNode.ObjName = markNode.SerializableNames[1];
                    markNode.ValueName = markNode.SerializableNames[2];
                    markNode.MType = 3;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))//最后加了一个冒号的表示书签名字不重复
                {
                    //分层(List)mtype=2表示list
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.MType = 2;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+！[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //分层(List)mtype=2表示list
                    markNode.SerializableNames = mark.Name.Split(new string[] { "！" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ListName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.MType = 2;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //对象的，mytpe=1表示obj对象
                    markNode.SerializableNames = Regex.Split(mark.Name, "￥", RegexOptions.IgnoreCase).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ObjName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.MType = 1;
                }
                else if (Regex.IsMatch(mark.Name, @"\w+￥[a-zA-Z0-9\u4e00-\u9fa5]+：[a-zA-Z0-9\u4e00-\u9fa5]+$"))
                {
                    //对象的，obj+value+区分唯一
                    markNode.SerializableNames = mark.Name.Split(new string[] { "￥", "：" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ObjName = markNode.SerializableNames[0];
                    markNode.ValueName = markNode.SerializableNames[1];
                    markNode.MType = 1;
                }
                else
                {
                    //表示无需再解析，叶子节点
                    markNode.SourceBookMarkName = mark.Name;
                    markNode.BookMarkText = mark.Text;
                    markNode.ValueName = mark.Name;
                    markNode.MType = 0;
                }
                //if (MarkNodes.FirstOrDefault(e => e.SourceBookMarkName == markNode.SourceBookMarkName) == null)
                //{
                MarkNodes.Add(markNode);
                //}
            }
        }
        /// <summary>
        /// 对服务返回的数据进行解析和序列化
        /// </summary>
        /// <param name="sourceJson">服务返回的json</param>
        public DataModel ReadDataSource(string sourceJson)
        {

            //得到result和status
            var jsonObj = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(sourceJson);
            var data = new DataModel();

            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            if (jsonObj.ContainsKey("result"))
            {
                //返回的服务中带____号的对象
                //var marks = MarkNodes.Where(e => e.MType == 4).ToList();
                //得到result中的各项属性的对象
                jsonResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonObj["result"] + "");
            }
            else
            {
                jsonResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(sourceJson);
            }
            if (jsonResult != null)
            {
                var level = 0;
                var dic = new Dictionary<string, List<string>>();
                //3月8 start
                if (SelectMtypeMarkNode(14).Count > 0)
                {
                    level = 14;
                    foreach (var item in SelectMtypeMarkNode(14))
                    {
                        var listValue = new List<string>();
                        var tempObj = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.Name] + "");
                        var tem = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(tempObj[item.ObjName] + "");
                        if (item.IndexNum == tem[item.ValueName] + "")
                        {
                            listValue.Add("☑");
                        }
                        else
                        {
                            listValue.Add("□");
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                if (SelectMtypeMarkNode(13).Count > 0)
                {
                    level = 13;
                    foreach (var item in SelectMtypeMarkNode(13))
                    {
                        var tempObj = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.Name] + "");
                        if (tempObj.ContainsKey(item.ListName))
                        {
                            var tempList = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(tempObj[item.ListName] + "");
                            if (!string.IsNullOrEmpty(item.BookMarkText))
                            {
                                var JougeConditon = item.BookMarkText.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                                tempList = tempList.Where(e => e[JougeConditon[0]] + "" == JougeConditon[1]).ToList();
                            }
                            var listValue = new List<string>();
                            foreach (var t in tempList)
                            {
                                if (t.ContainsKey(item.ObjName))
                                {
                                    var temp = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(t[item.ObjName] + "");
                                    var value = temp[item.ValueName] + "";
                                    //判断是否为datetime类型的正则表达式
                                    if (IsDate(value))
                                    {
                                        value = Convert.ToDateTime(value).ToLongDateString();
                                    }
                                    listValue.Add(value);
                                }
                            }
                            var dataChild = new DataChildModel();
                            dataChild.ResDataType = ResultDataType.BookMarkType;
                            dataChild.ExactValue = listValue;
                            if (data.Value.ContainsKey(item.SourceBookMarkName))
                            {
                                data.Value[item.SourceBookMarkName] = dataChild;
                            }
                            else
                            {
                                data.Value.Add(item.SourceBookMarkName, dataChild);
                            }
                        }
                    }
                }
                //3月1 start
                if (SelectMtypeMarkNode(12).Count > 0)
                {//ListName!ValueName……Index 表示集合中的第几个对象的哪个值  List！ValueName……Index（returnFieldName）第二种情况也是远程调用服务
                    level = 12;
                    foreach (var item in SelectMtypeMarkNode(12))
                    {
                        var listValue = new List<string>();
                        var tempList = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult[item.ListName] + "");
                        var temp = "";
                        if (tempList.Count > 0)
                        {
                            temp = tempList[int.Parse(item.IndexNum)][item.ValueName] + "";
                            if (!string.IsNullOrEmpty(item.MethodNameForValue))
                            {
                                var ReceiveRemoteData = GetRemoteValue(item.MethodNameForValue, new object[] { temp });
                                if (!string.IsNullOrEmpty(ReceiveRemoteData))
                                {
                                    var receiveData = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(ReceiveRemoteData);
                                    //这个result里面是一个对象
                                    var dataResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(receiveData["result"] + "");
                                    temp = dataResult.ContainsKey(item.ReturnFieldName) ? dataResult[item.ReturnFieldName] + "" : temp;
                                }
                            }
                            listValue.Add(temp);
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //2月27 start
                if (SelectMtypeMarkNode(11).Count > 0)
                {
                    level = 11;
                    foreach (var item in SelectMtypeMarkNode(11))
                    {
                        var listValue = new List<string>();
                        var temp = jsonResult;
                        if (temp[item.ObjName] + "" == item.TrueIsMatchValue)
                        {
                            listValue.Add("☑");
                        }
                        else if (temp[item.ObjName] + "" != item.TrueIsMatchValue && Regex.IsMatch(temp[item.ObjName] + "", @"[\u4e00-\u9fbb]") && item.TrueIsMatchValue == "9")
                        {//增加了两种判断，这是当书签的名字的是否匹配项为99，即截取的第一个true为9，并且数据的值为汉字的时候，表示的是“其他”的内容，直接填写的
                            listValue.Add(temp[item.ObjName] + "");
                        }
                        else if (item.TrueIsMatchValue == "9")
                        {//这种情况表示已经做出了选项，则“其他”这一项需要填空值
                        }
                        else
                        {
                            listValue.Add("□");
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                if (SelectMtypeMarkNode(10).Count > 0)
                {
                    level = 10;
                    foreach (var item in SelectMtypeMarkNode(10))
                    {
                        var listValue = new List<string>();
                        var temp = jsonResult;
                        if (!string.IsNullOrEmpty(item.ObjName) && item.ObjName != "result")
                        {
                            temp = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.ObjName] + "");
                        }
                        if (temp.ContainsKey("FID") && temp.ContainsKey("ID") && temp["FID"] + "" == item.ValueNameAdd1 && temp["ID"] + "" == item.ValueNameAdd2)
                        {
                            listValue.Add("☑");
                        }
                        else if (item.ValueNameAdd1 == "QJLBDM" && item.ValueNameAdd2 == temp["QJLBDM"] + "")
                        {
                            listValue.Add("☑");
                        }
                        else if ((item.ValueNameAdd1 == "PXFL" && temp["PXFL"] + "" == item.ValueNameAdd2) || (item.ValueNameAdd1 == "JYLX" && temp["JYLX"] + "" == item.ValueNameAdd2))
                        {
                            listValue.Add("☑");
                        }
                        else
                        {
                            listValue.Add("□");
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //2月24 start
                if (SelectMtypeMarkNode(9).Count > 0)
                {//嵌套调用远程服务的  Object？ValueName：ReturnFieldName   服务方法名保存在内容text中
                    level = 9;
                    foreach (var item in SelectMtypeMarkNode(9))
                    {
                        var listValue = new List<string>();
                        var temp = jsonResult;
                        //为null时表示调服务的，但是字段是result一级的，不为null时，表示是result下一级的。
                        if (item.ObjName != "null")
                        {
                            temp = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.ObjName] + "");
                        }
                        var RelatedId = temp[item.ValueName] + "";
                        var ReceiveRemoteData = GetRemoteValue(item.MethodNameForValue, null);

                        if (!string.IsNullOrEmpty(ReceiveRemoteData))
                        {
                            var receiveData = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(ReceiveRemoteData);
                            //这个result里面是一个集合
                            var dataResult = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(receiveData["result"] + "");
                            var value = dataResult.FirstOrDefault(e => e["DEPARTMENTID"] + "" == RelatedId)?[item.ReturnFieldName] + "";
                            listValue.Add(value);
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //2月22 start
                if (SelectMtypeMarkNode(8).Count > 0)
                {//书签形如Table——1——3，表示表格结构，动态插入行，1表示标题的行的索引，3表示默认存在了3行
                    level = 8;
                    foreach (var item in SelectMtypeMarkNode(8))
                    {
                        var objJson = new List<Dictionary<string, object>>();
                        if (item.ListName == "null")
                        {
                            if (jsonResult["JTList"].ToString().Length > 5)
                            {//交通
                                objJson = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult["JTList"] + "");
                            }
                            else if (jsonResult["FWList"].ToString().Length > 5)
                            {//房屋
                                objJson = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult["FWList"] + "");
                            }
                            else if (jsonResult["TDList"].ToString().Length > 5)
                            {//土地
                                objJson = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult["TDList"] + "");
                            }
                        }
                        else
                        {
                            objJson = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult[item.ListName] + "");
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.TableType;
                        dataChild.ValueCollection = objJson;
                        dataChild.TitleIndex = int.Parse(item.TitleIndex);
                        dataChild.RowsCount = int.Parse(item.DefaultRowsCount);
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //2月22 end
                if (SelectMtypeMarkNode(7).Count > 0)
                {
                    level = 7;
                    //分层表示一个obj￥list！value……索引---TXRY[0]XM
                    foreach (var item in SelectMtypeMarkNode(7))
                    {
                        var objJson = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.Name] + "");
                        var temp = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(objJson[item.ListName] + "");
                        //根据集合对书签进行按索引插入
                        if (!string.IsNullOrEmpty(item.IndexNum) && int.Parse(item.IndexNum) <= temp.Count && temp.Count > 0)
                        {
                            var listValue = new List<string>();
                            var value = temp[int.Parse(item.IndexNum)][item.ValueName] + "";
                            if (IsDate(value))
                            {
                                value = Convert.ToDateTime(value).ToLongDateString();
                            }
                            listValue.Add(value);
                            var dataChild = new DataChildModel();
                            dataChild.ResDataType = ResultDataType.BookMarkType;
                            dataChild.ExactValue = listValue;
                            data.Value.Add(item.SourceBookMarkName, dataChild);
                        }
                    }
                }
                if (SelectMtypeMarkNode(6).Count > 0)
                {
                    //obj￥list！value：FGLDSH(区分唯一)---SPYJ{LY[SHSM]}

                    level = 6;
                    foreach (var item in SelectMtypeMarkNode(6))
                    {
                        var objJson = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.Name] + "");
                        var temp = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(objJson[item.ListName] + "");
                        //增加对书签内容的判断，如果存在书签的内容，则进行内容的筛选过滤
                        if (!string.IsNullOrEmpty(item.BookMarkText))
                        {
                            var JougeConditon = item.BookMarkText.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                            temp = temp.Where(e => e[JougeConditon[0]] + "" == JougeConditon[1] + "").ToList();
                        }
                        var listValue = new List<string>();
                        foreach (var t in temp)
                        {
                            var value = "";
                            if (t.ContainsKey(item.ValueName))
                            {
                                value = t[item.ValueName] + "";
                                //判断是否为datetime类型的正则表达式
                                if (IsDate(value))
                                {
                                    value = Convert.ToDateTime(value).ToLongDateString();
                                }
                                listValue.Add(value);
                            }
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        if (data.Value.ContainsKey(item.SourceBookMarkName))
                        {
                            data.Value[item.SourceBookMarkName] = dataChild;
                        }
                        else
                        {
                            data.Value.Add(item.SourceBookMarkName, dataChild);
                        }
                    }
                }
                if (SelectMtypeMarkNode(5).Count > 0)
                {
                    //List+Value+Value1+Value2
                    level = 5;
                    foreach (var item in SelectMtypeMarkNode(5))
                    {
                        var temp = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult[item.ListName] + "");
                        if (!string.IsNullOrEmpty(item.BookMarkText))
                        {
                            var JougeConditon = item.BookMarkText.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                            temp = temp.Where(e => e[JougeConditon[0]] + "" == JougeConditon[1] + "").ToList();
                        }
                        var listValue = new List<string>();
                        foreach (var t in temp)
                        {
                            var tempValueTwo = "";
                            //判断是否为datetime类型的正则表达式
                            if (IsDate(t[item.ValueNameAdd2] + ""))
                            {
                                t[item.ValueNameAdd2] = Convert.ToDateTime(t[item.ValueNameAdd2] + "").ToLongDateString();
                            }
                            try
                            {
                                var ListValueNameAdd1 = item.ValueNameAdd1.Split('￥');
                                var ListIsSplitRxx = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(t[ListValueNameAdd1[0]] + "");
                                var NeedValue = ListIsSplitRxx[ListValueNameAdd1[1]] + "";
                                tempValueTwo = NeedValue;
                            }
                            catch (Exception)
                            {
                                tempValueTwo = t[item.ValueNameAdd1] + "";
                            }
                            var value = $"{t[item.ValueName] + ""}\t\t\t{t[item.ValueNameAdd1] + ""}\t\t\t{t[item.ValueNameAdd2] + ""}";
                            listValue.Add(value);
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //遍历书签规则，先查询是否存在类型为4的
                if (SelectMtypeMarkNode(4).Count > 0)
                {//List+Object
                    level = 4;
                    foreach (var item in SelectMtypeMarkNode(4))
                    {
                        var temp = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult[item.ListName] + "");
                        //增加对书签内容的判断，如果存在书签的内容，则进行内容的筛选过滤
                        if (!string.IsNullOrEmpty(item.BookMarkText))
                        {
                            var JougeConditon = item.BookMarkText.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                            temp = temp.Where(e => e[JougeConditon[0]] + "" == JougeConditon[1] + "").ToList();
                        }
                        var listValue = new List<string>();
                        foreach (var t in temp)
                        {
                            var tem = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(t[item.ObjName] + "");
                            var value = tem[item.ValueName] + "";
                            //判断是否为datetime类型的正则表达式
                            if (IsDate(value))
                            {
                                value = Convert.ToDateTime(value).ToLongDateString();
                            }
                            listValue.Add(value);
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                //再判断是否存在类型为3的
                if (SelectMtypeMarkNode(3).Count > 0)
                {//两个对象嵌套的
                    level = 3;
                    foreach (var item in SelectMtypeMarkNode(3))
                    {
                        var temp = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.Name] + "");

                        var listValue = new List<string>();
                        var tem = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(temp[item.ObjName] + "");
                        var value = tem[item.ValueName] + "";
                        //判断是否为datetime类型的正则表达式
                        if (IsDate(value))
                        {
                            value = Convert.ToDateTime(value).ToLongDateString();
                        }
                        listValue.Add(value);
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                if (SelectMtypeMarkNode(2).Count > 0)
                {//只有list
                    level = 2;
                    foreach (var item in SelectMtypeMarkNode(2))
                    {
                        var temp = JsonHelper.DeserializeJsonToObject<List<Dictionary<string, object>>>(jsonResult[item.ListName] + "");
                        //增加对书签内容的判断，如果存在书签的内容，则进行内容的筛选过滤
                        if (!string.IsNullOrEmpty(item.BookMarkText))
                        {
                            var JougeConditon = item.BookMarkText.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                            temp = temp.Where(e => e[JougeConditon[0]] + "" == JougeConditon[1] + "").ToList();
                        }
                        var listValue = new List<string>();
                        foreach (var t in temp)
                        {
                            var value = t[item.ValueName] + "";
                            //判断是否为datetime类型的正则表达式
                            if (IsDate(value))
                            {
                                value = Convert.ToDateTime(value).ToLongDateString();
                            }
                            listValue.Add(value);
                        }
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                if (SelectMtypeMarkNode(1).Count > 0)
                {//只有一个对象的
                    level = 1;
                    foreach (var item in SelectMtypeMarkNode(1))
                    {
                        var temp = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult[item.ObjName] + "");
                        if (temp == null) continue;
                        var value = temp[item.ValueName] + "";
                        //判断是否为datetime类型的正则表达式
                        if (IsDate(value))
                        {
                            if (item.BookMarkText != "all")
                                value = Convert.ToDateTime(value).ToLongDateString();
                        }
                        var listValue = new List<string>();
                        listValue.Add(value);
                        var dataChild = new DataChildModel();
                        dataChild.ResDataType = ResultDataType.BookMarkType;
                        dataChild.ExactValue = listValue;
                        data.Value.Add(item.SourceBookMarkName, dataChild);
                    }
                }
                if (SelectMtypeMarkNode(0).Count > 0)
                {//没有list值，也没有对象，直接为值的
                    foreach (var item in SelectMtypeMarkNode(0))
                    {
                        if (jsonResult.ContainsKey(item.SourceBookMarkName))
                        {
                            var value = jsonResult[item.ValueName] + "";
                            //判断是否为datetime类型的正则表达式
                            if (IsDate(value))
                            {
                                value = Convert.ToDateTime(value).ToLongDateString();
                            }
                            var listValue = new List<string>();
                            listValue.Add(value);
                            var dataChild = new DataChildModel();
                            if (item.OnlySign == "dunhao")
                            {
                                dataChild.ListSplitStr = "、";
                            }
                            dataChild.ResDataType = ResultDataType.BookMarkType;
                            dataChild.ExactValue = listValue;
                            data.Value.Add(item.SourceBookMarkName, dataChild);
                        }
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 筛选不同类型的书签规则模型对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<MarkNode> SelectMtypeMarkNode(int type)
        {
            var mark = MarkNodes.Where(e => e.MType == type).ToList();
            return mark;
        }
        private string GetRemoteValue(string methodName, object[] obj)
        {
            string result;
            try
            {
                //JAVA
                HproseHttpClient client = new HproseHttpClient("http://ngccoa.geo-compass.com/HproseServer");
                result = client.Invoke<string>(methodName, obj);
            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 对字符串进行时间格式的判断
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public bool IsDate(string strDate)
        {
            try
            {
                if (Regex.IsMatch(strDate, @"\."))
                {
                    return false;
                }
                double d;
                if (double.TryParse(strDate, out d))
                {
                    return false;
                }
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public enum ResultDataType
    {
        BookMarkType,
        TableType
    }
    public class DataChildModel
    {
        public List<string> ExactValue { get; set; }
        public object ValueCollection { get; set; }
        public ResultDataType ResDataType { get; set; }
        public string ListSplitStr { get; set; }
        public int TitleIndex { get; set; }
        public int RowsCount { get; set; }
    }
    public class DataModel
    {

        public DataModel()
        {
            this.Value = new Dictionary<string, DataChildModel>();
        }
        public Dictionary<string, DataChildModel> Value { get; set; }
    }
    /// <summary>
    /// 导出的主程序类
    /// </summary>
    public class ExportNgccoaWordByAsp
    {
        public static string ReplaceBookMarkBase(string sourceJson, string modelName, BookmarkCollection bookmarkList, string listColNameStr)
        {
            var bookMarkBase = new BookMarkBase(modelName);
            //获取书签规则
            bookMarkBase.GetBookMarkRules(bookmarkList);
            //解析服务数据并与书签匹配存为字典
            var dataModel = bookMarkBase.ReadDataSource(sourceJson);
            //遍历字典进行书签替换
            var baseWord = new BaseWord(modelName);

            //将插入表的列名进行序列化
            var listColNames = listColNameStr.Split('#').ToList();
            if (dataModel.Value.Count == 0)
            {
                return "";
            }
            foreach (var item in dataModel.Value.Keys)
            {
                switch (dataModel.Value[item].ResDataType)
                {
                    case ResultDataType.BookMarkType:
                        {
                            var contentList = dataModel.Value[item].ExactValue;
                            var contentStr = string.Join("\n", contentList.ToArray());
                            if (item.EndsWith("dunhao"))
                            {
                                dataModel.Value[item].ListSplitStr = "、";
                            }
                            var listSplitStr = dataModel.Value[item].ListSplitStr;
                            if (!string.IsNullOrEmpty(listSplitStr))
                            {
                                contentStr = string.Join(listSplitStr, contentList.ToArray());
                            }
                            //书签替换
                            BusinessWord.ReplaceBookmark(baseWord.doc, item, contentStr);
                        }
                        break;
                    case ResultDataType.TableType:
                        {
                            var contentTemp = dataModel.Value[item].ValueCollection as List<Dictionary<string, object>>;
                            var listRowCount = contentTemp.Count;
                            var titleIndex = dataModel.Value[item].TitleIndex;
                            var DefaultRowsCount = dataModel.Value[item].RowsCount;
                            ReplaceTable(baseWord, titleIndex, DefaultRowsCount, listColNames, contentTemp, sourceJson);
                        }
                        break;
                    default:
                        break;
                }
            }
            //设置word保存名字，进行保存
            var time = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            var wordName = string.Format("{0}{1}", modelName, time);
            try
            {
                baseWord.Save(wordName);
                return baseWord.SavePath;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 根据服务参数和word模板进行word导出
        /// </summary>
        /// <param name="para">对应服务的参数，以逗号分隔</param>
        /// <param name="docModelName">模板的名字，不带后缀</param>
        /// <returns></returns>
        [Hprose("string ExportNgccoaWordByAspNet(string para,string docModelName)", "根据服务参数和word模板进行word导出")]
        public string ExportNgccoaWordByAspNet(string para, string docModelName)
        {
            try
            {
                var paraList = para.Split(new char[] { ',' });
                var obj = new object[paraList.Length];
                var i = 0;
                foreach (var item in paraList)
                {
                    obj[i] = item;
                    i++;
                }
                var dic = JcbgService.GetDic();
                string result;
                try
                {
                    try
                    {
                        //JAVA
                        HproseHttpClient client = new HproseHttpClient("http://ngccoa.geo-compass.com/HproseServer");
                        //HproseHttpClient client = new HproseHttpClient("http://192.9.100.100/HproseServer");
                        result = client.Invoke<string>(dic[docModelName].MethodName, obj);
                    }
                    catch (Exception ex)
                    {
                        //PHP
                        HproseHttpClient client = new HproseHttpClient("http://ngccoaphp.geo-compass.com/public/admin.php/demo/");
                        //HproseHttpClient client = new HproseHttpClient("http://192.9.100.100:82/public/admin.php/demo");
                        result = client.Invoke<string>(dic[docModelName].MethodName, obj);
                    }
                }
                catch (Exception exM)
                {
                    SystemLogger.getLogger().Error($"调用数据源服务报错，错误信息为：{exM.ToString()}");
                    return JsonHelper.SerializeObject(new ResultModel(false, null, exM.ToString()));
                }
               
                //非涉密的
                if (docModelName == "附件6-国家基础地理信息中心在职干部因私事出国(境)领用表")
                {
                    var jsonResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(result);
                    var jsonCJDJ = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult["CJDJ"] + "");
                    //没证的话，需要领用表+审批表
                    if (jsonCJDJ["YWZZ"] + "" == "2")
                    {
                        docModelName = "附件6-国家基础地理信息中心在职干部因私事出国(境)领用表and审批表";
                        //savepath = dic["附件6-国家基础地理信息中心在职干部因私事出国(境)领用表and审批表"].GetPathByLogic(result, "附件6-国家基础地理信息中心在职干部因私事出国(境)领用表and审批表");
                    }
                    else
                    {//有证的话，只需要领用表
                        docModelName = "附件6-国家基础地理信息中心在职干部因私事出国(境)领用表";
                        // savepath = dic[docModelName].GetPathByLogic(result, docModelName);
                    }
                }
                else if (docModelName == "附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表")//涉密的话
                {
                    var jsonResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(result);
                    var jsonCJDJ = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonResult["CJDJ"] + "");
                    //没证的话，领用表+保密教育表+审批表涉密+审批表非涉密
                    if (jsonCJDJ["YWZZ"] + "" == "2")
                    {
                        docModelName = "附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密and审批表非涉密";
                    }
                    else
                    {//有证的话，只需要领用表+保密教育表+审批表涉密
                        docModelName = "附件7-国家基础地理信息中心涉密干部因私事出国(境)领用表and保密表and审批表涉密";
                    }
                }

                var exportConfig = dic[docModelName];
                //进入操作
                var savepath = ReplaceBookMarkBase(result, docModelName, exportConfig.BookMarkCollection, exportConfig.ListColNameStr);
                if (string.IsNullOrEmpty(savepath))
                {
                    return JsonHelper.SerializeObject(new ResultModel(false, "生成文件失败！"));
                }
                return JsonHelper.SerializeObject(new ResultModel(true, savepath));
            }
            catch (Exception e)
            {
                return JsonHelper.SerializeObject(new ResultModel(false, e.ToString()));
            }
        }
        /// <summary>
        /// 插入word表格中的中间动态行
        /// </summary>
        /// <param name="baseword"></param>
        /// <param name="titleRowIndex">标题的索引位置（所在的行）</param>
        /// <param name="rowsCount">模板默认存在几行</param>
        /// <param name="colsNames">列名</param>
        /// <param name="listData">json序列化出来的多个字典，一个字典对应一行</param>
        public static void ReplaceTable(BaseWord baseword, int titleRowIndex, int rowsCount, List<string> colsNames, List<Dictionary<string, object>> listData, string sourceJson)
        {
            var table = baseword.doc.GetChild(NodeType.Table, 0, true) as Table;
            var dtRow = table.Rows[titleRowIndex + 1];
            // Add 10 more rows into the table
            for (int i = 0; i < listData.Count; i++)
            {
                var rowDic = listData[i];
                // Clone last row
                Row newRow = (Row)dtRow.Clone(true);
                // Insert created row into the table
                //table.InsertAfter(newRow, dtRow);
                table.Rows.Insert(titleRowIndex + i + 1, newRow);
                if (i < rowsCount)
                {
                    table.Rows.RemoveAt(titleRowIndex + i + 2);
                }

                //table.InsertBefore(newRow, dtRangeEnd);
                //table.AppendChild(newRow);

                // Loop through all cells in row
                for (int y = 0; y < newRow.Cells.Count; y++)
                {
                    var cell = newRow.Cells[y];
                    if (cell.IsFirstCell)
                    {
                        cell.FirstParagraph.ChildNodes.Clear();
                        // Move DocumentBuilder cursor to the cell
                        baseword.builder.MoveTo(cell.FirstParagraph);
                        baseword.builder.Write(i + 1 + "");
                    }
                    else
                    {
                        cell.FirstParagraph.ChildNodes.Clear();
                        // Move DocumentBuilder cursor to the cell
                        baseword.builder.MoveTo(cell.FirstParagraph);
                        if (Regex.IsMatch(colsNames[y], @"\w+&[a-zA-Z0-9]+$"))
                        {
                            var tempField = Regex.Split(colsNames[y], "&", RegexOptions.IgnoreCase);
                            var tempFieldChild = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(rowDic[tempField[0]] + "");
                            if (tempFieldChild != null)
                            {
                                baseword.builder.Write(tempFieldChild[tempField[1]] + "");
                            }
                        }
                        else
                        if (Regex.IsMatch(colsNames[y], @"\w+%+$"))
                        {
                            var fieldPartArr = colsNames[y].Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                            if (fieldPartArr.Length >= 3)
                            {
                                var needWriteColumnName = fieldPartArr[0];
                                var needWriteColumnValue = double.Parse(string.IsNullOrEmpty(rowDic[fieldPartArr[1]] + "") ? "0" : rowDic[fieldPartArr[1]] + "") * int.Parse(string.IsNullOrEmpty(rowDic[fieldPartArr[2]] + "") ? "0" : rowDic[fieldPartArr[2]] + "");
                                if (needWriteColumnValue > 0)
                                {
                                    baseword.builder.Write(needWriteColumnValue.ToString("f2"));
                                }
                            }
                            //var field = colsNames[y].Substring(0, colsNames[y].Length - 1);
                            //var jsonTotal = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(sourceJson);
                            //if (jsonTotal != null)
                            //{
                            //    var jsonResult = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(jsonTotal["result"] + "");
                            //    if (jsonResult != null && jsonResult.ContainsKey(field))
                            //    {
                            //        baseword.builder.Write(jsonResult[field] + "");
                            //    }
                            //}
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(colsNames[y]))
                            {
                                if (new BookMarkBase().IsDate(rowDic[colsNames[y]] + ""))
                                {//时间的话，需要转化为日期
                                    var newValue = Convert.ToDateTime(rowDic[colsNames[y]] + "").ToLongDateString();
                                    baseword.builder.Write(newValue);
                                }
                                else
                                {//非时间的话，直接插入
                                    baseword.builder.Write(rowDic[colsNames[y]] + "");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}