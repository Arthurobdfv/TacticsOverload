using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : IAction
{
    public ActionType ActionType { get => ActionType.Attack; }

    public void Action(CharacterStats stats, Action ActionCallback = null)
    {
        throw new NotImplementedException();
    }

}
