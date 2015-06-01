namespace PasswordGen.RandomProviders
{
    using System.Runtime.Serialization;

    [DataContract]
    public enum RandomProviderType
    {
        [EnumMember]
        CryptoRandom,

        [EnumMember]
        SystemRandom,

        [EnumMember]
        RandomDotOrg,
    }
}