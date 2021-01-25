using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEventArgs : EventArgs
{
    public GameObject SenderGameObject { get; set; }
}

public interface IInteractible
{
    void OnClick();
}

public class InteractibleGameObject : MonoBehaviour, IInteractible
{
    public event EventHandler<OnClickEventArgs> RaiseOnClick;

    public void OnClick()
    {
        RaiseOnClick?.Invoke(this, new OnClickEventArgs()
        {
            SenderGameObject = this.gameObject
        });
    }
}