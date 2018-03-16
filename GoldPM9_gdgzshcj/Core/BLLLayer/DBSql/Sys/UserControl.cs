using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSql.Sys
{
    public class UserControl
    {
        public void MoveFile(int AttachRefID,int EmpID, string AttachRefTable)
        {
            var attach = new BaseAttach();
            attach.UploadFileToBaseAttach(AttachRefTable, AttachRefID, EmpID);
            attach.UnitOfWork.SaveChanges();
        }
    }
}
