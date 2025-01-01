﻿using Microsoft.EntityFrameworkCore;
using BlogAppAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ChangeLog> ChangeLogs { get; set; }
}
