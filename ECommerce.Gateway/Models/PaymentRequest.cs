namespace ECommerce.Gateway.Models;

public class PaymentRequest
{
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string ValidDate { get; set; }
    public string Cvv { get; set; }
    public decimal Amount { get; set; }
}