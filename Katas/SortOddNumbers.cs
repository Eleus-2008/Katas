using System.Linq;

namespace Katas
{
    public static class SortOddNumbers
    {
        public static int[] SortArray(int[] array)
        {
            var oddNumbersWithIndexes = array.Select((n, i) => (n, i)).Where(n => n.n % 2 == 1).ToArray();
            var oddNumbers = oddNumbersWithIndexes.Select(ni => ni.n).ToList();
            var indexes = oddNumbersWithIndexes.Select(ni => ni.i).ToArray();
            oddNumbers.Sort();

            for (var i = 0; i < oddNumbers.Count; i++)
            {
                array[indexes[i]] = oddNumbers[i];
            }

            return array;
        }
    }
}