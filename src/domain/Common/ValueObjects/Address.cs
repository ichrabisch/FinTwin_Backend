namespace Domain.Common.ValueObjects;

public sealed record Address
{
    public Address(string street, string city, string country)
    {
        Street = street;
        City = city;
        Country = country;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public float? Latitude { get; internal set; }
    public float? Longitude { get; internal set; }

}
