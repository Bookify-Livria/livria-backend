using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.communities.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new Community.
    /// </summary>
    public record CreateCommunityResource(
        [Required] [StringLength(100)] string Name,
        [Required] [StringLength(500)] string Description,
        [Required] string Type,
        string Image,
        string Banner
    );
}