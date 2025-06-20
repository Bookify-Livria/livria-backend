using LivriaBackend.users.Domain.Model.Aggregates; 
using System;

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    public class Post
    {
        public int Id { get; private set; }
        public int CommunityId { get; private set; } 
        public int UserId { get; private set; }      

        public string Username { get; private set; }

        public string Content { get; private set; }
        public string Img { get; private set; }
        public DateTime CreatedAt { get; private set; } 

        public Community Community { get; private set; }
        public UserClient UserClient { get; private set; } 

        protected Post() { }

        public Post(int communityId, int userId, string username, string content, string img)
        {
            CommunityId = communityId;
            UserId = userId;
            Username = username;
            Content = content;
            Img = img;
            CreatedAt = DateTime.UtcNow; 
        }

        public void Update(string content, string img)
        {
            Content = content;
            Img = img;
        }
    }
}