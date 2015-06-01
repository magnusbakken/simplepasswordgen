namespace PasswordGen
{
    using System;
    using System.Collections.Immutable;
    using System.Security;
    using PasswordGen.RandomProviders;

    public class PasswordGenerator : IDisposable
    {
        private readonly IRandom random;

        public PasswordGenerator(RandomProviderType providerType)
        {
            this.random = RandomProviderFactory.CreateProvider(providerType);
        }

        public SecureString GeneratePassword(int length, IImmutableSet<char> alphabet)
        {
            SecureString password = new SecureString();
            for (int i = 0; i < length; i++)
            {
                password.AppendChar(this.GenerateChar(alphabet));
            }

            password.MakeReadOnly();
            return password;
        }

        public char GenerateChar(IImmutableSet<char> alphabet)
        {
            return this.random.Choice(alphabet);
        }

        public void Dispose()
        {
            this.random.Dispose();
        }
    }
}