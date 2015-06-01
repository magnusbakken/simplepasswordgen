namespace PasswordGen
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public class AlphabetOptions
    {
        private static readonly ImmutableHashSet<char> AToZLowercaseChars = GetLetters(uppercase: false);
        private static readonly ImmutableHashSet<char> AToZUppercaseChars = GetLetters(uppercase: true);
        private static readonly ImmutableHashSet<char> DigitChars = GetDigits();
        private static readonly ImmutableHashSet<char> SpecialCharacterChars = GetSpecialCharacters();

        public AlphabetOptions()
        {
            this.IncludeExtra = ImmutableHashSet<char>.Empty;
            this.ExcludeExtra = ImmutableHashSet<char>.Empty;
        }

        [DataMember(Order = 0, IsRequired = true)]
        public bool IncludeAToZLowercase { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public bool IncludeAToZUppercase { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public bool IncludeDigits { get; set; }

        [DataMember(Order = 3, IsRequired = true)]
        public bool IncludeSpecialCharacters { get; set; }

        [DataMember(Order = 4, IsRequired = false)]
        public IImmutableSet<char> IncludeExtra { get; set; }

        [DataMember(Order = 5, IsRequired = false)]
        public IImmutableSet<char> ExcludeExtra { get; set; }

        public static AlphabetOptions Standard()
        {
            return new AlphabetOptions()
            {
                IncludeAToZLowercase = true,
                IncludeAToZUppercase = true,
                IncludeDigits = true,
                IncludeSpecialCharacters = false,
            };
        }

        public ImmutableHashSet<char> CreateAlphabet()
        {
            return ImmutableHashSet<char>.Empty
                .UnionIf(this.IncludeAToZLowercase, AToZLowercaseChars)
                .UnionIf(this.IncludeAToZUppercase, AToZUppercaseChars)
                .UnionIf(this.IncludeDigits, DigitChars)
                .UnionIf(this.IncludeSpecialCharacters, SpecialCharacterChars)
                .Union(this.IncludeExtra)
                .Except(this.ExcludeExtra);
        }

        private static ImmutableHashSet<char> GetLetters(bool uppercase)
        {
            return ImmutableHashSet.CreateRange(Chars(uppercase ? 65 : 97, 26));
        }

        private static ImmutableHashSet<char> GetDigits()
        {
            return ImmutableHashSet.CreateRange(Chars(48, 10));
        }

        private static ImmutableHashSet<char> GetSpecialCharacters()
        {
            return ImmutableHashSet.CreateRange(Chars(33, 15)) // !"#$%&'()*+,-./
                .Union(Chars(58, 7)) // :;<=>?@
                .Union(Chars(91, 6)) // [\]^_`
                .Union(Chars(123, 4)); // {|}~
        }

        private static IEnumerable<char> Chars(int start, int count)
        {
            return Enumerable.Range(start, count).Select(x => (char)x);
        }
    }

    internal static class ImmutableHashSetExt
    {
        internal static ImmutableHashSet<T> UnionIf<T>(this ImmutableHashSet<T> baseSet, bool condition, IEnumerable<T> includeIfTrue)
        {
            return condition ? baseSet.Union(includeIfTrue) : baseSet;
        }
    }
}