﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2017-03-03 09:52:54
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class HRAptitudeManagerMap : EntityTypeConfiguration<HRAptitudeManager>
    {
        public HRAptitudeManagerMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.AptitudeName).HasMaxLength(50).IsRequired();
	  
			this.Property(t => t.RegisterTime).IsRequired();
	  
			this.Property(t => t.EndTime).IsRequired();
	  
			this.Property(t => t.AptitudeEmpName).HasMaxLength(50).IsRequired();
	  
			this.Property(t => t.IDCard).HasMaxLength(50);
	  
			this.Property(t => t.Phone).HasMaxLength(50);
	  
			this.Property(t => t.Address).HasMaxLength(50);
	  
			this.Property(t => t.IsMess).IsRequired();

            this.Property(t => t.Remark).HasMaxLength(500);

            this.Property(t => t.AptitudeNumber).HasMaxLength(50).IsRequired();

            this.Property(t => t.SpecID).IsRequired();

            this.Property(t => t.ProTitleID).IsRequired();

            this.Property(t => t.LevelID).IsRequired();

            this.Property(t => t.AptitudeEmpId).IsRequired();

            this.Property(t => t.CompanyID).IsRequired();

            // Table & Column Mappings
            this.ToTable("HRAptitudeManager");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.AptitudeName).HasColumnName("AptitudeName");
			this.Property(t => t.RegisterTime).HasColumnName("RegisterTime");
			this.Property(t => t.EndTime).HasColumnName("EndTime");
			this.Property(t => t.AptitudeEmpName).HasColumnName("AptitudeEmpName");
			this.Property(t => t.IDCard).HasColumnName("IDCard");
			this.Property(t => t.Phone).HasColumnName("Phone");
			this.Property(t => t.Address).HasColumnName("Address");
			this.Property(t => t.IsMess).HasColumnName("IsMess");
            this.Property(t => t.Remark).HasColumnName("Remark");

            this.Property(t => t.AptitudeNumber).HasColumnName("AptitudeNumber");
            this.Property(t => t.SpecID).HasColumnName("SpecID");
            this.Property(t => t.ProTitleID).HasColumnName("ProTitleID");
            this.Property(t => t.LevelID).HasColumnName("LevelID");
            this.Property(t => t.AptitudeEmpId).HasColumnName("AptitudeEmpId");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            #region Relationships
            #endregion
        }
    }
}  
