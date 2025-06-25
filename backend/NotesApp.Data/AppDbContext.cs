using Microsoft.EntityFrameworkCore;
using NotesApp.Data.Entities;

namespace NotesApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Counter> Counters { get; set; }
}