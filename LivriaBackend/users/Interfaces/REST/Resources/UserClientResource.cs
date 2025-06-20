using System.Collections.Generic;

namespace LivriaBackend.users.Interfaces.REST.Resources
{
    
    public record UserClientResource(
        int Id,
        string Display,
        string Username,
        string Email,
        string Icon,
        string Phrase,
        List<int> Order, 
        string Subscription
    ) : UserResource(Id, Display, Username, Email); 
}