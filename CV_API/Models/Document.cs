namespace CV_API.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default;
        public string Content { get; set; } = default;
    }
}
