using Users.Domain.Common;

namespace Users.Domain.ValueObjects;
public class Email : ValueObject
{
    public const int MaxEmailLength = 100;

    public string Address { get; private set; } = null!;

    public Email(string value)
    {
        if (value.Length > MaxEmailLength)
            throw new Exception($"The provided e-mail must be smaller or equal to {MaxEmailLength}");

        if (!System.Net.Mail.MailAddress.TryCreate(value, out _))
            throw new Exception("The provided email address is not valid.");

        Address = value;
    }

    public static implicit operator string(Email email) => email.Address;

    public static implicit operator Email(string address) => new(address);

    public override string ToString() => Address;

    protected Email() { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}
