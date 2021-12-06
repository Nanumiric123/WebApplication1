using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Data
{
    public class IbsuinessContext : DbContext
    {
        public IbsuinessContext() : base("name=WebApplication1Context2")
        {
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.LOGIN_INFO> LOGIN_INFO { get; set; }
    }
}