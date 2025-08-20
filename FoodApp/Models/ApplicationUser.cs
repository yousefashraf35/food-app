using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<Address>? Addresses { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<CreditCard> CreditCards { get; set; }
}