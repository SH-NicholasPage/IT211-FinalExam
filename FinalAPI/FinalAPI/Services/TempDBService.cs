using FinalAPI.Models;
using System.Text.Json;

namespace FinalAPI.Services
{
    public class TempDBService
    {
        private const String POSTS_JSON_FILE = @"Data\posts.json";
        private const String PROFILES_JSON_FILE = @"Data\profiles.json";

        public List<BlogPost> BlogPosts { get; set; } = [];
        public List<Profile> Profiles { get; set; } = [];

        public TempDBService()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Populate BlogPosts and Profiles from JSON files
            Profiles = JsonSerializer.Deserialize<List<Profile>>(File.ReadAllText(PROFILES_JSON_FILE), options)!;

            #region Populate BlogPosts
            List<dynamic> tempPosts = JsonSerializer.Deserialize<List<dynamic>>(File.ReadAllText(POSTS_JSON_FILE))!;

            foreach (dynamic post in tempPosts)
            {
                JsonElement.ArrayEnumerator comments = post.GetProperty("comments").EnumerateArray();

                BlogPosts.Add(new BlogPost
                {
                    Id = post.GetProperty("id").GetInt32(),
                    PosterAccount = Profiles.First(p => p.Id == post.GetProperty("poster_id").GetInt32()),
                    Body = post.GetProperty("body").GetString(),
                    DateTimePosted = DateTime.Parse(post.GetProperty("date_posted").GetString()),
                    Likes = post.GetProperty("likes").GetInt32(),
                    Comments = comments.Select(c => c.GetString()!).ToList()
                });
            }
            #endregion
        }
    }
}
