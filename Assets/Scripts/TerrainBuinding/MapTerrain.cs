using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTerrain : MonoBehaviour
{
    Vector3 m_setPoint;
    GameObject m_object;

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

}
