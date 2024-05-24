namespace API.Services
{
    public interface ILuhnValidator
    {
        bool IsValid(string creditCardNumber);
    }
}
