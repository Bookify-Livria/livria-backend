namespace LivriaBackend.communities.Domain.Model.Commands
{
    /// <summary>
    /// Representa un comando para crear una nueva comunidad.
    /// Este comando encapsula todos los datos necesarios para iniciar el proceso de creación de una comunidad.
    /// </summary>
    /// <param name="Name">El nombre único y descriptivo de la comunidad.</param>
    /// <param name="Description">Una breve descripción de la comunidad, indicando su propósito o enfoque.</param>
    /// <param name="Type">El tipo de comunidad (por ejemplo, "Pública", "Privada", "Restringida").</param>
    /// <param name="Image">La URL o ruta a la imagen de perfil de la comunidad.</param>
    /// <param name="Banner">La URL o ruta a la imagen de banner de la comunidad.</param>
    public record CreateCommunityCommand(
        string Name,
        string Description,
        string Type,
        string Image,
        string Banner
    );
}