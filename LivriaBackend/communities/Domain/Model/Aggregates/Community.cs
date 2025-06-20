using System.Collections.Generic;
using LivriaBackend.users.Domain.Model.Aggregates; 

namespace LivriaBackend.communities.Domain.Model.Aggregates
{

    public class Community
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string Image { get; private set; }
        public string Banner { get; private set; }

        public ICollection<Post> Posts { get; private set; } = new List<Post>(); 

        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();


        private Community()
        {
            Posts = new List<Post>(); 
            UserCommunities = new List<UserCommunity>(); 
        }


        public Community(string name, string description, string type, string image, string banner)
        {
            Name = name;
            Description = description;
            Type = type;
            Image = image;
            Banner = banner;
            Posts = new List<Post>(); 
            UserCommunities = new List<UserCommunity>(); 
        }


        public void AddPost(Post post)
        {
            if (post != null && !Posts.Contains(post))
            {
                Posts.Add(post);
            }
        }

       
        public void AddUser(UserClient userClient)
        {
            if (userClient != null && !UserCommunities.Any(uc => uc.UserClientId == userClient.Id))
            {
                UserCommunities.Add(new UserCommunity(userClient.Id, this.Id));
            }
        }
    }
}