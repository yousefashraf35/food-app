using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int PaymentId { get; set; } // PK

    public decimal Amount { get; set; } // Amount paid

    public DateTime PaymentDate { get; set; } // Date of payment

    public string Status { get; set; } // Status of the payment (e.g., Completed, Pending)

    public int OrderId { get; set; }
    public Order Order { get; set; }


    public int CardId { get; set; } // FK to CreditCard
    public CreditCard CreditCard { get; set; } // Navigation property to the associated credit card

}