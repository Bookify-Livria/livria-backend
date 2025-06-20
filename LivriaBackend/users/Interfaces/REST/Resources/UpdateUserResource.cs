namespace LivriaBackend.users.Interfaces.REST.Resources
{
    public record class UpdateUserResource
    {
      
        public string? Display { get; init; }
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; } 
        public string? Icon { get; init; } 
        public string? Phrase { get; init; } 

        
        public UpdateUserResource(string? display, string? username, string? email, string? password, string? icon, string? phrase)
        {
            Display = display;
            Username = username;
            Email = email;
            Password = password;
            Icon = icon;
            Phrase = phrase;
        }

        
        public UpdateUserResource() { }
    }
}