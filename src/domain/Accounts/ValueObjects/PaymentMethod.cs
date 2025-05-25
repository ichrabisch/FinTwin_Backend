namespace Domain.Accounts.ValueObjects;
public enum PaymentMethod
{
    Cash,
    CreditCard,
    DebitCard,
    BankTransfer
}

public sealed record PaymentMethodInfo
{
    public string? Last4 { get; private set; }
    public PaymentMethod Type { get; private set; }

    private PaymentMethodInfo(PaymentMethod type, string? last4)
    {
        Type = type;
        Last4 = last4;
    }

    public static PaymentMethodInfo Create(PaymentMethod type, string? last4 = null)
    {
        if (type == PaymentMethod.Cash && last4 != null)
        {
            throw new ArgumentException("Cash payments should not have last 4 digits.");
        }

        if (type != PaymentMethod.Cash && string.IsNullOrEmpty(last4))
        {
            throw new ArgumentException("Non-cash payments must provide last 4 digits.");
        }

        return new PaymentMethodInfo(type, last4);
    }
}
