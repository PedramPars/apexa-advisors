using ApexaAdvisors.Domain.CustomExceptions;
using ApexaAdvisors.Domain.Resources;

namespace ApexaAdvisors.Domain.Models.ValueObjects;

public record ContactInformation
{
    protected ContactInformation()
    {

    }

    public ContactInformation(string? address, string? phone) : this()
    {
        var validations = Validate(address, phone);
        InvalidDomainStateException.ThrowIfAny(validations);

        Address = address?.Trim();
        Phone = phone?.Trim();
    }

    public string? Address { get; private set; }
    public string? Phone { get; private set; }

    public static ContactInformation Empty => new ContactInformation(null, null);

    public static IEnumerable<string> Validate(string? address, string? phone)
    {
        if (!string.IsNullOrEmpty(address) && address.Trim().Length > 255)
            yield return nameof(CommonResource.Validations_AddressShouldBeLessThan255Characters);

        if (!string.IsNullOrEmpty(phone) && phone.Trim().Length != 8)
            yield return nameof(CommonResource.Validations_PhoneShouldBe8Digits);
    }
}
