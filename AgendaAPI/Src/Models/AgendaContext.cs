using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Src.Models
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
        }
        public DbSet<Contact> Contact { get; set; }
    }
}
