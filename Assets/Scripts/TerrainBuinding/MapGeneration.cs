using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGeneration : MonoBehaviour, IMapGenerator
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

    public MapTerrain[] m_selection;

    [Range(1f,10f)]
    public int HeightAdjustment;

    private int m_mapSize;

    void Awake()
    {
        Injector.RegisterContainer<IMapGenerator,MapGeneration>(this);
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
            var z = UnityEngine.Random.Range(0, MapSize);
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

    public void HighLightTile(MapTerrain _terr)
    {
        _terr.Highlight();
    }

    public void HighlightTiles(IEnumerable<MapTerrain> tiles)
    {
        foreach(var t in tiles)
        {
            HighLightTile(t);
        }
    }

    public MapTerrain[] HighlighTilesAround(Vector3 pos, int distance)
    {
        var index = IndexOf(GetClosestTile(pos), map);
        var tiles = new List<MapTerrain>();
        for (int i = 0; i < MapSize * (2 * MapSize); i++)
        {
            var x = (int)Mathf.Floor(i / MapSize);
            var z = (int)Mathf.Floor(i % MapSize);
            var mag1 = x + z;
            var mag2 = (index.x + index.y);
            if (Mathf.Abs(x - Mathf.FloorToInt(index.x)) + Mathf.Abs(z - Mathf.FloorToInt(index.y)) <= distance) tiles.Add(map[x, z]);
        }
        m_selection = tiles.ToArray();
        HighlightTiles(tiles.ToArray());
        return m_selection;
    }

    public MapTerrain GetClosestTile(Vector3 pos)
    {
        return MapToList().Aggregate((m1, m2) => Vector3.SqrMagnitude(m1.transform.position - pos) <= Vector3.SqrMagnitude(m2.transform.position - pos) ? m1 : m2);
    }

    public Vector2 IndexOf(MapTerrain target, MapTerrain[,] array)
    {
        for (int i = 0; i < MapSize * (2 * MapSize); i++)
        {
            var x = (int)Mathf.Floor(i / MapSize);
            var z = (int)Mathf.Floor(i % MapSize);
            if (map[x, z] == target) return new Vector2(x, z);
        }
        return Vector2.negativeInfinity;
    }

    private IEnumerable<MapTerrain> MapToList()
    {
        var aux = new List<MapTerrain>();
        for (int i = 0; i < MapSize * (2 * MapSize); i++)
        {
            var x = (int)Mathf.Floor(i / MapSize);
            var z = (int)Mathf.Floor(i % MapSize);
            aux.Add(map[x, z]);
        }
        return aux;
    }
}
