using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : InteractibleGameObject
{
    private CharacterStats m_stats;

    public CharacterStats Stats
    {
        get { return m_stats; }
        set { m_stats = value; }
    }
}
    