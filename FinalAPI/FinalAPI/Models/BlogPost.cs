namespace FinalAPI.Models
{
    public class BlogPost
    {
        public int Id { get; init; }
        public Profile PosterAccount { get; set; } = null!;
        public String Body { get; set; } = String.Empty;
        public DateTime DateTimePosted { get; set; }
        public int Likes { get; set; }
        public List<String> Comments { get; set; } = [];
    }
}