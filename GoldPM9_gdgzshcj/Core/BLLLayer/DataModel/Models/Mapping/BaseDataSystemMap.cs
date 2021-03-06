﻿#region <auto-generated>
//此代码由T4模板自动生成 
//生成时间 2016-09-06 20:09:48
#endregion
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace DataModel.Models.Mapping
{
public class BaseDataSystemMap : EntityTypeConfiguration<BaseDataSystem>
    {
        public BaseDataSystemMap()
        {
			this.HasKey(t => new { t.BaseID}); 
				  
			this.Property(t => t.BaseName).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseOrder).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseNameEng).IsRequired().HasMaxLength(50);
	  
			this.Property(t => t.BaseAtt1).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseAtt2).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseAtt3).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseAtt4).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseAtt5).IsRequired().HasMaxLength(100);
	  
			this.Property(t => t.BaseIsFixed).IsRequired();
	  
			this.Property(t => t.BaseIsDeleted).IsRequired();


			 // Table & Column Mappings
			this.ToTable("BaseDataSystem");
			this.Property(t => t.BaseID).HasColumnName("BaseID");
			this.Property(t => t.BaseName).HasColumnName("BaseName");
			this.Property(t => t.BaseOrder).HasColumnName("BaseOrder");
			this.Property(t => t.BaseNameEng).HasColumnName("BaseNameEng");
			this.Property(t => t.BaseAtt1).HasColumnName("BaseAtt1");
			this.Property(t => t.BaseAtt2).HasColumnName("BaseAtt2");
			this.Property(t => t.BaseAtt3).HasColumnName("BaseAtt3");
			this.Property(t => t.BaseAtt4).HasColumnName("BaseAtt4");
			this.Property(t => t.BaseAtt5).HasColumnName("BaseAtt5");
			this.Property(t => t.BaseIsFixed).HasColumnName("BaseIsFixed");
			this.Property(t => t.BaseIsDeleted).HasColumnName("BaseIsDeleted");
			           
			#region Relationships
			#endregion
        }
    }
}  
