using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public struct FloatComparer
{
    public string key;
    public NumberComparison comparison;
    public float value;

    public bool Evaluate(float x)
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