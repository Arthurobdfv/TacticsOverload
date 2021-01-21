using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : IAction
{
    public ActionType ActionType { get => ActionType.Attack; }

    public string ActionName => "Action";

    public Unit Entity => throw new NotImplementedException();

    public void Action(CharacterStats stats, Action ActionCallback = null)
    {
        throw new NotImplementedException();
    }

    public void ShowActionUI()
    {
        throw new NotImplementedException();
    }

    public void ShowConfirmation(Action onConfirm, Action onCancel)
    {
        throw new NotImplementedException();
    }
}
