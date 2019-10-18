namespace PasswordHasher.Core.Hashing
{
    public interface IHasher
    {
        string ToBase64EncodedSha512(string input);
    }
}
