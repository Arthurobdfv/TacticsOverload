using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private TMP_Text buttonText;
    private Button button;
    private string m_label;

    public string Label
    {
        get { return m_label; }
        set
        {
            m_label = value;
            LabelChanged(m_label);
        }
    }

    private UnityAction m_action;
    public UnityAction Action
    {
        get { return m_action; }
        set
        {
            m_action = value;
            ActionChanged(m_action);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>() ?? throw new MissingComponentOnStartException(nameof(buttonText));
        button = GetComponentInChildren<Button>() ?? throw new MissingComponentOnStartException(nameof(button));
        SetFields();
    }

    void ActionChanged(UnityAction _newAction)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(_newAction);
        }
    }

    void LabelChanged(string _newLabel)
    {
        if (buttonText != null) buttonText.text = _newLabel;
    }

    private void SetFields()
    {
        LabelChanged(Label);
        ActionChanged(Action);        
    }
}
