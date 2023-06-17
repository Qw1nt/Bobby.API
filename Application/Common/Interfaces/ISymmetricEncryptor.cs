namespace Application.Common.Interfaces;

public interface ISymmetricEncryptor
{
    string Encrypt(string sourceString, out string salt);

    string Decrypt(string ciphertext, in string salt);
}