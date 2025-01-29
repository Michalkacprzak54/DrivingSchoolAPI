using System;
using System.ComponentModel.DataAnnotations;

public class PaymentRequest
{
    [Required]
    public int InvoiceId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Method { get; set; }
}
