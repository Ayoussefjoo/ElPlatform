﻿using ElPlatform.DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.DAL
{
    public class ElPlatformDbContext : IdentityDbContext<ApplicationUser>
    {
        public ElPlatformDbContext(DbContextOptions<ElPlatformDbContext> options) : base(options)
        {
      
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
