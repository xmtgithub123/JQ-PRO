using System.Web;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using System.Collections;
 namespace Common
{
    public class SessionService
    {
        public static void InsertObject(string Key, object Obj)
        {
            if ((Obj != null))
            {
                if (HttpContext.Current.Session[Key] == null)
                {
                    HttpContext.Current.Session[Key] = Obj;
                }
            }
        }

        public static object GetObject(string Key)
        {
            return HttpContext.Current.Session[Key];
        }

        public static void RemoveObject(string Key)
        {
            if ((HttpContext.Current.Session[Key] != null))
            {
                HttpContext.Current.Session[Key] = null;
            }
        }
    }

}
