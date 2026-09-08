using UnityEngine;

namespace PLIbox.Extensions
{
    public static class RandomExtensions
    {
        public static bool RandomBool() => Random.Range(0, 2) == 0;
        public static int RandomSign() => Random.Range(0, 2) == 0 ? -1 : 1;
    }
}

