using System;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_entityframework.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}
