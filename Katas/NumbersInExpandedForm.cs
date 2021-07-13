using System.Text;

namespace Katas
{
    public static class NumbersInExpandedForm
    {
        public static string ExpandedForm(long num)
        {
            var numString = num.ToString();
            var sb = new StringBuilder();

            for (var i = 0; i < numString.Length; i++)
            {
                var digit = (int) char.GetNumericValue(numString[i]);

                if (digit != 0)
                {
                    if (i != 0)
                    {
                        sb.Append(" + ");
                    }
                    sb.Append(digit);
                    sb.Append('0', numString.Length - i - 1);
                }
            }

            return sb.ToString();
        }
    }
}