namespace Workflow;

public class AlphNumber
{
    private static readonly int Base = 36;
    private static readonly int MaxDigits = 4;
    private static readonly string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public static string GetAlphNumberFromInt(int num)
    {
        bool numberIsTooLarge = num > Math.Pow(Base, MaxDigits) - 1;
        if (numberIsTooLarge)
        {
            throw new Exception("Given number is too large to convert to a alphnumber with the set length");
        }

        string alphNumber = "";
        int remainder = num;
        for (int d = MaxDigits - 1; d >= 0; d--)
        {
            char divisor;
            long alphValue = (int)Math.Pow(Base, d);
            if (alphValue <= remainder)
            {
                int numDivisor = (int)(remainder / alphValue);
                divisor = Digits[numDivisor];
                remainder = (int)(remainder % alphValue);
            }
            else
            {
                divisor = '0';
            }

            alphNumber += divisor;
        }

        return alphNumber;
    }
}