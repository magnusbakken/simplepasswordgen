namespace PasswordGen.WebApi.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using PasswordGen.RandomProviders;
    using PasswordGen.WebApi.Models;

    [RoutePrefix("api/passwordgen")]
    public class PasswordGeneratorController : ApiController
    {
        private const int DefaultPasswordLength = 8;
        private static readonly int MaxPasswordLength = 100;

        /// <summary>
        /// Generates a new random password with the specified options.
        /// </summary>
        /// <remarks>
        /// The special characters include all printable ASCII characters in the range 1-127 except the space character, letters and digits.
        /// </remarks>
        /// <param name="length">The length of the password. Must be in the range 1-100.</param>
        /// <param name="lowercase">If true, lowercase letters (a-z) are included.</param>
        /// <param name="uppercase">If true, uppercase letters (a-z) are included.</param>
        /// <param name="digits">If true, digits (0-9) are included.</param>
        /// <param name="special">
        /// <para>
        /// If true, special characters (e.g. !\"#$) are included.
        /// </para>
        /// <para>
        /// Specifically, the special characters include all printable ASCII characters in the range 1-127 except the space character, letters and digits.
        /// </para>
        /// </param>
        /// <param name="provider">
        /// <para>
        /// The randomness provider. This determines the source of the randomly generated characters in the password.
        /// </para>
        /// <para>
        /// If you intend to use the generated passwords for real purposes, you should use the cryptographic RNG or the random.org provider.
        /// The pseudo-random generator is faster, but not a good choice for generating real passwords.
        /// </para>
        /// </param>
        /// <returns>A new random password.</returns>
        [Route("generate")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GeneratePassword(
            int length = DefaultPasswordLength,
            bool lowercase = true,
            bool uppercase = true,
            bool digits = true,
            bool special = false,
            RandomProviderType provider = RandomProviderType.CryptoRandom)
        {
            var options = new AlphabetOptions()
            {
                IncludeAToZLowercase = lowercase,
                IncludeAToZUppercase = uppercase,
                IncludeDigits = digits,
                IncludeSpecialCharacters = special,
            };

            return Ok(this.GeneratePassword(length, options, provider));
        }

        /// <summary>
        /// Gets a list of randomness providers.
        /// </summary>
        /// <returns>A list of randomness providers, with IDs and names.</returns>
        [Route("provider")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetProviders()
        {
            return Ok(Enum.GetValues(typeof(RandomProviderType))
                .Cast<RandomProviderType>()
                .Select(rpt => new RandomnessProvider(rpt))
                .ToList());
        }

        /// <summary>
        /// Gets the randomness provider with the given ID.
        /// </summary>
        /// <param name="id">The ID to get.</param>
        /// <returns>The ID and name of the randomness provider with the given ID.</returns>
        [Route("provider/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetProvider(int id)
        {
            return Ok(new RandomnessProvider((RandomProviderType)id));
        }

        private static HttpResponseException PasswordLengthTooShortError()

        {
            return new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Password length must be at least 1" });
        }

        private static HttpResponseException PasswordLengthTooLongError()
        {
            string errorMessage = string.Format(CultureInfo.InvariantCulture, "Password length may not exceed {0}", MaxPasswordLength);
            return new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = errorMessage });
        }

        private static HttpResponseException EmptyAlphabetError()
        {
            return new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "No characters are included in the alphabet" });
        }

        private string GeneratePassword(int length, AlphabetOptions alphabetOptions, RandomProviderType providerType)
        {
            if (length < 1)
                throw PasswordLengthTooShortError();
            else if (length > MaxPasswordLength)
                throw PasswordLengthTooLongError();

            var alphabet = alphabetOptions.CreateAlphabet();
            if (!alphabet.Any())
                throw EmptyAlphabetError();

            var passwordGenerator = new PasswordGenerator(providerType);
            return passwordGenerator.GeneratePassword(length, alphabet).ConvertToString();
        }
    }
}