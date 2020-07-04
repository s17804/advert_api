namespace AdvertApi.Services
{
    public interface IPasswordService
    {
        string CreateSaltedPasswordHash(string password, byte[] salt);

        byte[] GenerateSalt();

        bool ValidatePassword(string receivedPassword, string storesPassword, byte[] salt);
    }
}