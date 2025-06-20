using LivriaBackend.commerce.Domain.Model.Aggregates; 
using LivriaBackend.communities.Domain.Model.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LivriaBackend.users.Domain.Model.Aggregates
{
    public class UserClient : User
    {
        public string Icon { get; private set; }
        public string Phrase { get; private set; }
        public string Subscription { get; private set; }

        
        
        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();

        private readonly List<Book> _favoriteBooks = new List<Book>();
        public IReadOnlyCollection<Book> FavoriteBooks => _favoriteBooks.AsReadOnly();


        protected UserClient() : base()
        {
            
            Orders = new List<Order>(); 
            UserCommunities = new List<UserCommunity>();
        }

        public UserClient(string display, string username, string email, string password, string icon, string phrase, string subscription)
            : base(display, username, email, password)
        {
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
            Orders = new List<Order>();
            UserCommunities = new List<UserCommunity>();
        }

        public void Update(string display, string username, string email, string password, string icon, string phrase, string subscription)
        {
            base.UpdateUserProperties(display, username, email, password);
            Icon = icon;
            Phrase = phrase;
            Subscription = subscription;
        }

        public void JoinCommunity(int communityId)
        {
            if (!UserCommunities.Any(uc => uc.CommunityId == communityId))
            {
                UserCommunities.Add(new UserCommunity(this.Id, communityId));
            }
        }

        public void LeaveCommunity(int communityId)
        {
                var userCommunity = UserCommunities.FirstOrDefault(uc => uc.CommunityId == communityId);
                if (userCommunity != null)
                {
                    UserCommunities.Remove(userCommunity);
                }
        }
        
        public void AddFavoriteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (!_favoriteBooks.Contains(book))
            {
                _favoriteBooks.Add(book);
            }
        }
        
        public void RemoveFavoriteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _favoriteBooks.Remove(book);
        }
    }
}