namespace PasswordGen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RandomExt
    {
        public static T Choice<T>(this Random random, IReadOnlyCollection<T> input)
        {
            return input.ElementAt(random.Next(input.Count));
        }
    }
}