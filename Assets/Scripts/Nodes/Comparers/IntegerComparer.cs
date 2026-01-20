using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
    public enum NumberComparison { Equal, NotEqual, Greater, Lower }

    [Serializable]
    public struct IntegerComparer
    {
        public string key;
        public NumberComparison comparison;
        public int value;

        public bool Evaluate(int x)
        {
            switch (comparison)
            {
                case NumberComparison.Equal:        return x == value;
                case NumberComparison.NotEqual:     return x != value;
                case NumberComparison.Greater:      return x > value;
                case NumberComparison.Lower:        return x < value;
                default:                            return false;
            }
        }
    }
}