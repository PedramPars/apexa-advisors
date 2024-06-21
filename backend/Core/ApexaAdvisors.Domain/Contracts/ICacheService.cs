namespace ApexaAdvisors.Domain.Contracts;

public interface ICacheService
{
    void Set<T>(string key, T value);
    T? Get<T>(string key);
    void Remove(string key);
}
