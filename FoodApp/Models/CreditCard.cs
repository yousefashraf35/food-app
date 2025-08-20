using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CreditCard
{
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    [Key]
    public int CardId { get; set; } // PK
    public string CardNumber { get; set; } // Card number
    public string CardHolderName { get; set; } // Name on the card
    public DateTime ExpiryDate { get; set; } // Expiry date of the card

    // FK: User
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}