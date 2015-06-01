namespace PasswordGen
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Security;

    public class PasswordGenerator
    {
        private readonly Random random;

        public PasswordGenerator(int seed)
        {
            this.random = new Random(seed);
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
    }
}