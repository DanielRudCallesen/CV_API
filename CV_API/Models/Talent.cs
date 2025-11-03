using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CV_API.Models
{
    public class Talent
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = default;
        [Required]
        public string Title { get; set; } = default;

        [JsonPropertyName("profile_text")]
        public string? ProfileText { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public Uri? Github { get; set; }
        public Uri? LinkedIn { get; set; }
    }
}
