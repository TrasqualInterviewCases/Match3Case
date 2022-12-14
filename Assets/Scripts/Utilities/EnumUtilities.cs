using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Utilities
{
    public class EnumUtilities : MonoBehaviour
    {
        public static T GetRandomFromEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            int rand = Random.Range(0, values.Length);
            return (T)values.GetValue(rand);
        }
    }
}