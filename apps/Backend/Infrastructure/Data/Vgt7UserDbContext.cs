using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Vgt7UserDbContext(DbContextOptions<Vgt7UserDbContext> options) : DbContext(options)
{
    public DbSet<Vgt7User> Users => Set<Vgt7User>();
}