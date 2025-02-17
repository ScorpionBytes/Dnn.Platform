﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information

namespace DotNetNuke.Web.Api
{
    using System;
    using System.Web.Http;

    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;

    /// <summary>Defines properties and methods for Dnn specific API controllers.</summary>
    [DnnExceptionFilter]
    public abstract class DnnApiController : ApiController
    {
        private readonly Lazy<ModuleInfo> activeModule;

        /// <summary>Initializes a new instance of the <see cref="DnnApiController"/> class.</summary>
        protected DnnApiController()
        {
            this.activeModule = new Lazy<ModuleInfo>(this.InitModuleInfo);
        }

        /// <summary>Gets portalSettings for the current portal.</summary>
        public PortalSettings PortalSettings
        {
            get
            {
                return PortalController.Instance.GetCurrentPortalSettings();
            }
        }

        /// <summary>Gets userInfo for the current user.</summary>
        public UserInfo UserInfo
        {
            get { return this.PortalSettings.UserInfo; }
        }

        /// <summary>
        /// Gets moduleInfo for the current module.
        /// <remarks>Will be null unless a valid pair of module and tab ids were provided in the request</remarks>
        /// </summary>
        public ModuleInfo ActiveModule
        {
            get { return this.activeModule.Value; }
        }

        private ModuleInfo InitModuleInfo()
        {
            return this.Request.FindModuleInfo();
        }
    }
}
