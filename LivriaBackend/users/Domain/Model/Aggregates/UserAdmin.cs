using System.Text.Json.Serialization;

namespace LivriaBackend.users.Domain.Model.Aggregates;

public class UserAdmin : User
{
    public bool AdminAccess { get; set; }
    public string SecurityPin { get; set; }

    [JsonConstructor]
    public UserAdmin(int id, string display, string username, string email, string password,
        bool adminAccess, string securityPin)
        : base(id, display, username, email, password)
    {
        AdminAccess = adminAccess;
        SecurityPin = securityPin;
    }

    public UserAdmin(string display, string username, string email, string password,
        bool adminAccess, string securityPin)
        : base(display, username, email, password)
    {
        AdminAccess = adminAccess;
        SecurityPin = securityPin;
    }

    protected UserAdmin() : base() { }

    public void Update(string display, string username, string email, string password, bool adminAccess, string securityPin)
    {
        base.Update(display, username, email, password);
        AdminAccess = adminAccess;
        SecurityPin = securityPin;
    }
}