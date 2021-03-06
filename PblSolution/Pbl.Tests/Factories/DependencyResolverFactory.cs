using System.Web.Mvc;
// <copyright file="DependencyResolverFactory.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;

namespace System.Web.Mvc
{
    /// <summary>A factory for System.Web.Mvc.DependencyResolver+CacheDependencyResolver instances</summary>
    public static partial class DependencyResolverFactory
    {
        /// <summary>A factory for System.Web.Mvc.DependencyResolver+CacheDependencyResolver instances</summary>
        [PexFactoryMethod(typeof(IView), "System.Web.Mvc.DependencyResolver+CacheDependencyResolver")]
        public static object CreateCacheDependencyResolver()
        {
            throw new NotImplementedException();

            // TODO: Edit factory method of CacheDependencyResolver
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
        /// <summary>A factory for System.Web.Mvc.DependencyResolver+DelegateBasedDependencyResolver instances</summary>
        [PexFactoryMethod(typeof(IView), "System.Web.Mvc.DependencyResolver+DelegateBasedDependencyResolver")]
        public static object CreateDelegateBasedDependencyResolver()
        {
            throw new NotImplementedException();

            // TODO: Edit factory method of DelegateBasedDependencyResolver
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
