namespace LivriaBackend.IAM.Domain.Model.Aggregates;
using LivriaBackend.IAM.Domain.Model.ValueObjects;

/// <summary>
/// Clase que controla el acceso a una cuenta de usuario
/// Contiene los campos asociados al proceso de login y verificación de identidad
/// </summary>

public class Identity
{
    public int Id { get; private set; } 
    
    /// <summary>
    /// La Id del usuario asociado a la Identity
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// El username único asociado a la Identity
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// La contraseña asociada a la Identity
    /// </summary>
    public PasswordHash HashedPassword { get; set; }
    
    /// <summary>
    /// Constructor sin parámetros
    /// </summary>
    
    protected Identity() { }

    /// <summary>
    /// Constructor con parámetros para Identity
    /// </summary>
    /// <param name="userid">Id de usuario asociado</param>
    /// <param name="username">Username de usuario asociado</param>
    /// <param name="hashedPassword">Contraseña encriptada de usuario asociado</param>
    public Identity(int userid, string username, string hashedPassword)
    {
        UserId = userid;
        UserName = username;
        HashedPassword = new PasswordHash(hashedPassword);
    }

    /// <summary>
    /// Método protegido para actualizar las propiedades de Identity
    /// </summary>
    /// <param name="username"></param>
    /// <param name="hashedPassword"></param>
    protected void UpdateIdentity(string username, string hashedPassword)
    {
        UserName = username;
        HashedPassword = new PasswordHash(hashedPassword);
    }
    
    public bool VerifyPassword(string plainTextPassword)
    {
        if (HashedPassword == null) return false;
        return HashedPassword.Matches(plainTextPassword);
    }
}