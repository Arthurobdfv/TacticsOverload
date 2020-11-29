using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public int MapSize
    {
        get { return m_mapSize; }
        set
        {
            if(value != m_mapSize)
            {
                m_mapSize = value;
            }
        }
    }

    public MapTerrain[,] map;

    public MapTerrain m_terrain;

    [Range(1f,10f)]
    public int HeightAdjustment;

    private int m_mapSize;
    void Awake()
    {
        MapSize = 5;
        GenerateTerrain();
    }

    public void SpawnPlayers(List<GameObject> players)
    {
        if (players.Count == 0) Debug.LogWarning($"PlayerCount = 0");
        var offset = Mathf.FloorToInt(map.GetLength(0) / players.Count);
        int i = 0;
        foreach(var player in players)
        {
            var x = offset * i;
            var z = Random.Range(0, MapSize);
            var terrainToSpawn = map[x, z];
            terrainToSpawn.SetObject(player);
            i++;
        }
    }

    void GenerateTerrain()
    {
        var heigths = TerrainGenerator.GenerateNoiseMap(2*MapSize, MapSize, 10);
        map = new MapTerrain[2 * MapSize, MapSize];
        for(int i=0; i<MapSize*(2*MapSize); i++)
        {
            var x = (int)Mathf.Floor(i / MapSize);
            var z = (int)Mathf.Floor(i % MapSize);
            map[x,z] = Instantiate(m_terrain, new Vector3(x, Mathf.Floor(heigths[x, z] * 10)/2, z),Quaternion.identity);
        }
    }

    public int GetDistanceBetween(MapTerrain m1, MapTerrain m2) => Mathf.CeilToInt(Mathf.Abs(m1.transform.position.z - m2.transform.position.z) + Mathf.Abs(m1.transform.position.x - m2.transform.position.x));
}
