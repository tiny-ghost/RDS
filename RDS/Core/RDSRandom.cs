using System;

namespace RDS.Core
{
    static class RDSRandom
    {
        private static Random _rand = null;

        static RDSRandom()
        {
            SetRandomizer(null);
        }

        public static int GetIntValue(int max)
        {
            return _rand.Next(max);
        }

        public static int GetIntValue(int min, int max)
        {
            return _rand.Next(min, max);
        }

        public static bool IsPercentHit(double percent)
        {
            return _rand.NextDouble() < percent;
        }

        private static void SetRandomizer(Random randomizer)
        {
            if (randomizer == null)
            {
                _rand = new Random();
            }
            else
            {
                _rand = randomizer;
            }
        }
    }
}
