using System.Web.Mvc;
// <copyright file="ModelBindingContextFactory.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;

namespace System.Web.Mvc
{
    /// <summary>A factory for System.Web.Mvc.ModelBindingContext+UnvalidatedValueProviderWrapper instances</summary>
    public static partial class ModelBindingContextFactory
    {
        /// <summary>A factory for System.Web.Mvc.ModelBindingContext+UnvalidatedValueProviderWrapper instances</summary>
        [PexFactoryMethod(typeof(IView), "System.Web.Mvc.ModelBindingContext+UnvalidatedValueProviderWrapper")]
        public static object CreateUnvalidatedValueProviderWrapper()
        {
            throw new NotImplementedException();

            // TODO: Edit factory method of UnvalidatedValueProviderWrapper
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
