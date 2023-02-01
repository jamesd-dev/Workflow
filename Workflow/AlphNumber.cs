using System.Text.RegularExpressions;
using static Workflow.Utils;

namespace Workflow;

public class AlphNumber
{
    private static readonly int MaxDigits = 4;
    private static readonly string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly int Base = Digits.Length;
    
    public string Value { get; private set; }

    public AlphNumber(long decimalNumber)
    {
        Value = GetAlphNumberFromLong(decimalNumber);
    }
    
    public AlphNumber(string alphNumber)
    {
        Value = alphNumber.ToUpper();
    }

    private static string Add(string a, string b)
    {
        if (!IsValidAlphNumber(a) || !IsValidAlphNumber(b))
        {
            throw new ArgumentException("Must provide a valid alphNumber");
        }
        
        string result = "";
        int carry = 0;
        for (int i = a.Length - 1; i >= 0; i--)
        {
            char aDigit = a[i];
            char bDigit = b[i];
            int aValue = Digits.IndexOf(aDigit);
            int bValue = Digits.IndexOf(bDigit);
            int totalValue = aValue + bValue + carry;
            carry = totalValue / Base;
            int remainder = totalValue % Base;
            char remainderDigit = Digits[remainder];
            result += remainderDigit;
        }

        if (carry > 0)
        {
            throw new Exception("Cannot add two alphNumbers when total is greater than the max digits set.");
        }

        result = Reverse(result);
        return result;
    }

    private static bool IsValidAlphNumber(string alphNumber)
    {
        string pattern = "[0-9A-Z]{" + MaxDigits + "}"; 
        Regex rg = new Regex(pattern);
        return rg.IsMatch(alphNumber);
    }

    public void Increment()
    {
        Value = Add(Value, "0001");
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
        if (!IsValidAlphNumber(alphNumber))
        {
            throw new ArgumentException("Must provide a valid alphNumber");
        }
        
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