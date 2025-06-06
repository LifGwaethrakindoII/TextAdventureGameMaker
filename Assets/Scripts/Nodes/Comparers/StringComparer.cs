using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
public enum StringComparison { Equal, NotEqual }

[Serializable]
public struct StringComparer
{
    public string key;
    public StringComparison comparison;
    public string value;

    public bool Evaluate(string x)
    {
        switch (comparison)
        {
            case StringComparison.Equal:        return x == value;
            case StringComparison.NotEqual:     return x != value;
            default:                            return false;
        }
    }
}
}