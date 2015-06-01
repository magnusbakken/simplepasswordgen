namespace PasswordGen.RandomProviders
{
    using System;

    public class SystemRandomProvider : IRandom
    {
        private readonly Random random;

        public SystemRandomProvider(Random random)
        {
            this.random = random;
        }

        public int Next(int maxValue)
        {
            return this.random.Next(maxValue);
        }

        public void Dispose()
        {
            // System.Random is a local PRNG, so it doesn't hold any external resources.
        }
    }
}