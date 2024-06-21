
using System.Text;
using ApexaAdvisors.Domain.Resources;

namespace ApexaAdvisors.Domain.CustomExceptions;

public class InvalidDomainStateException : Exception
{
    private InvalidDomainStateException(string message) : base(message)
    {
    }

    public static void ThrowIfAny(IEnumerable<string> validationCodes)
    {
        if (!validationCodes.Any())
            return;

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Domain model entered invalid state with following error(s):");

        foreach (var code in validationCodes)
        {
            stringBuilder.AppendLine(CommonResource.ResourceManager.GetString(code));
        }

        throw new InvalidDomainStateException(stringBuilder.ToString());
    }
}
