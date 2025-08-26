using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int PaymentId { get; set; } // PK
    public double Amount { get; set; } // Amount paid
    public DateTime PaymentDate { get; set; } // Date of payment
    public string Status { get; set; } = "Pending";

    public required int OrderId { get; set; }
    public required Order Order { get; set; }
    public int? CardId { get; set; } // FK to CreditCard
    public CreditCard? CreditCard { get; set; } // Navigation property to the associated credit card

}