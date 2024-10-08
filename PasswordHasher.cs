using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Gis.Net.Auth;

/// <summary>
/// Provides methods for hashing and verifying passwords.
/// </summary>
public static class PasswordHasher
{
    private const int Pbkdf2Iterations = 1000;
    
    /// <summary>
    /// Create Hash Password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    ///
    /// Example:
    /// PasswordHash = PasswordHasher.HashPasswordV3(model.Password)
    public static string HashPasswordV3(string password)
    {
        return Convert.ToBase64String(HashPasswordV3(password, RandomNumberGenerator.Create()
            , prf: KeyDerivationPrf.HMACSHA512, iterCount: Pbkdf2Iterations, saltSize: 128 / 8
            , numBytesRequested: 256 / 8));
    }

    /// <summary>
    /// Verify Password hashed
    /// </summary>
    /// <param name="hashedPasswordStr"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    ///
    /// Example:
    /// if (!PasswordHasher.VerifyHashedPasswordV3(user.PasswordHash, model.Password))) ....
    public static bool VerifyHashedPasswordV3(string hashedPasswordStr, string password)
    {
        var hashedPassword = Convert.FromBase64String(hashedPasswordStr);

        try
        {
            // Read header information
            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            var iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            var saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
                return false;
            
            var salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            var subKeyLenght = hashedPassword.Length - 13 - salt.Length;
            if (subKeyLenght < 128 / 8)
                return false;
            
            var expectedSubkey = new byte[subKeyLenght];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            var actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subKeyLenght);
            
#if NETSTANDARD2_0 || NETFRAMEWORK
            return ByteArraysEqual(actualSubkey, expectedSubkey);
#elif NETCOREAPP
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
#else
#error Update target frameworks
#endif
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    // privates
    private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
    {
        var salt = new byte[saltSize];
        rng.GetBytes(salt);
        var subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);
        var outputBytes = new byte[13 + salt.Length + subKey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);
        return outputBytes;
    }

    private static void WriteNetworkByteOrder(IList<byte> buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }

    private static uint ReadNetworkByteOrder(IReadOnlyList<byte> buffer, int offset)
    {
        return (uint)buffer[offset + 0] << 24
            | (uint)buffer[offset + 1] << 16
            | (uint)buffer[offset + 2] << 8
            | buffer[offset + 3];
    }

}