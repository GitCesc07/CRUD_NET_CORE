using CRUDNET_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDNET_MVC.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }

        //Agregar los modelos, cada modelo corresponde a un tabla de la base detos
        public DbSet<Contactos> Contactos { get; set; }
    }
}
