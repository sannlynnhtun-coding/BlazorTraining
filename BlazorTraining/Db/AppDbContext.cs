using BlazorTraining.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTraining.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<BlogDataModel> Blogs { get; set; }
}
