using System.Collections.Generic;
using LivriaBackend.users.Domain.Model.Aggregates; // Nuevo using para UserClient (para navegación)

namespace LivriaBackend.communities.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a Community aggregate root. Communities can only be created and queried.
    /// </summary>
    public class Community
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string Image { get; private set; }
        public string Banner { get; private set; }

        public ICollection<Post> Posts { get; private set; } = new List<Post>(); // Collection of Posts

        // NEW: Collection for the many-to-many relationship with UserClient via UserCommunity
        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();


        // Constructor for Entity Framework Core (private or protected)
        private Community()
        {
            // Constructor without parameters required by EF Core for hydration
            Posts = new List<Post>(); // Ensure collections are initialized
            UserCommunities = new List<UserCommunity>(); // Initialize the collection
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Community"/> class.
        /// Constructor for creating new communities in the domain.
        /// </summary>
        public Community(string name, string description, string type, string image, string banner)
        {
            Name = name;
            Description = description;
            Type = type;
            Image = image;
            Banner = banner;
            Posts = new List<Post>(); // Initialize the collection
            UserCommunities = new List<UserCommunity>(); // Initialize the collection
        }

        // Domain methods: No explicit "Update" or "Delete" methods are provided
        // as the rule is that they are not updated or deleted.

        // Method to associate posts (if Community "owns" the Posts in the domain)
        public void AddPost(Post post)
        {
            if (post != null && !Posts.Contains(post))
            {
                Posts.Add(post);
            }
        }

        // NEW: Method to add a UserClient to this community (via UserCommunity)
        // This would typically be orchestrated by a domain service or the UserClient aggregate.
        public void AddUser(UserClient userClient)
        {
            if (userClient != null && !UserCommunities.Any(uc => uc.UserClientId == userClient.Id))
            {
                UserCommunities.Add(new UserCommunity(userClient.Id, this.Id));
            }
        }
    }
}