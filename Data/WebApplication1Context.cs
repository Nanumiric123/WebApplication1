using System.Data.Entity;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public WebApplication1Context() : base("name=WebApplication1Context")
        {
        }

        public System.Data.Entity.DbSet<WebApplication1.Models.KANBAN_MASTER> KANBAN_MASTER { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.SUPERMARKET_SLIDER> SUPERMARKET_SLIDER { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.SUPERMARKET_LINE_CAPACITY> SUPERMARKET_LINE_CAPACITY { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.PACKAGING_INFO> PACKAGING_INFO { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.BS_BIN_REGISTER> BS_BIN_REGISTER { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.SMT_PULLLIST> SMT_PULLLIST { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.RACKS_WAREHOUSE> RACKS_WAREHOUSE { get; set; }

    }
}
