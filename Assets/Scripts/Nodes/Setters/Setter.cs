using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Voidless.TextAdventureMaker
{
[Serializable] public class Setter<T> : SerializableKeyValuePair<string, T> { /*...*/ }
[Serializable] public class BoolSetter : Setter<bool> { /*...*/ }
[Serializable] public class IntegerSetter : Setter<int> { /*...*/ }
[Serializable] public class FloatSetter : Setter<float> { /*...*/ }
[Serializable] public class StringSetter : Setter<string> { /*...*/ }
}