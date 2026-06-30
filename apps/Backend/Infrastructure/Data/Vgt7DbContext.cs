using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Vgt7DbContext(DbContextOptions<Vgt7DbContext> options) : DbContext(options)
{
    public DbSet<PromptTemplate> PromptTemplates => Set<PromptTemplate>();
}