namespace MVCBasic.Models
{
    public class Book
    {
        public Int64 Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateTime Date { get; set; }
        public string? Photo { get; set; }
    }
}
