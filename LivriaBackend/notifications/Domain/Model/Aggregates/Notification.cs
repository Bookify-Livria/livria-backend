using System;
using LivriaBackend.notifications.Domain.Model.ValueObjects; 

namespace LivriaBackend.notifications.Domain.Model.Aggregates
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public ENotificationType Type { get; private set; } 
        public string Title { get; private set; }
        public string Content { get; private set; }

        
        protected Notification() { }

        
        public Notification(ENotificationType type, DateTime date)
        {
            Date = date;
            Type = type;
            SetTitleAndContentByType(type); 
        }

        
        private void SetTitleAndContentByType(ENotificationType type)
        {
            switch (type)
            {
                case ENotificationType.Welcome:
                    Title = "Welcome to Livria!";
                    Content = "We're thrilled to have you here.";
                    break;
                case ENotificationType.Login:
                    Title = "Welcome back!";
                    Content = "Nice to see you again. Let’s continue exploring.";
                    break;
                case ENotificationType.Order:
                    Title = "Order Received";
                    Content = "Thanks for your order! It's being processed.";
                    break;
                case ENotificationType.Plan:
                    Title = "You just subscribed to a community plan.";
                    Content = "Enjoy the perks!";
                    break;
                case ENotificationType.Like:
                    Title = "Recent Likes";
                    Content = "Wow! Your recent post got a lot of likes! Wanna see?";
                    break;
                case ENotificationType.Default:
                default:
                    Title = "Notification";
                    Content = "An event just occurred.";
                    break;
            }
        }

        
        public void SetDateToNow()
        {
            Date = DateTime.UtcNow; 
        }

        
    }
}