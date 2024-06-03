namespace FinalAPI.Models
{
    public class Profile
    {
        public int Id { get; init; }
        public String Name { get; set; } = String.Empty;
        public String Biography { get; set; } = String.Empty;

        public override String ToString()
        {
            return $"{Id}: {Name} | {Biography[..12]}";
        }
    }
}
