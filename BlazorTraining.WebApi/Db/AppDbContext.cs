using BlazorTraining.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace BlazorTraining.WebApi.Db;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<BlogDataModel> Blogs { get; set; }
}
