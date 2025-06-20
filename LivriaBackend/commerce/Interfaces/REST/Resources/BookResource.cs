using System.Collections.Generic;
using LivriaBackend.commerce.Interfaces.REST.Resources; 

namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record BookResource(
        int Id,
        string Title,
        string Description,
        string Author,
        decimal Price,
        int Stock,
        string Cover,
        string Genre,
        string Language,
        IEnumerable<ReviewResource> Reviews 
    );
}