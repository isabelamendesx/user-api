using Users.Domain.Common;

namespace Users.Domain.ValueObjects;
public class Name : ValueObject
{
    public const int MaxFirstNameLength = 99;
    public const int MaxLastNameLength = 99;

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    public Name(string firstName, string lastName)
    {
        if (firstName.Length >= MaxFirstNameLength)
            throw new Exception($"The {nameof(firstName)} must be smaller or equal to {MaxFirstNameLength}");

        if (lastName.Length >= MaxLastNameLength)
            throw new Exception($"The {nameof(lastName)} must be smaller or equal to {MaxLastNameLength}");

        FirstName = firstName;
        LastName = lastName;
    }

    public string GetFullName() => FirstName + " " + LastName;

    public static implicit operator string(Name name) => name.GetFullName();

    public static implicit operator Name(string name)
    {
        var data = name.Split(" ");
        return data.Length != 2
            ? throw new Exception("Input string was not in a correct format. It must be composed by two words separated by space.")
            : new Name(data[0], data[1]);
    }

    public override string ToString() => GetFullName();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    protected Name() { }
}
