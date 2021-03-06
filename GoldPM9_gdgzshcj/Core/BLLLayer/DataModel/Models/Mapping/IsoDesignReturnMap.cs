﻿#region <auto-generated>
//此代码由T4模板自动生成
//生成时间 2017-09-25 18:00:59
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class IsoDesignReturnMap : EntityTypeConfiguration<IsoDesignReturn>
    {
        public IsoDesignReturnMap()
        {
			this.HasKey(t => new { t.Id});
							this.Property(t => t.Number).IsRequired().HasMaxLength(50);
				this.Property(t => t.ProjId).IsRequired();
				this.Property(t => t.ProjNumer).IsRequired().HasMaxLength(50);
				this.Property(t => t.ProjName).IsRequired().HasMaxLength(100);
				this.Property(t => t.ReturnPerson).HasMaxLength(50);
				this.Property(t => t.ReturnDate);
				this.Property(t => t.OrganizeDepart).HasMaxLength(50);
				this.Property(t => t.Participant).HasMaxLength(50);
				this.Property(t => t.Text).HasMaxLength(200);
				this.Property(t => t.Unresolved).HasMaxLength(200);
				this.Property(t => t.IsResolved);
				this.Property(t => t.IsResponse);
				this.Property(t => t.RecordName).HasMaxLength(50);
				this.Property(t => t.TechnologyName).HasMaxLength(50);
				this.Property(t => t.ResponbilityName).HasMaxLength(50);
				this.Property(t => t.RecordDate);
				this.Property(t => t.TechnologyDate);
				this.Property(t => t.ResponsibilityDate);
				this.Property(t => t.DeletionTime).IsRequired();
				this.Property(t => t.CreationTime).IsRequired();
				this.Property(t => t.CreatorEmpId).IsRequired();
				this.Property(t => t.CreatorEmpName).IsRequired().HasMaxLength(50);
				this.Property(t => t.CreatorDepId).IsRequired();
				this.Property(t => t.CreatorDepName).IsRequired().HasMaxLength(50);
				this.Property(t => t.LastModificationTime).IsRequired();
				this.Property(t => t.LastModifierEmpId).IsRequired();
				this.Property(t => t.LastModifierEmpName).IsRequired().HasMaxLength(50);
				this.Property(t => t.AgenCreatorEmpId).IsRequired();
				this.Property(t => t.AgenCreatorEmpName).IsRequired().HasMaxLength(50);
				this.Property(t => t.AgenLastModifierEmpId).IsRequired();
				this.Property(t => t.AgenLastModifierEmpName).IsRequired().HasMaxLength(50);
				this.Property(t => t.AgenDeleterEmpId).IsRequired();
				this.Property(t => t.AgenDeleterEmpName).IsRequired().HasMaxLength(50);
				this.Property(t => t.DeleterEmpId).IsRequired();
				this.Property(t => t.DeleterEmpName).IsRequired().HasMaxLength(50);


			 // Table & Column Mappings
			this.ToTable("IsoDesignReturn");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Number).HasColumnName("Number");
			this.Property(t => t.ProjId).HasColumnName("ProjId");
			this.Property(t => t.ProjNumer).HasColumnName("ProjNumer");
			this.Property(t => t.ProjName).HasColumnName("ProjName");
			this.Property(t => t.ReturnPerson).HasColumnName("ReturnPerson");
			this.Property(t => t.ReturnDate).HasColumnName("ReturnDate");
			this.Property(t => t.OrganizeDepart).HasColumnName("OrganizeDepart");
			this.Property(t => t.Participant).HasColumnName("Participant");
			this.Property(t => t.Text).HasColumnName("Text");
			this.Property(t => t.Unresolved).HasColumnName("Unresolved");
			this.Property(t => t.IsResolved).HasColumnName("IsResolved");
			this.Property(t => t.IsResponse).HasColumnName("IsResponse");
			this.Property(t => t.RecordName).HasColumnName("RecordName");
			this.Property(t => t.TechnologyName).HasColumnName("TechnologyName");
			this.Property(t => t.ResponbilityName).HasColumnName("ResponbilityName");
			this.Property(t => t.RecordDate).HasColumnName("RecordDate");
			this.Property(t => t.TechnologyDate).HasColumnName("TechnologyDate");
			this.Property(t => t.ResponsibilityDate).HasColumnName("ResponsibilityDate");
			this.Property(t => t.DeletionTime).HasColumnName("DeletionTime");
			this.Property(t => t.CreationTime).HasColumnName("CreationTime");
			this.Property(t => t.CreatorEmpId).HasColumnName("CreatorEmpId");
			this.Property(t => t.CreatorEmpName).HasColumnName("CreatorEmpName");
			this.Property(t => t.CreatorDepId).HasColumnName("CreatorDepId");
			this.Property(t => t.CreatorDepName).HasColumnName("CreatorDepName");
			this.Property(t => t.LastModificationTime).HasColumnName("LastModificationTime");
			this.Property(t => t.LastModifierEmpId).HasColumnName("LastModifierEmpId");
			this.Property(t => t.LastModifierEmpName).HasColumnName("LastModifierEmpName");
			this.Property(t => t.AgenCreatorEmpId).HasColumnName("AgenCreatorEmpId");
			this.Property(t => t.AgenCreatorEmpName).HasColumnName("AgenCreatorEmpName");
			this.Property(t => t.AgenLastModifierEmpId).HasColumnName("AgenLastModifierEmpId");
			this.Property(t => t.AgenLastModifierEmpName).HasColumnName("AgenLastModifierEmpName");
			this.Property(t => t.AgenDeleterEmpId).HasColumnName("AgenDeleterEmpId");
			this.Property(t => t.AgenDeleterEmpName).HasColumnName("AgenDeleterEmpName");
			this.Property(t => t.DeleterEmpId).HasColumnName("DeleterEmpId");
			this.Property(t => t.DeleterEmpName).HasColumnName("DeleterEmpName");
			
			#region Relationships
			#endregion
        }
    }
}
