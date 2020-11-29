using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        DetectInputs();
    }

    private void DetectInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (RaycastOnMousePosition(out hit))
            {
                var hitObject = hit.collider?.GetComponent<InteractibleGameObject>();
                if (hitObject != null)
                {
                    hitObject.OnClick();
                }
            }
        }
    }

    bool RaycastOnMousePosition(out RaycastHit hit)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}
