using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCHeritagePlatform.Services.PersonnelRightsManagement.Model
{
    public class FuncTree
    {
        public string ID { get; set; }
        public string ARGS { get; set; }
        public string PID { get; set; }
        public string NAME { get; set; }
        public string ITEMID { get; set; }
        public string DESCRIPTION { get; set; }
        public string GROUPNAME { get; set; }
        public string SYSTEMID { get; set; }
        public int? INDEXOFORDER { get; set; }
        public List<FuncTree> CHILDREN { get; set; }
        public FuncTree(string id, string name,string args)
        {
            this.ID = id;
            this.ARGS = args;
            this.NAME = name;
            CHILDREN = new List<FuncTree>();
        }
        public FuncTree()
        {
            CHILDREN = new List<FuncTree>();
        }


    }
}