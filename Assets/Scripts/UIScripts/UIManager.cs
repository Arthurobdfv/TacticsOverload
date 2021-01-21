﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_commandPanel;
    [SerializeField]
    private GameObject m_actionButtonPrefab;
    [SerializeField]
    private GameObject m_popupWindow;
    [SerializeField]
    private GameObject m_textField;

    private GameObject m_textPanel;
    private GameObject m_currentPanel;
    private GameObject m_currentPopup;

    public GameObject CurrentPanel
    {
        get => m_currentPanel;
        set
        {
            if (m_currentPanel != null)
            {
                DestroyPanel();
            }
            m_currentPanel = value;
        }
    }
    public GameObject CurrentPopup
    {
        get => m_currentPopup;
        set
        {
            if (m_currentPopup != null)
            {
                DestroyPopup();
            }
            m_currentPopup = value;
        }
    }

    public Stack<GameObject> windows = new Stack<GameObject>();

    public void ShowCommandPanel(Unit unit, List<IAction> actions)
    {
        CurrentPanel = Instantiate(m_commandPanel, transform);
        foreach (var ac in actions)
        {
            var btnGo = Instantiate(m_actionButtonPrefab, CurrentPanel.transform);
            var btnComp = btnGo.GetComponent<ButtonScript>();
            btnComp.Label = ac.ActionName;
            btnComp.Action = delegate { ac.Action(unit.Stats); };
        }
    }

    public void ShowInfoPanel(Unit unit)
    {
        CurrentPanel = Instantiate(m_commandPanel, transform);
        var btnGo = Instantiate(m_actionButtonPrefab, CurrentPanel.transform);
        var btnComp = btnGo.GetComponent<Button>();
        btnComp.onClick.AddListener(delegate { ShowPopup(WindowType.Popup, unit.ToString()); });
    }

    public void ShowPopup(WindowType type, string Content, Action onConfirm = null, Action onCancel = null)
    {
        switch (type)
        {
            case WindowType.Popup:
                StartCoroutine(Popup(Content, DestroyPopup));
                break;
            case WindowType.YesNoPopup:
                StartCoroutine(Popup(Content, DestroyPopup, onConfirm, onCancel));
                break;
        }
    }

    IEnumerator Popup(string content, Action callback)
    {
        CurrentPopup = Instantiate(m_popupWindow, transform);
        var textLabel = Instantiate(m_textField, CurrentPopup.transform).GetComponent<TMP_Text>();
        textLabel.text = content;
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                break;
            }
            yield return null;
        }
        callback();
    }

    IEnumerator Popup(string content, Action callback, Action onConfirm, Action onCancel)
    {
        CurrentPopup = Instantiate(m_popupWindow, transform);
        var textLabel = Instantiate(m_textField, CurrentPopup.transform).GetComponent<TMP_Text>();
        textLabel.text = content;
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                onCancel?.Invoke();
                break;
            }
            yield return null;
        }
        callback?.Invoke();
    }


    private void DestroyPanel()
    {
        Destroy(m_currentPanel);
        m_currentPanel = null;
    }

    private void DestroyPopup()
    {
        Destroy(m_currentPopup);
        m_currentPopup = null;
    }

    private void Awake()
    {
        Injector.RegisterContainer<UIManager>(this);
    }
}

