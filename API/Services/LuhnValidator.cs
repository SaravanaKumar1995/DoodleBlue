namespace API.Services
{
    public class LuhnValidator
    {
        public bool IsValid(string creditCardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                char[] nx = creditCardNumber.ToCharArray();
                int n = int.Parse(nx[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }
    }
}
