using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    public string Name => m_name;
    private CharacterInfo m_info;
    public bool AllowMove
    {
        get { return m_allowMove; }
        private set
        {
            m_allowMove = value;
        }
    }
    public bool AllowAction
    {
        get { return m_allowAction; }
        private set
        {
            m_allowAction = value;
        }
    }

    public List<IAction> m_actions = new List<IAction>();

    private int move = 2;

    private List<Unit> m_units = new List<Unit>();

    private Unit m_selectedUnit;

    public Unit SelectedUnit
    {
        get { return m_selectedUnit; }
        set { m_selectedUnit = value; }
    }


    public List<Unit> Units
    {   
        get { return m_units; }
        private set { m_units = value; }
    }

    private void Start()
    {
        Stats = new CharacterStats()
        {
            Move = 4,
            MaxHealth = 10,
            Attack = 3
        };

        var x = gameObject.GetComponent<Unit>();
        Units.Add(x);
        m_actions.Add(new MoveAction(this));
        m_actions.Add(new AttackAction());
    }

    void OnActionComplete(IAction action)
    {
        m_actions.Remove(action);
    }

    private string m_name;
    bool m_allowMove;
    bool m_allowAction;
    private MeshRenderer m_material;

    public void SetMaterial(Material m)
    {
        m_material = GetComponent<MeshRenderer>() ?? throw new MissingComponentOnStartException(nameof(m_material));
        m_material.material = m;
    }

    void OnRoundStart(Player plr)
    {
        if (plr == this) RoundStart();
    }

    void RoundStart()
    {
        AllowMove = true;
        AllowAction = true;
    }

    public void SetName(string newName)
    {
        m_name = newName;
    }
}
