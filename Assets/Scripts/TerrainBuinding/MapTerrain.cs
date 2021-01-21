using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTerrain : InteractibleGameObject
{
    Vector3 m_setPoint;
    GameObject m_object;

    public Material m_highlightedMaterial;
    private Material m_originalMaterial;
    private MeshRenderer m_renderer;

    public Vector3 SetPoint => (transform.position + ((Vector3.up) / 2));
    public bool IsOccupied => Object != null;
    public GameObject Object
    {
        get { return m_object; }
        private set
        {
            m_object = value;
        }
    }

    public void SetObject(GameObject objectToSet)
    {
        if (IsOccupied)
        {
            Debug.Log("Tile Already Occupied");
            return;
        }
        Object = objectToSet;
        Debug.Log($"Terrain Place = {transform.position.ToString()}\nSpawnPoint = {SetPoint.ToString()}");
        objectToSet.transform.position = SetPoint;
    }

    public void Highlight()
    {
        m_renderer.material = m_highlightedMaterial;
    }

    public void DeHighlight()
    {
        m_renderer.material = m_originalMaterial;
    }

    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>() ?? throw new MissingComponentOnStartException(nameof(m_renderer));
        m_originalMaterial = m_renderer.material;
    }
}
