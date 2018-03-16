using System.Net;
using System.Text;

namespace JQ.Util
{
    /// <summary>
    /// Http助手
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// 请求Http
        /// </summary>
        /// <param name="paramterString">参数字符串</param>
        /// <param name="requestUrl">访问地址</param>
        /// <param name="methodType">请求方式</param>
        /// <param name="dataEncoding">编码格式</param>
        /// <param name="header">标头</param>
        /// <returns></returns>
        public static string requestByHead(string paramterString, string requestUrl, string methodType, Encoding dataEncoding, string header)
        {
            byte[] requestData = dataEncoding.GetBytes(paramterString);
            //继承重写时间
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", header);
            byte[] responseData = webClient.UploadData(requestUrl, methodType, requestData);
            return dataEncoding.GetString(responseData);
        }
    }
}
