using System;
using System.Collections;
using UnityEngine;

public class Unit : InteractibleGameObject
{
    private float m_stepTime = .5f;

    private CharacterStats m_stats;

    public float StepTime
    {
        get { return m_stepTime; }
        set { m_stepTime = value; }
    }
    public CharacterStats Stats
    {
        get { return m_stats; }
        set { m_stats = value; }
    }

    IEnumerator MoveCoroutine(MapTerrain[] path, Action callback = null)
    {
        for(int i=0; i<path.Length; i++)
        {
            var tileToChase = path[i];
            var t = 0f;
            var pos = transform.position;
            while(t < StepTime)
            {
                transform.position = Vector3.Lerp(pos, tileToChase.SetPoint, t/StepTime);
                t += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
        callback?.Invoke();
    }

    public bool Move(MapTerrain[] path, Action callback = null)
    {
        var t = StartCoroutine(MoveCoroutine(path));
        return true;
    }
}
    