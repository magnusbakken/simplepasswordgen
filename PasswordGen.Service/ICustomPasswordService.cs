namespace PasswordGen.Service
{
    using System;
    using System.ServiceModel;

    //[ServiceContract]
    public interface ICustomPasswordService
    {
        [FaultContract(typeof(ArgumentException))]
        [FaultContract(typeof(ArgumentNullException))]
        [OperationContract]
        string GenerateCustomPassword(int length, AlphabetOptions alphabetOptions);
    }
}