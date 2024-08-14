using Users.Domain.Common;

namespace Users.Domain.ValueObjects;

public class Phone : ValueObject
{
    public const int MinIddLength = 1;
    public const int MaxIddLength = 4;
    public const int MaxNumberLength = 15;
    public const char MinIddValue = '0';

    public string IDD { get; private set; } = null!;
    public string Number { get; private set; } = null!;

    public string GetFullPhone() => $"+{IDD} {Number}";
    protected Phone() { }

    public static implicit operator Phone(string phone)
    {
        var data = phone.Split(" ");

        if (data.Length != 2)
            throw new Exception(
                "Input string was not in a correct format. It must be composed by two words separated by space.");

        return new Phone(data[0], data[1]);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IDD;
        yield return Number;
    }

    public Phone(string idd, string number)
    {
        EnsureIddIsValid(idd);
        EnsureNumberIsValid(number);

        IDD = idd;
        Number = number;
    }

    private static void EnsureIddIsValid(string idd)
    {
        if (string.IsNullOrWhiteSpace(idd))
            throw new Exception("You must inform a IDD");

        if (!idd.All(char.IsDigit))
            throw new Exception($"The {nameof(idd)} must contain only digits");

        if (idd.Length < MinIddLength)
            throw new Exception($"The {nameof(idd)} must be greater than {MinIddLength}");

        if (idd.Length == MinIddLength && idd[0] == MinIddValue)
            throw new Exception($"The {nameof(idd)} must not be zero");

        if (idd.Length > MaxIddLength)
            throw new Exception($"The {nameof(idd)} must be less or equal to {MaxIddLength}");
    }

    private static void EnsureNumberIsValid(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new Exception("You must inform a number");

        if (!number.All(char.IsDigit))
            throw new Exception($"The {nameof(number)} must contain only digits");

        if (number.Length > MaxNumberLength)
            throw new Exception($"The {nameof(number)} must be less or equal to {MaxNumberLength}");
    }

    public static implicit operator string(Phone phone) => phone.GetFullPhone();
    public override string ToString() => GetFullPhone();
}
