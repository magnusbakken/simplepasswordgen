namespace PasswordGen
{
    using System;
    using System.Security.Cryptography;

    public static class CryptoSeedProvider
    {
        public static int GenerateSeed()
        {
            var bytes = new byte[4];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}