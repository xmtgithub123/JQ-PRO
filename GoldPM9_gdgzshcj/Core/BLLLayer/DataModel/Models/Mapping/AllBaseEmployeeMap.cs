using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataModel.Models.Mapping
{
    public class AllBaseEmployeeMap : EntityTypeConfiguration<AllBaseEmployee>
    {
        public AllBaseEmployeeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EmpID, t.EmpGUId, t.EmpName, t.EmpLogin, t.EmpPassword, t.SalaryPassword, t.EmpDepID, t.EmpDepName, t.DepOrder, t.EmpOrder, t.EmpBirthDate, t.EmpTitle, t.EmpTel, t.EmpComputer, t.EmpIPAddress, t.EmpDisk, t.EmpIsDeleted, t.EmpPageSize, t.EmpThemesName, t.EmpMenuType, t.EmpIsAgent, t.EmpSignUrl, t.EmpIsBind, t.EmpMacAddress, t.EmpTelNX, t.EmpTelWX, t.EmpFJNum, t.EmpOaMail, t.EmpComMail, t.EmpZWAddress, t.EmpWGAddress, t.EmpNote, t.EmpPort, t.EmpHead, t.EmpIsSub, t.DepartmentID });

            // Properties
            this.Property(t => t.EmpID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpGUId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpLogin)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpPassword)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SalaryPassword)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpDepID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpDepName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DepOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpTitle)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpTel)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpComputer)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpIPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpDisk)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpPageSize)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EmpThemesName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpMenuType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpSignUrl)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EmpMacAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpTelNX)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpTelWX)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpFJNum)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpOaMail)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpComMail)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmpZWAddress)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmpWGAddress)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmpNote)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.EmpPort)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.EmpHead)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.EmpIsSub)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DepartmentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DepartmentName)
                .HasMaxLength(100);

            this.Property(t => t.DepartmentOrder)
                .HasMaxLength(100);

            this.Property(t => t.RoleName)
                .HasMaxLength(500);

            this.Property(t => t.PayManageCoeff).IsRequired();

            this.Property(t => t.PaySkillCoeff).IsRequired();

            // Table & Column Mappings
            this.ToTable("AllBaseEmployee");
            this.Property(t => t.EmpID).HasColumnName("EmpID");
            this.Property(t => t.EmpGUId).HasColumnName("EmpGUId");
            this.Property(t => t.EmpName).HasColumnName("EmpName");
            this.Property(t => t.EmpLogin).HasColumnName("EmpLogin");
            this.Property(t => t.EmpPassword).HasColumnName("EmpPassword");
            this.Property(t => t.SalaryPassword).HasColumnName("SalaryPassword");
            this.Property(t => t.EmpDepID).HasColumnName("EmpDepID");
            this.Property(t => t.EmpDepName).HasColumnName("EmpDepName");
            this.Property(t => t.DepOrder).HasColumnName("DepOrder");
            this.Property(t => t.EmpOrder).HasColumnName("EmpOrder");
            this.Property(t => t.EmpBirthDate).HasColumnName("EmpBirthDate");
            this.Property(t => t.EmpTitle).HasColumnName("EmpTitle");
            this.Property(t => t.EmpTel).HasColumnName("EmpTel");
            this.Property(t => t.EmpComputer).HasColumnName("EmpComputer");
            this.Property(t => t.EmpIPAddress).HasColumnName("EmpIPAddress");
            this.Property(t => t.EmpDisk).HasColumnName("EmpDisk");
            this.Property(t => t.EmpIsDeleted).HasColumnName("EmpIsDeleted");
            this.Property(t => t.EmpPageSize).HasColumnName("EmpPageSize");
            this.Property(t => t.EmpThemesName).HasColumnName("EmpThemesName");
            this.Property(t => t.EmpMenuType).HasColumnName("EmpMenuType");
            this.Property(t => t.EmpIsAgent).HasColumnName("EmpIsAgent");
            this.Property(t => t.EmpSignUrl).HasColumnName("EmpSignUrl");
            this.Property(t => t.EmpIsBind).HasColumnName("EmpIsBind");
            this.Property(t => t.EmpMacAddress).HasColumnName("EmpMacAddress");
            this.Property(t => t.EmpTelNX).HasColumnName("EmpTelNX");
            this.Property(t => t.EmpTelWX).HasColumnName("EmpTelWX");
            this.Property(t => t.EmpFJNum).HasColumnName("EmpFJNum");
            this.Property(t => t.EmpOaMail).HasColumnName("EmpOaMail");
            this.Property(t => t.EmpComMail).HasColumnName("EmpComMail");
            this.Property(t => t.EmpZWAddress).HasColumnName("EmpZWAddress");
            this.Property(t => t.EmpWGAddress).HasColumnName("EmpWGAddress");
            this.Property(t => t.EmpNote).HasColumnName("EmpNote");
            this.Property(t => t.EmpPort).HasColumnName("EmpPort");
            this.Property(t => t.EmpHead).HasColumnName("EmpHead");
            this.Property(t => t.EmpIsSub).HasColumnName("EmpIsSub");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.DepartmentOrder).HasColumnName("DepartmentOrder");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.PayManageCoeff).HasColumnName("PayManageCoeff");
            this.Property(t => t.PaySkillCoeff).HasColumnName("PaySkillCoeff");
        }
    }
}
