using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public enum BoolComparison { True, False }

[Serializable]
public struct BoolComparer
{
    public string key;
    public BoolComparison comparison;

    public bool Evaluate(bool x)
    {
        return comparison == BoolComparison.True ? x == true : x == false;
    }
}
}