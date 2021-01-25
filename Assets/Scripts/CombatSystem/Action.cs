using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Move,
    Attack
}

public enum ActionResult
{
    Success,
    Cancel
}

public class ActionEventArgs : EventArgs
{
    ActionResult Result { get; set; }
    public ActionEventArgs(ActionResult res)
    {
        Result = res;
    }
}
public interface IAction
{
    Unit Entity { get; }
    ActionType ActionType { get; }

    string ActionName { get; }

    void Action(CharacterStats stats, Action ActionCallback = null);

    event EventHandler<ActionEventArgs> ActionCompleted;

    void ShowActionUI();

    void ShowConfirmation(Action onConfirm, Action onCancel);
}

public class BaseAction : IAction
{
    public Unit Entity { get; set; }

    public ActionType ActionType { get; set; }
    public ActionResult? Result { get; set; }

    public string ActionName { get; set; }

    public event EventHandler<ActionEventArgs> ActionCompleted;

    public BaseAction(Unit entity, ActionType actionType, string actionName, Action<object, ActionEventArgs> callback = null)
    {
        Entity = entity;
        ActionType = actionType;
        ActionName = actionName;
        if(callback != null) ActionCompleted += callback.Invoke;
    }

    public virtual void Action(CharacterStats stats, Action ActionCallback = null)
    {
        OnComplete();
    }

    public void ShowActionUI()
    {
        throw new NotImplementedException();
    }

    public void ShowConfirmation(Action onConfirm, Action onCancel)
    {
        throw new NotImplementedException();
    }

    private void OnComplete()
    {
        ActionCompleted?.Invoke(this,new ActionEventArgs(Result.Value));
        foreach (var d in ActionCompleted.GetInvocationList())
        {
            ActionCompleted -= (EventHandler<ActionEventArgs>)d;
        }
    }

}

public class CharacterStats {

    private int m_move;
    private int m_attack;
    private int m_maxHealth;
    public int Move
    {
        get { return m_move; }
        set { m_move = value; }
    }


    public int Attack
    {
        get { return m_attack; }
        set { m_attack = value; }
    }


    public int MaxHealth
    {
        get { return m_maxHealth; }
        set { m_maxHealth = value; }
    }



}