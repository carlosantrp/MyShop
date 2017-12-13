using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContex : DbContext
    {

        public DataContex()
            :base("DefaultConnection")
            {
            }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        //comandos migrations
        //Enable-Migrations
        //Add-Migration Initial
    }   //Update-Database
}
