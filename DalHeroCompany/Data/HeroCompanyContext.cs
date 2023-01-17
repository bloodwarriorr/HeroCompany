using DalHeroCompany;
using DalHeroCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeroCompany.Data
{
    public class HeroCompanyContext : DbContext
    {
      //Db context class for migration
    
        public HeroCompanyContext() : base("Data Source=DESKTOP-AQUR41C;Initial Catalog=heroCompany;Integrated Security=True")
        {
        }

        public System.Data.Entity.DbSet<Trainer> Trainers { get; set; }

        public System.Data.Entity.DbSet<Hero> Heroes { get; set; }
    }
}
