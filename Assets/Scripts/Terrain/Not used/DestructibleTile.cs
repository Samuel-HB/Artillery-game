using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTile : MonoBehaviour
{
    [SerializeField] Tile topLeft;
    [SerializeField] Tile topRight;
    [SerializeField] Tile topMiddle;

    [SerializeField] Tile centerLeft;
    [SerializeField] Tile centerMiddle;
    [SerializeField] Tile centerRight;

    [SerializeField] Tile downLeft;
    [SerializeField] Tile downMiddle;
    [SerializeField] Tile downRight;

    private Tilemap tilemap;
    private Vector3 mousePos;
    private Vector3Int tilePos;

    [SerializeField] private Grid grid;

    private Vector3Int tilePosN;
    private Vector3Int tilePosW;
    private Vector3Int tilePosS;
    private Vector3Int tilePosE;
    private Tile tileN;
    private Tile tileW;
    private Tile tileS;
    private Tile tileE;


    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        tilePos = grid.LocalToCell(mousePos);
        if (Input.GetMouseButtonUp(0))
        {
            GetLocalTiles(tilePos);
            tilemap.SetTile(tilePos, null);
        }
    }
    
    private void GetLocalTiles(Vector3Int tilePos)
    {
        tilePosN = new Vector3Int(tilePos.x, tilePos.y + 1, tilePos.z);
        tilePosW = new Vector3Int(tilePos.x, tilePos.y - 1, tilePos.z);
        tilePosS = new Vector3Int(tilePos.x - 1, tilePos.y, tilePos.z);
        tilePosE = new Vector3Int(tilePos.x + 1, tilePos.y, tilePos.z);
        tileN = tilemap.GetTile<Tile>(tilePosN);
        tileW = tilemap.GetTile<Tile>(tilePosW);
        tileS = tilemap.GetTile<Tile>(tilePosS);
        tileE = tilemap.GetTile<Tile>(tilePosE);
    }
}
