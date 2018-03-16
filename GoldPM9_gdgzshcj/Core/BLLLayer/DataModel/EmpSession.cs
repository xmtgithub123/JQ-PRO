using System;

namespace DataModel
{
    /// <summary>
    /// EmpSession ��ժҪ˵����
    /// </summary>
    [Serializable]
    public class EmpSession
    {
        /// <summary>
        /// ��¼�û�ID
        /// </summary>
		public int EmpID { get; set; }

        /// <summary>
        /// ��¼�û�����
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// ��¼�û��Ĳ���
        /// </summary>
        public int EmpDepID { get; set; }
        /// <summary>
        /// ��¼�û��Ĳ���
        /// </summary>
        public string EmpDepName { get; set; }
        /// <summary>
        /// ��¼�û��ķ�ҳ
        /// </summary>
        public int EmpPageSize { get; set; }

        /// <summary>
        /// �������õ�Ƥ��
        /// </summary>
        public string EmpThemesName { get; set; }

        /// <summary>
        /// ���˵���ʽ
        /// </summary>
        public string EmpMenuType { get; set; }

        /// <summary>
        /// ��������̷�
        /// </summary>
        public string EmpDisk { get; set; }


        /// <summary>
        /// �Ƿ񱾻�IP-MARK
        /// </summary>
        public string EmpMacAddress { get; set; }


        /// <summary>
        /// �����½ EmpID
        /// </summary>
        public int AgenEmpID { get; set; }

        /// <summary>
        /// �����½ DepID
        /// </summary>
        public int AgenDepID { get; set; }

        /// <summary>
        /// �����½ EmpName
        /// </summary>
        public string AgenEmpName { get; set; }


        /// <summary>
        /// �����½ DepName
        /// </summary>
        public string AgenDepName { get; set; }

        /// <summary>
        /// �����½ ��� ��0,1,2��
        /// -1 ȫ��
        /// 0 ��ί�е�¼
        /// 1 �˵�
        /// 2 ����
        /// </summary>
        public int AgenTypeID { get; set; }

        /// <summary>
        /// ����˵�
        /// </summary>
        public string AgenMenu { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string AgenFlow { get; set; }

        /// <summary>
        /// ��֤��Ч����
        /// </summary>
        public bool EmpIsPay { get; set; }

        /// <summary>
        /// ��¼�û�IP
        /// </summary>
        public string LoginIP { get; set; }

        /// <summary>
        /// ������˾
        /// </summary>
        public int CompanyID { get; set; }
    }

}
