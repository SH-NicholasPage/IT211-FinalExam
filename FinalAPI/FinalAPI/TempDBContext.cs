using FinalAPI.Models;

namespace FinalAPI
{
    public class TempDBContext
    {
        public static List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public static List<Profile> Profiles { get; set; } = new List<Profile>();

        public static void FillBlogsWithDummyData()
        {
            BlogPosts.AddRange(new List<BlogPost>{ new BlogPost
            {
                Id = 1,
                PosterAccountId = 1,
                Body = "This is a test blog post",
                DateTimePosted = DateTime.Parse("2021-01-01 12:00:00"),
                Likes = 12,
                Comments = new List<String>()
                {
                    "This is a test comment",
                    "This is another test comment"
                }
            },
            new BlogPost
            {
                Id = 2,
                PosterAccountId = 1,
                Body = "This is another test blog post",
                DateTimePosted = DateTime.Parse("2022-01-01 12:00:00"),
                Likes = 9,
                Comments = new List<String>()
                {
                    "This is a test comment",
                    "This is another test comment"
                }
            },
            new BlogPost
            {
                Id = 3,
                PosterAccountId = 1,
                Body = "This is a third test blog post",
                DateTimePosted = DateTime.Parse("2023-01-01 12:00:00"),
                Likes = 8,
                Comments = new List<String>()
                {
                    "This is a test comment",
                    "This is another test comment"
                }
            },
            new BlogPost
            {
                Id = 4,
                PosterAccountId = 2,
                Body = "Wife took my fishing rods >:(",
                DateTimePosted = DateTime.Parse("2020-02-12 13:01:56"),
                Likes = 30,
                Comments = new List<String>()
                {
                    "Haha!",
                    "Can't fish without rods!!"
                }
            },
            new BlogPost
            {
                Id = 5,
                PosterAccountId = 2,
                Body = "gots me beear in my beard. hahaha fishing 2 btw!!",
                DateTimePosted = DateTime.Parse("2020-04-08 16:48:32"),
                Likes = 3354,
                Comments = new List<String>()
                {
                    "Golly you're so funny John!!",
                    "Wow, that's awesome! I wish I could do that!",
                    "I'll never look at fishing the same way again!",
                    "Lol, is that what they call it now?? Fishin is the best tho!!",
                    "LMAO, looks like you're having a good time! XD",
                    "You should have seen me the other day, I was fishin' like a pro!",
                    "That's so cool, wish I could do that too!"
                }
            },
            new BlogPost
            {
                Id = 6,
                PosterAccountId = 2,
                Body = "HEYUP, whered all these dang people come from",
                DateTimePosted = DateTime.Parse("2020-04-09 08:11:10"),
                Likes = 544843,
                Comments = new List<String>()
                {
                    "heyy sis, i know rite! so many ppl these days",
                    "^^^ I AIN'T UR SIS ^^^",
                    "omg lol so many peeps out here these days",
                    "yeaa same, dunt kno where dey came frum",
                    "hahaha twas a surprise to me too",
                }
            },
            new BlogPost
            {
                Id = 7,
                PosterAccountId = 3,
                Body = "I'm a cat haha jkjk. Peace be with you all.",
                DateTimePosted = DateTime.Parse("2019-12-10 07:10:58"),
                Likes = 7,
                Comments = new List<String>()
                {
                    "LMAO so funny! Stay blessed!",
                    "That's adorable! Hope you're havin a great day Jane",
                    "Cats are the best! Wishin u lots of joy",
                    "call later Jnae",
                    "Lol that's hilarious! Sending lots of love ur way",
                    "Hahaha that's so true! Peace and love xoxo"
                }
            },
            new BlogPost
            {
                Id = 8,
                PosterAccountId = 3,
                Body = "siri go to amazon google google google google recipies google google chicken rice caserol yahoo is how to make chicken rice stop quit end stop typing what i say",
                DateTimePosted = DateTime.Parse("2019-12-10 07:10:58"),
                Likes = 2,
                Comments = new List<String>()
                {
                    "Mom, this is not Google. Please go to google.com",
                    "^^^ THANK ^^^",
                    "do u have a link to the recipe?",
                    "^^^ NO ^^^"
                }
            },
            new BlogPost
            {
                Id = 9,
                PosterAccountId = 3,
                Body = "husband is internet famous now!!! best, jane",
                DateTimePosted = DateTime.Parse("2020-04-09 12:51:25"),
                Likes = 19,
                Comments = new List<String>()
                {
                    "That's so cool Jane! Congrats to you both!",
                    "Woohoo!!! Congrats to ya'll!!!",
                    "Congrats! Bet your husband is loving it!",
                    "Awwww this is amazin!! Good job hubby!",
                    "Woowee! Congrats on your hubby's internet fame!",
                    "Yayyyyyy! Congrats Jane and hubby!"
                }
            }
            });
        }

        public static void FillWithDummyProfiles()
        {
            Profiles.AddRange(new List<Profile>{ new Profile
            {
                Id = 1,
                Name = "Test User",
                Bio = "I'm just a simple guy doing simple tests."
            },
            new Profile
            {
                Id = 2,
                Name = "John Doe",
                Bio = "My name is John and I like to fish. That's all. My wife is Jane Doe."
            },
            new Profile
            {
                Id = 3,
                Name = "Jane Doe",
                Bio = "Hi! I'm Jane Doe! I'm an elderly lady who loves nature and cats. I have a passion for reading, cooking and spending time with my grandkids. I'm always up for a good laugh!"
            }});
        }
    }
}
