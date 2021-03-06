﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.ComponentModelHost;

namespace Microsoft.VisualStudio.ProjectSystem.VS
{
    internal static class IServiceProviderExtensions
    {
        /// <summary>
        /// Returns the specified interface from the service. This is useful when the service and interface differ
        /// </summary>
        public static InterfaceType GetService<InterfaceType, ServiceType>(this IServiceProvider sp)
            where InterfaceType : class
            where ServiceType : class
        {
            return (InterfaceType)sp.GetService(typeof(ServiceType));
        }

        /// <summary>
        /// Returns the specified service type from the service.
        /// </summary>
        public static ServiceType GetService<ServiceType>(this IServiceProvider sp) where ServiceType : class
        {
            return sp.GetService<ServiceType, ServiceType>();
        }

        /// <summary>
        /// Returns IProjectService a global scope component that provdes data accross all CPS projects
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static IProjectService GetProjectService(this IServiceProvider sp)
        {
            var componentModel = sp.GetService(typeof(SComponentModel)) as IComponentModel;
            if (componentModel == null)
            {
                return null;
            }

            var projectServiceAccessor = componentModel.GetService<IProjectServiceAccessor>();
            if (projectServiceAccessor == null)
            {
                return null;
            }

            return projectServiceAccessor.GetProjectService();
        }
    }
}
