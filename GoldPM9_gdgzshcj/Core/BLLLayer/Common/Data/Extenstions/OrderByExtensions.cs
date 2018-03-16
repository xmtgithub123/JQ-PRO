using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
namespace Common.Data.Extenstions
{
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyNames) where T : class
        {
            string[] propertyNameArray = propertyNames.Split(';');
            bool IsSecondOrder = false;
            for (int i = 0; i < propertyNameArray.Length; i++)
            {
                string[] propertyArray = propertyNameArray[i].Split(',');
                string propertyName = propertyArray[0];
                bool ascending = true;

                if (propertyArray.Count() > 1)
                {
                    if (propertyArray[1].ToLower().Trim() == "desc")
                    {
                        ascending = false;
                    }
                    else
                    {
                        ascending = true;
                    }
                }
                var methodName = !IsSecondOrder ? (ascending ? "OrderBy" : "OrderByDescending") : (ascending ? "ThenBy" : "ThenByDescending");
                IsSecondOrder = true;
                source = ApplyOrder<T>(source, propertyName, methodName);
            }
            return (IOrderedQueryable<T>)source;
        }

        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}