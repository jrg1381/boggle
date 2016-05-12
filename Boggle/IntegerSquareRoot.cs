using System;

namespace Boggle
{
    internal static class IntegerSquareRoot
    {
        internal static int Sqrt(int number)
        {
            var answer = Math.Sqrt(number);
            if (!IsCloseEnoughToInteger(answer))
                throw new ArgumentOutOfRangeException(nameof(number), "Not a square number of letters");

            return (int) answer;
        }

        private static bool IsCloseEnoughToInteger(double answer)
        {
            return Math.Abs(Math.Floor(answer) - Math.Ceiling(answer)) <= double.Epsilon;
        }
    }
}