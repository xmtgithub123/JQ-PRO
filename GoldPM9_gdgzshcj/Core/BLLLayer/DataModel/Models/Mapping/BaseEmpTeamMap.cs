﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseEmpTeamMap : EntityTypeConfiguration<BaseEmpTeam>
    {
        public BaseEmpTeamMap()
        {
			this.HasKey(t => new { t.Id}); 
				  
			this.Property(t => t.TeamName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.TeamType).IsRequired();
	  
			this.Property(t => t.CreationTime).IsRequired();
	  
			this.Property(t => t.CreatorEmpId).IsRequired();
	  
			this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.LastModificationTime).IsRequired();
	  
			this.Property(t => t.LastModifierEmpId).IsRequired();
	  
			this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.TeamMembers).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.TeamNote).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("BaseEmpTeam");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.TeamName).HasColumnName("TeamName");
			this.Property(t => t.TeamType).HasColumnName("TeamType");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.TeamMembers).HasColumnName("TeamMembers");
			this.Property(t => t.TeamNote).HasColumnName("TeamNote");
			           
			#region Relationships
			#endregion
        }
    }
}  