using Microsoft.EntityFrameworkCore;

namespace task_api.Models;

public class TasksContext : DbContext
{
    public TasksContext(DbContextOptions<TasksContext> options)
        : base(options)
    {
    }

    public DbSet<TasksItem> TasksItems { get; set; } = null!;
}