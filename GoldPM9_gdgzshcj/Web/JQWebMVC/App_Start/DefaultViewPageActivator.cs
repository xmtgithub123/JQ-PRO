﻿using System;
using System.Web.Mvc;

namespace JQWebMVC.App_Start
{
    internal class DefaultViewPageActivator : IViewPageActivator
    {
        private readonly Func<IDependencyResolver> _resolverThunk;

        public DefaultViewPageActivator()
            : this(null)
        {
        }

        public static DefaultViewPageActivator Current { get { return Nested.Instance; } }

        public DefaultViewPageActivator(IDependencyResolver resolver)
        {
            if (resolver == null)
                _resolverThunk = () => DependencyResolver.Current;
            else
                _resolverThunk = () => resolver;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            return _resolverThunk().GetService(type) ?? Activator.CreateInstance(type);
        }

        private static class Nested
        {
            static Nested()
            {
            }

            internal static readonly DefaultViewPageActivator Instance = new DefaultViewPageActivator();
        }
    }
}
