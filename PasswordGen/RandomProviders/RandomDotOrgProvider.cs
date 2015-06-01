namespace PasswordGen.RandomProviders
{
    using System.Collections.Generic;
    using System.Linq;

    public class RandomDotOrgProvider : IRandom
    {
        private Random.Org.Random random;

        public RandomDotOrgProvider(Random.Org.Random random)
        {
            this.random = random;
        }

        public int Next(int maxValue)
        {
            return this.random.Next(0, maxValue);
        }

        public void Dispose()
        {
            // No need to dispose anything here.
        }
    }
}