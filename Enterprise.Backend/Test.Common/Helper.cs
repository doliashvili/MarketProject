using System;

namespace Test.Common
{
    public static class Helper
    {
        private static readonly Random Rnd = new Random();


        public static Guid NewGuid => Guid.NewGuid();
        public static string GuidString => Guid.NewGuid().ToString();
        public static int RandomInt => Rnd.Next();

    }
}
