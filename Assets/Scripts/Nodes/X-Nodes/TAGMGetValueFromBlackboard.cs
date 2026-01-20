using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

using IO = XNode.NodePort.IO;

namespace Voidless.TextAdventureMaker
{
public enum DataType { Int, Float, Bool, String }

[Serializable]
public class TAGMGetValueFromBlackboard : Node
{
    public DataType dataType;
    public int x;
    [Input] public int intInput;
    [Input] public float floatInput;
    [Input] public bool boolInput;
    [Input] public string stringInput;
    [Output] public int intValue;    
    [Output] public float floatValue;    
    [Output] public bool boolValue;    
    [Output] public string stringValue;

     /// <summary>GetValue should be overridden to return a value for any specified output port.</summary>
    public override object GetValue(NodePort port)
    {
        switch(port.fieldName)
        {
            case "intValue":        return x;
            case "floatValue":      return floatValue;
            case "boolValue":       return boolValue;
            case "stringValue":     return floatValue;
            default:                return default(object);
        }
    }
}
}