// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{

    var iterate = 10000;
    //var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    //byte[] hash = pbkdf2.GetBytes(20);

    //byte[] hashBytes = new byte[36];
    //Array.Copy(salt, 0, hashBytes, 0, 16);
    //Array.Copy(hash, 0, hashBytes, 16, 20);

    var hash = Rfc2898DeriveBytes.Pbkdf2(
        Encoding.UTF8.GetBytes(passwordText),
        salt,
        iterate,
        HashAlgorithmName.SHA512,
        64);

    var passwordHash = Convert.ToHexString(hash);

    return passwordHash;

}