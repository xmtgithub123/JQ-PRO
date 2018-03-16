using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("DesTaskSplitFile")]
    public class DesTaskSplitFile
    {
        [Key]
        public int ID
        {
            get; set;
        }

        public long BaseAttachID
        {
            get; set;
        }

        public long BaseAttachVerID
        {
            get; set;
        }

        public int BaseAttachVer
        {
            get; set;
        }

        public string Extension
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Path
        {
            get; set;
        }

        public long Size
        {
            get; set;
        }

        public string Attributes
        {
            get; set;
        }

        public DateTime CreationTime
        {
            get; set;
        }

        public int CreatorEmpId
        {
            get; set;
        }

        public string CreatorEmpName
        {
            get; set;
        }
    }
}
