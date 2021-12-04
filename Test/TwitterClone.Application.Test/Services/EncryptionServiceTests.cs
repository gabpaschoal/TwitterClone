using FluentAssertions;
using Microsoft.Extensions.Options;
using TwitterClone.Application.Services;
using TwitterClone.Domain.Services.Models;
using Xunit;

namespace TwitterClone.Application.Test.Services;

public class EncryptionServiceTests
{
    private static EncryptionService MakeSut(string key = "This is a valid key to encypt data")
    {
        EncryptionModel model = new() { Key = key.Replace(" ", "") };
        var modelOptions = Options.Create(model);
        return new(modelOptions);
    }

    [Fact(DisplayName = "Should encrypt and descrypt to the same data")]
    public void Should_encrypt_and_descrypt_to_the_same_data()
    {
        const string originalString = "ThisIsTheOriginal String";
        var sut = MakeSut();

        var encryptedData = sut.Encrypt(originalString);
        encryptedData.Should().NotMatch(originalString);

        var decryptedData = sut.Decrypt(encryptedData);
        decryptedData.Should().Match(originalString);
    }
}
