using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Data
{
    public class capacityContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public capacityContext() : base("name=WebApplication1Context")
        {
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.SUPERMARKET_LINE_CAPACITY> SUPERMARKET_LINE_CAPACITY { get; set; }

    }
}