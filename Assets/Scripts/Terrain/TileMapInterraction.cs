using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TileMapInterraction : MonoBehaviour
{
    public static Tilemap tilemap;
    public static Grid grid;
    public static List<Vector3Int> tilesPositions;

    public static bool isStartedTileExplosion = false;


    void Start()
    {
        tilesPositions = new List<Vector3Int>();
        grid = GetComponentInParent<Grid>();
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (isStartedTileExplosion == true)
        {
            RemoveTile(tilesPositions);
            isStartedTileExplosion = false;
        }
    }

    public static void RemoveTile(List<Vector3Int> tilesPositions)
    {
        for (int i = 0;  i < tilesPositions.Count; i++)
        {
            Vector3Int position = grid.WorldToCell(tilesPositions[i]);
            tilemap.SetTile(position, null);
        }
        tilesPositions.Clear();
    }
}
