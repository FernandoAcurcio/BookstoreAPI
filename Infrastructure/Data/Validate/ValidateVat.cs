namespace Infrastructure.Data.Validate
{
    public static class ValidateVat
    {
        // checks the validity of a Portuguese VAT(NIF) number by verifying its length,
        // examining the first digit against specified values, and applying a calculation for a check digit.
        public static bool ValidVat(string vatString)
        {
            // Check if the VAT string has exactly 9 characters
            if (vatString.Length != 9)
                return false;

            // Extract the first character of the VAT string
            char firstChar = vatString[0];

            // vat of persons start with 1 and 2, it is expected to the 3 will be added soon
            // vat for companies start with 5, 6 and 9
            if (firstChar != '1' && firstChar != '2' && firstChar != '3' && firstChar != '5' && firstChar != '6' && firstChar != '9')
                return false;

            // Calculate the check digit
            int checkDigit = (int)char.GetNumericValue(firstChar) * 9;
            for (int i = 2; i <= 8; i++)
            {
                checkDigit += (int)char.GetNumericValue(vatString[i - 1]) * (10 - i);
            }
            checkDigit = 11 - (checkDigit % 11);
            if (checkDigit >= 10)
                checkDigit = 0;

            // Compare the calculated check digit with the last digit of the VAT string
            return checkDigit == (int)char.GetNumericValue(vatString[8]);
        }
    }
}
