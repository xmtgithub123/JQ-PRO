using EntityFramework.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Project
{
    public class ProjectDynamic : EFRepository<DataModel.Models.ProjectDynamic>, IDisposable
    {
        public List<dynamic> GetList(Common.SqlPageInfo queryContext)
        {
            var query = (IQueryable<DataModel.Models.ProjectDynamic>)DbContext.Set<DataModel.Models.ProjectDynamic>();
            if (queryContext.SelectCondtion != null && queryContext.SelectCondtion.Count > 0)
            {
                foreach (DictionaryEntry de in queryContext.SelectCondtion)
                {
                    if (de.Value == null || de.Value.ToString() == "")
                    {
                        continue;
                    }
                    switch (de.Key.ToString())
                    {
                        case "RefID":
                            var refID = JQ.Util.TypeParse.parse<int>(de.Value);
                            query = query.Where(m => m.RefID == refID);
                            break;
                        case "RefTable":
                            query = query.Where(m => m.RefTable == de.Value.ToString());
                            break;
                    }
                }
            }
            query = query.OrderByDescending(m => m.CreationTime);
            if (!queryContext.ToPageData)
            {
                if (queryContext.PageIndex < 1)
                {
                    queryContext.PageIndex = 1;
                }
                if (queryContext.PageSize == 0)
                {
                    queryContext.PageSize = 10;
                }
                var itemIndex = (queryContext.PageIndex - 1) * queryContext.PageSize;
                queryContext.PageTotleRowCount = query.FutureCount();
                query = query.Skip(itemIndex).Take(queryContext.PageSize);
            }
            else
            {
                queryContext.PageTotleRowCount = 0;
            }
            return query.ToList<dynamic>();
        }

        /// <summary>
        /// 添加项目动态
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="refTable"></param>
        /// <param name="refID"></param>
        /// <param name="content"></param>
        /// <param name="empSession"></param>
        /// <param name="linkTitle"></param>
        /// <param name="linkUrl"></param>
        /// <returns></returns>
        public DataModel.Models.ProjectDynamic AddDynamic(int projectID, string refTable, int refID, string content, DataModel.EmpSession empSession, string linkTitle = "", string linkUrl = "")
        {
            var model = new DataModel.Models.ProjectDynamic();
            Common.ModelConvert.MvcDefaultSave<DataModel.Models.ProjectDynamic>(model, empSession);
            model.ProjectID = projectID;
            model.RefTable = refTable;
            model.RefID = refID;
            model.Content = content;
            model.LinkTitle = linkTitle;
            model.LinkUrl = linkUrl;
            model.IsInvalid = false;
            this.Add(model);
            this.UnitOfWork.SaveChanges();

            return model;
        }

        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            if (this._dbContext != null)
            {

            }
        }
    }
}
