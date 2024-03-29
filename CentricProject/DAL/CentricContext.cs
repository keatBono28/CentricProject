﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentricProject.Models;
using System.Data.Entity;


namespace CentricProject.DAL
{
    public class CentricContext : DbContext
    {
        public CentricContext() : base("name=DefaultConnection")
        {
            // Migration point will go here
        }
        public DbSet<RecognitionModel> Recognitions { get; set; }

        public System.Data.Entity.DbSet<CentricProject.Models.ProfileDetails> ProfileDetails { get; set; }
    }
}