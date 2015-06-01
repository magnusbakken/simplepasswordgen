namespace PasswordGen
{
    using System.Collections.Generic;
    using System.Linq;
    using PasswordGen.RandomProviders;

    public static class RandomExt
    {
        public static T Choice<T>(this IRandom random, IReadOnlyCollection<T> input)
        {
            return input.ElementAt(random.Next(input.Count));
        }
    }
}