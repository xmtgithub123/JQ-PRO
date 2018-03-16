using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    [Table("DesTaskFlowExe")]
    public class DesTaskFlowExe
    {
        [Key]
        public int Id
        {
            get; set;
        }

        //1 提交  2 退回 3 换人
        public int TaskType
        {
            get; set;
        }

        public int ProjectId
        {
            get; set;
        }

        public int PhaseId
        {
            get; set;
        }

        public int SpecialtyId
        {
            get; set;
        }

        public int TaskId
        {
            get; set;
        }

        public int TaskGroupId
        {
            get; set;
        }

        public DateTime ActiveTime
        {
            get; set;
        }

        public int FromEmpId
        {
            get; set;
        }

        public string FromEmpName
        {
            get; set;
        }

        public string ToEmpIds
        {
            get; set;
        }

        public string ToEmpNames
        {
            get; set;
        }

        public string NodeTypeIds
        {
            get; set;
        }

        public string DesTaskAttachExIds
        {
            get; set;
        }

    }
}
