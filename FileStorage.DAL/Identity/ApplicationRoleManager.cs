﻿using FileStorage.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FileStorage.DAL.Identity
{
    /// <summary>
    /// Manage roles.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}