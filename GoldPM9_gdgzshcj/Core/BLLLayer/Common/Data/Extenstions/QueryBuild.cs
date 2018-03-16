using System;
using System.Linq.Expressions;

namespace Common.Data.Extenstions
{
    public static class QueryBuild<T> where T : class
    {
        public static Expression<Func<T, bool>> True()
        {
            return c => true;
        }

        public static Expression<Func<T, bool>> False()
        {
            return c => false;
        }
    }
}