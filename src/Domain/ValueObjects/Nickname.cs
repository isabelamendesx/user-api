using Users.Domain.Common;

namespace Users.Domain.ValueObjects;

public class Nickname : ValueObject
{
    public const int MaxNickNameLength = 50;

    public string Value { get; private set; } = null!;

    protected Nickname() { }

    public Nickname(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new Exception("The provided nickname can not be null or white space.");
        }

        if (nickname.Length > MaxNickNameLength)
        {
            throw new Exception($"The provided nickname length must be smaller or equal to {MaxNickNameLength}");
        }

        Value = nickname;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator Nickname(string value) => new(value);
    public static implicit operator string(Nickname nickname) => nickname.Value;
}
