namespace PasswordGen.Service
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    public class PasswordService : IRESTPasswordService, ICustomPasswordService
    {
        public string GenerateStandardPassword()
        {
            return this.GenerateCustomLengthPassword(8);
        }

        public string GenerateCustomLengthPassword(int length)
        {
            return this.GenerateCustomPassword(length, AlphabetOptions.Standard());
        }

        public string GenerateCustomPassword(int length, AlphabetOptions alphabetOptions)
        {
            if (length < 1)
                throw new FaultException<ArgumentException>(new ArgumentException("Password length must be at least 1", "length"), "Password length must be at least 1");
            if (alphabetOptions == null)
                throw new FaultException<ArgumentNullException>(new ArgumentNullException("alphabetOptions"), "Alphabet options cannot be null");

            var passwordGenerator = new PasswordGenerator(CryptoSeedProvider.GenerateSeed());
            return passwordGenerator.GeneratePassword(length, alphabetOptions.CreateAlphabet()).ConvertToString();
        }
    }
}