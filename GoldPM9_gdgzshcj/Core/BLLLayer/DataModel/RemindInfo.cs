using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class RemindInfo
    {
        public RemindInfo() { }
        public RemindInfo(string _refTable, int _refID, string _title, string _name, string _url,int _num=0)
        {
            this.RefTable = _refTable;
            this.RefID = _refID;
            this.Title = _title;
            this.Name = _name;
            this.Url = _url;
            this.Num = _num;
        }

        public string RefTable{ get; set; }
        public int RefID{ get; set; }
        public string Title{ get; set; }
        public string Name{ get; set; }
        public string Url{ get; set; }
        public int Num { get; set; }
         
    }
}
