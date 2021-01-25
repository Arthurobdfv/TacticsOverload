using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapGenerator
{
    MapTerrain[] HighlighTilesAround(Vector3 pos, int distance);
    void DeHighlight();
    MapTerrain[] FindPath(MapTerrain start, MapTerrain end);
    MapTerrain GetTileOf(GameObject obj);
}


