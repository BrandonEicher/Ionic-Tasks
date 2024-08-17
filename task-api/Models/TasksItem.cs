using System.ComponentModel.DataAnnotations;

public class TasksItem
{
    [Key]
    public long TaskId { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
}