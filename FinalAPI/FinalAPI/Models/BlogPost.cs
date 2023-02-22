namespace FinalAPI.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public Profile PosterAccount { get; set; } = null!;//lmao
        public String Body { get; set; } = String.Empty;
        public DateTime DateTimePosted { get; set; }
        public int Likes { get; set; }
        public List<String> Comments { get; set; } = new List<String>();
    }
}