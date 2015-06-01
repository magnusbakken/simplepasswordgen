namespace PasswordGen.RandomProviders
{
    using System;
    using System.Security.Cryptography;

    public static class RandomProviderFactory
    {
        public static IRandom CreateProvider(RandomProviderType providerType)
        {
            switch (providerType)
            {
                case RandomProviderType.SystemRandom:
                    return new SystemRandomProvider(new Random());
                case RandomProviderType.CryptoRandom:
                    return new CryptoRandomProvider(new RNGCryptoServiceProvider());
                case RandomProviderType.RandomDotOrg:
                    return new RandomDotOrgProvider(new global::Random.Org.Random());
                default:
                    throw new ArgumentException("providerType");
            }
        }
    }
}