using api_usuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace api_usuarios.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
