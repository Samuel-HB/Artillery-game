using System;
using System.Diagnostics;
using UnityEngine;

public class MS_Terrain : MonoBehaviour
{
    // inspector
    [field: SerializeField]
    public Vector2Int GridSize { get; private set; }
    public float gridResolution;
    public float maxBrushSize;
    public float brushChangeSizeSpeed;

    // private
    Vector3 mouseWorldPosition;
    float currentRadius;
    float Smallradius => currentRadius - gridResolution;

    MSVertex[,] grid;
    MSVertex[,] Grid
    {
        get
        {
            if (grid == null || grid.GetLength(0) != GridSize.x || grid.GetLength(1) != GridSize.y)
                grid = CreateNewGrid(GridSize, gridResolution);

            return grid;
        }
    }


    MSVertex[,] CreateNewGrid(Vector2Int gridSize, float gridResolution)
    {
        gridSize.x = Mathf.Max(gridSize.x, 2);
        gridSize.y = Mathf.Max(gridSize.y, 2);

        gridResolution = Mathf.Max(gridResolution, 0.01f);

        grid = new MSVertex[gridSize.x, gridSize.y];

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                grid[i, j] = new MSVertex(i, j, gridResolution);
            }
        }

        return grid;
    }

    void Start()
    {
        currentRadius = Mathf.Lerp(gridResolution, maxBrushSize, .5f);
    }

    void Update()
    {
        Plane terrainPlane = new Plane(Vector3.back, Vector3.zero);
        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!terrainPlane.Raycast(clickRay, out float distance)) {
            return; // si pas de raycast alors return
        }
        mouseWorldPosition = clickRay.GetPoint(distance);

        float deltaRadius = ZeroSign(Input.mouseScrollDelta.y * brushChangeSizeSpeed);
        currentRadius = Mathf.Clamp(currentRadius + deltaRadius, gridResolution, maxBrushSize);

        if (Input.GetMouseButton(0)) {
            ChangeTerrain(true);
        }
        if (Input.GetMouseButton(1)) {
            ChangeTerrain(false);
        }
    }

    float ZeroSign(float f) => f == 0 ? 0 : (f > 0 ? 1 : -1);

    private void ChangeTerrain(bool isAdding)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                float distance = Vector3.Distance(mouseWorldPosition, Grid[i, j].position);

                if (distance > currentRadius) {
                    continue;
                }
                if (distance < Smallradius)
                {
                    Grid[i, j].fill = isAdding ? 1 : 0;
                    continue;
                }

                // value closer to big radius => value tends to 0
                // value closer to small radius => value tends to 1
                float normalizedEdgeDistance = 1 - Mathf.InverseLerp(Smallradius, currentRadius, distance);

                if (isAdding) {
                    Grid[i, j].fill = Mathf.Max(normalizedEdgeDistance, Grid[i, j].fill);
                }
                else {
                    Grid[i, j].fill = Mathf.Min(normalizedEdgeDistance, Grid[i, j].fill);
                }
            }
        }
    }

    void OnValidate()
    {
        grid = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(mouseWorldPosition, currentRadius);

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Gizmos.color = Grid[i, j].Active ? Color.green : Color.red;
                Gizmos.DrawSphere(Grid[i, j].position, .05f);
            }
        }

        Gizmos.color = Color.yellow;

        for (int i = 0; i < Grid.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < Grid.GetLength(1) - 1; j++)
            {
                MSVertex bl = Grid[i, j];
                MSVertex tl = Grid[i, j + 1];
                MSVertex tr = Grid[i + 1, j + 1];
                MSVertex br = Grid[i + 1, j];

                DisplayCell(bl, tl, tr, br);
            }
        }
    }

    void DisplayCell(MSVertex bl, MSVertex tl, MSVertex tr, MSVertex br)
    {
        byte activationMask = 0;

        activationMask |= (byte)((bl.Active ? 1 : 0) << 0);
        activationMask |= (byte)((tl.Active ? 1 : 0) << 1);
        activationMask |= (byte)((tr.Active ? 1 : 0) << 2);
        activationMask |= (byte)((br.Active ? 1 : 0) << 3);

        Vector3 t = WeightedLerp(tl, tr);
        Vector3 r = WeightedLerp(tr, br);
        Vector3 b = WeightedLerp(br, bl);
        Vector3 l = WeightedLerp(bl, tl);
        switch (activationMask)
        {
            case 0b_0000:
                break;
            case 0b_0001:
                Gizmos.DrawLine(l, b);
                break;
            case 0b_0010:
                Gizmos.DrawLine(l, t);
                break;
            case 0b_0011:
                Gizmos.DrawLine(b, t);
                break;
            case 0b_0100:
                Gizmos.DrawLine(t, r);
                break;
            case 0b_0101:
                Gizmos.DrawLine(l, b);
                Gizmos.DrawLine(t, r);
                break;
            case 0b_0110:
                Gizmos.DrawLine(l, r);
                break;
            case 0b_0111:
                Gizmos.DrawLine(b, r);
                break;
            case 0b_1000:
                Gizmos.DrawLine(b, r);
                break;
            case 0b_1001:
                Gizmos.DrawLine(l, r);
                break;
            case 0b_1010:
                Gizmos.DrawLine(b, r);
                Gizmos.DrawLine(l, t);
                break;
            case 0b_1011:
                Gizmos.DrawLine(t, r);
                break;
            case 0b_1100:
                Gizmos.DrawLine(b, t);
                break;
            case 0b_1101:
                Gizmos.DrawLine(l, t);
                break;
            case 0b_1110:
                Gizmos.DrawLine(l, b);
                break;
            case 0b_1111:
                break;
        }

        Vector3 Mid(MSVertex a, MSVertex b) => (a.position + b.position) / 2;
    }

    Vector3 WeightedLerp(MSVertex a, MSVertex b)
    {
        MSVertex min = a.fill > b.fill ? b : a;
        MSVertex max = a.fill > b.fill ? a : b;

        float sumFill = a.fill + b.fill;

        if (sumFill == 0)
        {
            return (a.position + b.position) / 2;
        }
        float lerp = min.fill / sumFill;
        return Vector3.Lerp(min.position, max.position, lerp);
    }
}

class MSVertex
{
    public readonly Vector2 position;
    public bool Active => fill == 1;    
    public float fill;

    public MSVertex(int i, int j, float resolution)
    {
        position = new Vector2(i, j) * resolution;
    }
}