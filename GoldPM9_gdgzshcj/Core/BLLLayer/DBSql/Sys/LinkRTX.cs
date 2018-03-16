using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTXSAPILib;
using RTXServerApi;
using System.Data;
using System.Runtime.InteropServices;

namespace DBSql.Sys
{
    public class LinkRTX
    {
        DBSql.Sys.BaseData baseData = new BaseData();

        DBSql.Sys.BaseEmployee baseEmp = new BaseEmployee();


        RTXSAPILib.RTXSAPIRootObj RootObj;  //声明一个根对象
        RTXSAPILib.RTXSAPIDeptManager DeptManagerObj;       //声明一个应用对象

        RTXSAPILib.RTXSAPIUserManager UserManagerObj;       //声明一个用户对象

        private void Inital()
        {
            RootObj = new RTXSAPIRootObj();     //创建根对象
            RootObj.ServerIP = System.Configuration.ConfigurationManager.AppSettings["RTXServerIP"]; //服务端 IP地址
            //RootObj.ServerIP = "192.168.0.181"; //服务端 IP地址
            RootObj.ServerPort = 8006;
        }


        //添加部门
        public void AddDept(string DeptName)
        {
            Inital();

            DeptManagerObj = RootObj.DeptManager;    //通过根对象创建部门管理对象
            
            DeptManagerObj.AddDept(DeptName, ""); //添加部门,如果用户没有上级部门，txtParentDept不需填写，为空即可
        }


        public void DeleteDept(string DeptName)
        {
            //RootObj = new RTXSAPIRootObj();     //创建根对象
            //DeptManagerObj = RootObj.DeptManager;    //通过根对象创建部门管理对象
            //RootObj.ServerIP = "127.0.0.1";
            //RootObj.ServerPort = 8006;

            Inital();

            DeptManagerObj = RootObj.DeptManager;    //通过根对象创建部门管理对象
            DeptManagerObj.DelDept(DeptName, true); //'删除部门
        }

        public void AddUser( string Username,string depName)
        {
            try
            {
                Inital();
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.AddUser(Username, 0);
                DeptManagerObj = RootObj.DeptManager;
                DeptManagerObj.AddUserToDept(Username, null, depName, false); //添加用户进某个部门,如果用户没有所属部门，源部门名为null即可。
                UserManagerObj.SetUserPwd(Username,"pass");
            }
            catch (COMException ex)
            {

            }
        }

        public void UpdateUser(string UserName,int Empsex,string PhoneNumber )//修改用户
        {
            Inital();
            try
            {
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.SetUserBasicInfo(UserName, "", Empsex, "", "", PhoneNumber, 0);
            }
            catch (COMException ex)
            {
            }
        }

        public void UpdatePassword(string empName,string pwd)//改密码
        {
            Inital();
            try
            {
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.SetUserPwd(empName, pwd);
            }
            catch { }
           
        }
        public void DelUser(string Username)
        {
            try
            {
                Inital();
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.DeleteUser(Username);
            }
            catch (COMException ex)
            {
                
            }
        }

