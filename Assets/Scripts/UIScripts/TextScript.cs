using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class TextScript : MonoBehaviour
{
    private TMPro.TMP_Text _text;
    private void Awake()
    {
        _text = GetComponent<TMPro.TMP_Text>() ?? throw new MissingComponentOnStartException(nameof(_text));
        GameController.RoundChanged += UpdateText;
    }

    private void OnDestroy()
    {
        GameController.RoundChanged -= UpdateText;
    }

    void UpdateText(Player a)
    {
        _text.text = a.Name;
    }
}
