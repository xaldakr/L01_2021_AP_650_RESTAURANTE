using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2021_AP_650.Models{
    public class restauranteContext:DbContext{
        public restauranteContext(DbContextOptions<restauranteContext> options) : base (options){

        }
        public DbSet<pedidos> pedidos {get; set;}
        public DbSet<platos> platos {get; set;}
        public DbSet<motoristas> motoristas {get; set;}
    }
}