        public bool RTXADDDEPT(int Pdeptid, string Deptid, string name, string info)//添加部门 
        {
            //作用:添加部门 
            //参数说明:Pdeptid:所属部门()上级部门的ID 
            //deptid:增加的该部门的ID 
            //name:该增加部门的名称 
            //info:该增加部门的相关信息 
            #region 
            try
            {
                Inital();

                RTXObjectClass RTXObj = new RTXObjectClass();
                RTXCollectionClass RTXParams = new RTXCollectionClass();
                RTXObj.Name = "USERMANAGER";
                RTXParams.Add("PDEPTID", Pdeptid);
                RTXParams.Add("DEPTID", Deptid);
                RTXParams.Add("NAME", name);
                RTXParams.Add("INFO", info);
                Object iStatus = new Object();
                iStatus = RTXObj.Call2(RTXServerApi.enumCommand_.PRO_ADDDEPT, RTXParams);

                return true;
            }
            catch (Exception E)
            {
                return false;
            }
            #endregion
        }
        public bool RTXDelDEPT(string dpmtid, string delall)//删除部门 
        {
            #region 
            //作用:删除部门 
            //参数说明: 
            //dpmtid:要删除部门的ID号 
            //delall:删除部门的下属部门的选择(0为不删除,为删除) 
            try
            {
                Inital();

                RTXObjectClass RTXObj = new RTXObjectClass();
                RTXCollectionClass RTXParams = new RTXCollectionClass();
                RTXObj.Name = "USERMANAGER";
                RTXParams.Add("DEPTID", dpmtid);
                RTXParams.Add("COMPLETEDELBS", delall);
                Object iStatus = new Object();
                iStatus = RTXObj.Call2(RTXServerApi.enumCommand_.PRO_DELDEPT, RTXParams);

                return true;
            }
            catch (Exception E)
            {
                return false;
            }
            #endregion
        }
        public bool RTXADDUSER(string Dpmid, string Nick, string pwd, string name, string rtxnumber, string mobile)//添加用户 
        {
            #region 
            //作用:添加用户 
            //参数说明: 
            //Dpmid:用户所属于的ID号 
            //Nick:用户的登陆名 
            //pwd:用户的登陆密码 
            //name:用户名 
            //rtxnumber:用户的RTX号码 
            //mobile:用户的手机号码 
            try
            {
                Inital();

                RTXObjectClass RTXObj = new RTXObjectClass();
                RTXCollectionClass RTXParams = new RTXCollectionClass();
                RTXObj.Name = "USERMANAGER";
                RTXParams.Add("DEPTID", Dpmid);
                RTXParams.Add("NICK", Nick);
                RTXParams.Add("PWD", pwd);
                RTXParams.Add("NAME", name);
                RTXParams.Add("UIN", rtxnumber);
                RTXParams.Add("MOBILE", mobile);
                Object iStatus = new Object();
                iStatus = RTXObj.Call2(RTXServerApi.enumCommand_.PRO_ADDUSER, RTXParams);

                return true;
            }
            catch (Exception E)
            {
                return false;
            }
            //帮助来自http://www.joozone.com/
            #endregion
        }
        public bool RTXDelUSR(string unick)//删除用户 
        {
            #region 
            //作用:删除用户 
            //参数说明:unick:用户的登陆名或用户的RTX号码都可 
            try
            {
                Inital();

                RTXObjectClass RTXObj = new RTXObjectClass();
                RTXCollectionClass RTXParams = new RTXCollectionClass();
                RTXObj.Name = "USERMANAGER";
                RTXParams.Add("USERNAME", unick);
                Object iStatus = new Object();
                iStatus = RTXObj.Call2(RTXServerApi.enumCommand_.PRO_DELUSER, RTXParams);
                return true;
            }
            catch (Exception E)
            {
                return false;
            }
            #endregion
        }
        public void SynchDept()
        {
            //DataTable dt = baseData.GetOrderedBaseDataListByBaseNameEng("department");
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        try
            //        {
            //            DeleteDept(dt.Rows[i]["BaseName"].ToString());
            //        }
            //        catch { }
            //        RTXADDDEPT(0, dt.Rows[i]["BaseID"].ToString(), dt.Rows[i]["BaseName"].ToString(), "");
            //    }
            //}

            List<DataModel.Models.BaseData> listDept = baseData.getDeparmentList();


            if (listDept.Count> 0)
            {
                foreach (DataModel.Models.BaseData deptInfo in listDept)
                {
                    try
                    {
                        DeleteDept(deptInfo.BaseName);
                    }
                    catch { }
                    try
                    {
                        RTXADDDEPT(0, deptInfo.BaseID.ToString(), deptInfo.BaseName, "");
                    }
                    catch { }
                    
                }
            }
        }
        private void SetUserBasicInfo(string Username, string ChsName)
        {
            UserManagerObj.SetUserBasicInfo(Username, ChsName, 0, "", "", "", 0);
        }
        public void SynchUser(string Username, string ChsName, string depName)
        {
            try
            {
                Inital();
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.AddUser(Username, 0);



                DeptManagerObj = RootObj.DeptManager;
                DeptManagerObj.AddUserToDept(Username, null, depName, false); //添加用户进某个部门,如果用户没有所属部门，源部门名为null即可。
                UserManagerObj.SetUserPwd(Username, "pass");
                UserManagerObj = RootObj.UserManager;   //通过根对象创建用户对象
                UserManagerObj.SetUserBasicInfo(Username, ChsName, 0, "", "", "", 0);

            }
            catch (COMException ex)
            {

            }
        }
        public void LinkSynchDeptEmp()
        {
            //DataTable dt = baseEmp.GetBaseEmployeeList();
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (dt.Rows[i]["EmpDepID"].ToString()!="0")
            //        {
            //            try
            //            {
            //                RTXDelUSR(dt.Rows[i]["EmpName"].ToString());
            //            }
            //            catch { }
            //           // AddUser(dt.Rows[i]["EmpName"].ToString(), dt.Rows[i]["DepName"].ToString());
            //            SynchUser(dt.Rows[i]["EmpLogin"].ToString(), dt.Rows[i]["EmpName"].ToString(), dt.Rows[i]["DepName"].ToString());
            //        }

            //    }
            //}

            SynchDept();

            List<DataModel.Models.BaseEmployee> EmpList = baseEmp.GetAllEmpList();
            if (EmpList.Count>0)
            {
                foreach (DataModel.Models.BaseEmployee empInfo in EmpList)
                {
                    try
                    {
                        RTXDelUSR(empInfo.EmpName);
                    }
                    catch { }
                    SynchUser(empInfo.EmpLogin, empInfo.EmpName, empInfo.EmpDepName);
                }
            }
        }
        public void SetDeptNam(string OldDeptName,string NewDeptName)
        {
            Inital();
            DeptManagerObj = RootObj.DeptManager;
            try
            {
                DeptManagerObj.SetDeptName(OldDeptName, NewDeptName); //'设置部门名称
            }
            catch (COMException ex)
            {

            }
        }

    }
}
