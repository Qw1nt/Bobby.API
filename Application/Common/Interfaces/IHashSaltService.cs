namespace Application.Common.Interfaces;

public interface IHashSaltService
{
    string Salt();

    string Hash(string sourceValue, string forSalt);
}