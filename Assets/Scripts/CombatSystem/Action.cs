﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Move,
    Attack
}

public interface IAction
{
    ActionType ActionType { get; }

    void Action(CharacterStats stats, Action ActionCallback = null);
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