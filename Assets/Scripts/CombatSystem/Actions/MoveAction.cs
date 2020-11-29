using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : IAction
{
    public ActionType ActionType { get => ActionType.Move; }

    private ActionType m_actionType;
    public void Action(CharacterStats charStats, Action ActionCallback = null)
    {
        Debug.Log(ActionType.ToString());
    } 
}
