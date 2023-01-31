namespace Workflow;

public class AlphNumber
{
    private static readonly int MaxDigits = 4;
    private static readonly string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly int Base = Digits.Length;
    
    public string AlphValue { get; private set; }
    public long DecimalValue { get; private set; }

    public AlphNumber(long decimalNumber)
    {
        DecimalValue = decimalNumber;
        AlphValue = GetAlphNumberFromLong(DecimalValue);
    }
    
    public AlphNumber(string alphNumber)
    {
        AlphValue = alphNumber.ToUpper();
        DecimalValue = GetLongFromAlphNumber(AlphValue);
    }

    public void Increment()
    {
        DecimalValue += 1;
        AlphValue = GetAlphNumberFromLong(DecimalValue);
    }
    
    public void Decrement()
    {
        DecimalValue -= 1;
        AlphValue = GetAlphNumberFromLong(DecimalValue);
    }
    
    private static string GetAlphNumberFromLong(long num)
    {
        bool numberIsTooLarge = num > Math.Pow(Base, MaxDigits) - 1;
        if (numberIsTooLarge)
        {
            throw new Exception("Given number is too large to convert to a alphnumber with the set length");
        }

        string alphNumber = "";
        long remainder = num;
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

    private static long GetLongFromAlphNumber(string alphNumber)
    {
        bool numberIsTooLarge = alphNumber.Length > MaxDigits;
        if (numberIsTooLarge)
        {
            throw new Exception("Given alphnumber is too large to convert to a long with the set length");
        }

        long convertedNum = 0;

        for (int i = 0; i < alphNumber.Length; i++)
        {
            int exponent = MaxDigits - (i + 1);
            char digit = alphNumber[i];
            long digitBaseValue = Digits.IndexOf(digit);
            long digitValue = (long)Math.Pow(Base, exponent) * digitBaseValue;
            convertedNum += digitValue;
        }

        return convertedNum;
    }
}