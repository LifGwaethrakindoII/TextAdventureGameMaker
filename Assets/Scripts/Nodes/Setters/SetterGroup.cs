using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless.TextAdventureMaker
{
[Serializable]
public class SetterGroup
{
    public StringSetter[] stringSetters;
    public FloatSetter[] floatSetters;
    public BoolSetter[] boolSetters;
    public IntegerSetter[] intSetters;

    /// <summary>Sets all parameters into Game's data.</summary>
    public void Set()
    {
        if(TextAdventureGame.Instance == null) return;

        if(stringSetters != null) foreach(StringSetter stringSetter in stringSetters)
        {
            TextAdventureGame.stringDictionary.Set(stringSetter.key, stringSetter.value);
        }
        if(floatSetters != null) foreach(FloatSetter floatSetter in floatSetters)
        {
            TextAdventureGame.floatDictionary.Set(floatSetter.key, floatSetter.value);
        }
        if(boolSetters != null) foreach(BoolSetter boolSetter in boolSetters)
        {
            TextAdventureGame.boolDictionary.Set(boolSetter.key, boolSetter.value);
        }
        if(intSetters != null) foreach(IntegerSetter intSetter in intSetters)
        {
            TextAdventureGame.intDictionary.Set(intSetter.key, intSetter.value);
        }
    }
}
}