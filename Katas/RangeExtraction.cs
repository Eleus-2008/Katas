using System.Text;

namespace Katas
{
    public static class RangeExtraction
    {
        public static string Extract(int[] args)
        {
            var counter = 0;
            var sb = new StringBuilder();
            for (var i = 0; i < args.Length; i++)
            {
                if (i != args.Length - 1 && args[i] + 1 == args[i + 1])
                {
                    counter++;
                    continue;
                }

                if (counter >= 2)
                {
                    if (i - counter != 0)
                    {
                        sb.Append(',');   
                    }
                    sb.AppendFormat("{0}-{1}", args[i - counter], args[i]);
                    counter = 0;
                    continue;
                }

                if (counter == 1)
                {
                    if (i - 1 != 0)
                    {
                        sb.Append(',');
                    }
                    sb.Append(args[i - 1]);
                    counter = 0;
                }

                if (counter == 0)
                {
                    if (i != 0)
                    {
                        sb.Append(',');
                    }
                    sb.Append(args[i]);
                }
            }

            return sb.ToString();
        }
    }
}