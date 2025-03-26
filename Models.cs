namespace TodoApp.Models
{
    public class Task2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IsCompleted { get; set; }
        public Task2(int id, string title, string isCompleted = "Pending")
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }
}