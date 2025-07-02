using LivriaBackend.communities.Domain.Model.ValueObjects; 

namespace LivriaBackend.communities.Domain.Model.Commands
{
    public record CreateCommunityCommand(
        string Name,
        string Description,
        CommunityType Type, // ¡Modificado a CommunityType!
        string Image, 
        string Banner 
    );

  
}