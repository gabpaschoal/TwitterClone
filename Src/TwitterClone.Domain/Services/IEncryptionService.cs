namespace TwitterClone.Domain.Services;

public interface IEncryptionService
{
    public string Encrypt(string data);
    public string Decrypt(string data);
}
