using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingComponentOnStartException : Exception
{
    public MissingComponentOnStartException()
    {

    }

    public MissingComponentOnStartException(string message):base(message)
    {

    }
        
}
