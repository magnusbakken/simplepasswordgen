namespace PasswordGen.WebApi.Models
{
    using System.Runtime.Serialization;
    using PasswordGen.RandomProviders;

    [DataContract]
    public class RandomnessProvider
    {
        public RandomnessProvider(RandomProviderType provider)
        {
            this.Id = (int)provider;
            this.Name = GeneratePasswordBindingModel.GetProviderDescription(this.Provider);
        }

        [DataMember(Order = 0)]
        public int Id
        {
            get { return (int)this.Provider; }
            set { this.Provider = (RandomProviderType)value; }
        }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        private RandomProviderType Provider { get; set; }
    }
}