using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common.Data;
using Common.Data.Extenstions;
using DAL;
namespace DBSql.PubFunction
{
    public class V_SystemTableFKey : EFRepository<DataModel.Models.V_SystemTableFKey>
    {
        public int SP_FKReName()
        {
            int result = ExecuteNonQuery("EXEC  [dbo].[SP_FKReName]", new List<System.Data.SqlClient.SqlParameter>().ToArray());

            return result;
        }

        public static string GetFirstString(List<DataModel.Models.V_SystemTableStructure> arr, string TableName)
        {
            var model = arr.Where(p => p.TableName == TableName && p.ColType == "varchar").First();
            var result = model == null ? "" : model.ColName ;
            return result ;
        }

        public static string GetFirstDateTime(List<DataModel.Models.V_SystemTableStructure> arr, string TableName)
        {
            var model = arr.Where(p => p.TableName == TableName && p.ColType == "datetime").First();
            var result = model == null ? "" : model.ColName;
            return result;
        }

        public static List<string> GetPKColumn(List<DataModel.Models.V_SystemTableStructure> arr, string TableName)
        {
            var model = arr.Where(p => p.TableName == TableName && p.IsPK == "√").ToList();
            List<string> result = new List<string>();
            model.ForEach(p => result.Add(p.ColName));
            return result;
        }

        public static List<DataModel.Models.V_SystemTableFKey> GetFKeyColumn(List<DataModel.Models.V_SystemTableFKey> arr, string TableName, string ColName = "")
        {
            var model = arr.Where(p => p.TableName == TableName);
            if (string.IsNullOrEmpty(ColName))
            {
                model = model.Where(p => p.ColName == ColName);   
            }
            return model.ToList() ;
        }

        public string GetSortField(List<DataModel.Models.V_SystemTableStructure> Struct,List<DataModel.Models.V_SystemTableFKey> arr,string TableName, string ColName)
        {
            var Fkey = GetFKeyColumn(arr, TableName, ColName);
            if (Fkey == null || Fkey.Count == 0) return "";
            var Model = Fkey[0];
            string RefName = GetFirstString(Struct, Model.RefTableName);
            return String.Format("{0}.{1}",Model.FKName,RefName);
        }

        public string GetEngNameField(List<DataModel.Models.BaseData> data, List<DataModel.Models.V_SystemTableStructure> Struct, List<DataModel.Models.V_SystemTableFKey> arr, string TableName, string ColName)
        {
            //判断是否有 人员选择外键  BaseData外键
            var Fkey = GetFKeyColumn(arr, TableName, ColName);
            Fkey = Fkey.Where(p => p.RefTableName != "BaseData" || p.RefTableName == "BaseEmployee").ToList();

            List<string> Result = new List<string>();

            foreach (var item in Fkey)
            {
                if (item.RefTableName == "BaseEmployee" && !Result.Contains("BaseEmployee"))
                {
                    Result.Add("BaseEmployee");
                }

                if (item.RefTableName == "BaseData")
                {
                    //备注BaseData名称进行比较 找到BaseNameEng
                    var model = Struct.Where(p => p.TableName == TableName && p.ColType == ColName);
                    if (model != null && model.Count() != 0)
                    {
                        string ColNote = model.ElementAt(0).ColNote ;

                        var refName = data.FirstOrDefault(p => p.BaseName == ColNote);

                        if (refName != null) Result.Add(refName.BaseNameEng);

                    }
                }
            }
            return string.Join(",", Result);
        }
    }
}
