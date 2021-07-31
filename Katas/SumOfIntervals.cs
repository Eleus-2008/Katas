using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public static class SumOfIntervals
    {
        public static int SumIntervals((int, int)[] intervals)
        {
            var handledIntervals = new List<(int, int)>();
            foreach (var newInterval in intervals)
            {
                var (newStart, newEnd) = newInterval;
                var removingIntervals = new List<(int, int)>();
                foreach (var interval in handledIntervals)
                {
                    var (start, end) = interval;

                    if (newStart <= start && end <= newEnd)
                    {
                        removingIntervals.Add((start, end));
                    }

                    if (start <= newStart && newEnd <= end)
                    {
                        (newStart, newEnd) = (start, end);
                        removingIntervals.Add((start, end));
                        break;
                    }

                    if (start <= newStart && newStart < end)
                    {
                        newStart = start;
                        removingIntervals.Add((start, end));
                    }

                    if (start < newEnd && newEnd <= end)
                    {
                        newEnd = end;
                        removingIntervals.Add((start, end));
                    }
                }

                removingIntervals.ForEach(i => handledIntervals.Remove(i));
                handledIntervals.Add((newStart, newEnd));
            }

            return handledIntervals.Sum(interval => interval.Item2 - interval.Item1);
        }
    }
}