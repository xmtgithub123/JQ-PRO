using System;

namespace DataModel.Models
{
    public class ProjManage
    {
        public ProjManage()
        {
            Id = 0;
            EngineeringID = 0;
            Cost = "";
            Safe = "";
            Finished = "";
            Doc = "";
            Note = "";
            CreatorEmpId = 0;
            CreatorEmpName = "";
            EmpManageId = 0;
            CreatorTime = DateTime.Now;
        }

        ///<summary></summary>
        public int Id { get; set; }

        ///<summary>实施方案</summary>
        public int EngineeringID { get; set; }

        ///<summary>成本指标</summary>
        public string Cost { get; set; }

        ///<summary>安全目标</summary>
        public string Safe { get; set; }

        ///<summary>工期目标</summary>
        public string Finished { get; set; }

        ///<summary>资料目标</summary>
        public string Doc { get; set; }

        ///<summary>特殊要求</summary>
        public string Note { get; set; }

        ///<summary>创建人Id</summary>
        public int CreatorEmpId { get; set; }

        ///<summary>创建人名称</summary>
        public string CreatorEmpName { get; set; }

        ///<summary>创建时间</summary>
        public DateTime CreatorTime { get; set; }
        public int EmpManageId { get; set; }
    }
}
