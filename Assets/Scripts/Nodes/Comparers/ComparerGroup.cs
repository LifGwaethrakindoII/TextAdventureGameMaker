using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class ComparerGroup
{
    public IntegerComparer[] intComparers;
    public FloatComparer[] floatComparers;
    public StringComparer[] stringComparers;
    public BoolComparer[] boolComparers;
}
}