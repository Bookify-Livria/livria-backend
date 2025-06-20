namespace LivriaBackend.commerce.Interfaces.REST.Resources
{
    public record CartItemResource(
        int Id,
        
        BookResource Book, 
        
        int Quantity,
        
        int UserClientId 
    );
}