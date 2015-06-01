namespace PasswordGen.RandomProviders
{
    using System;

    public interface IRandom : IDisposable
    {
        int Next(int maxValue);
    }
}