using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Persistence.Configurations;
using SecurityDriven.Inferno;
using SecurityDriven.Inferno.Extensions;

namespace Infrastructure.Services;

public class SymmetricEncryptor : ISymmetricEncryptor
{
    private readonly AuthenticationConfiguration _authenticationConfiguration;
    private readonly IHashSaltService _hashSaltService;

    public SymmetricEncryptor(AuthenticationConfiguration authenticationConfiguration, IHashSaltService hashSaltService)
    {
        _authenticationConfiguration = authenticationConfiguration;
        _hashSaltService = hashSaltService;
    }

    public string Encrypt(string sourceString, out string salt)
    {
        salt = _hashSaltService.Salt();
        var masterKey = _authenticationConfiguration.SymmetricSecurityKey().Key;
        var sourceStringBytes = Utils.SafeUTF8.GetBytes(sourceString);
        var saltBytes = Utils.SafeUTF8.GetBytes(salt);
        var chippedBytes = SuiteB.Encrypt(masterKey, sourceStringBytes, saltBytes);
        var chippedText = chippedBytes.ToB64();
        return chippedText;
    }

    public string Decrypt(string ciphertext, in string salt)
    {
        var masterKey = _authenticationConfiguration.SymmetricSecurityKey().Key;
        var chippedBytes = ciphertext.FromB64();
        var saltBytes = Utils.SafeUTF8.GetBytes(salt);
        var decryptBytes = SuiteB.Decrypt(masterKey, chippedBytes, saltBytes);
        var decryptText = Utils.SafeUTF8.GetString(decryptBytes);
        return decryptText;
    }
}