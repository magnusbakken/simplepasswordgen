namespace PasswordGen.RandomProviders
{
    using System;
    using System.Security.Cryptography;

    public class CryptoRandomProvider : Random, IRandom
    {
        private readonly RNGCryptoServiceProvider innerProvider;

        public CryptoRandomProvider(RNGCryptoServiceProvider innerProvider)
        {
            this.innerProvider = innerProvider;
        }

        public override int Next()
        {
            return this.InternalSample();
        }

        public override void NextBytes(byte[] buffer)
        {
            this.innerProvider.GetBytes(buffer);
        }

        public void Dispose()
        {
            this.innerProvider.Dispose();
        }

        protected override double Sample()
        {
            return this.InternalSample() * (1.0 / int.MaxValue);
        }

        private int InternalSample()
        {
            var bytes = new byte[sizeof(int)];
            this.innerProvider.GetBytes(bytes);
            bytes[BitConverter.IsLittleEndian ? 3 : 0] &= 0x7f; // Force a positive number.
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}