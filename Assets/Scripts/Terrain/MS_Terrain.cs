using UnityEngine;

[ExecuteAlways]
public class MS_Terrain : MonoBehaviour
{
    [field:SerializeField]
    public Vector2Int GridSize { get; private set; }
    public float gridResolution;

    MSVertex[,] grid;
    MSVertex[,] Grid
    {
        get
        {
            if (grid == null || grid.GetLength(0) != GridSize.x || grid.GetLength(1) != GridSize.y) {
                grid = CreateNewGrid(GridSize, gridResolution);
            }
            return grid;
        }
    }


    MSVertex[,] CreateNewGrid(Vector2Int gridSize,float gridResolution )
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

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Plane terrainPlane = new Plane(Vector3.back, Vector3.zero);
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (terrainPlane.Raycast(clickRay, out float distance))
            {
                Vector3 hitPoint = clickRay.GetPoint(distance);
                TryAndTgogleVertexState(hitPoint);
            }
        }
    }

    private void TryAndTgogleVertexState(Vector3 mousePosition)
    {
        MSVertex closest = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(0); j++)
            {
                float distance = Vector3.Distance(mousePosition, Grid[i, j].position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = Grid[i, j];
                }
            }
        }
        closest.active = !closest.active;
    }

    void OnValidate()
    {
        grid = null;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(j); j++)
            {
                Gizmos.color = Grid[i, j].active ? Color.green : Color.red;
                Gizmos.DrawSphere(Grid[i, j].position, 0.05f);
            }
        }

        Gizmos.color = Color.yellow;

        for (int i = 0; i < Grid.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < Grid.GetLength(j) - 1; j++)
            {
                MSVertex bl = Grid [i, j];
                MSVertex tl = Grid [i, j + 1];
                MSVertex tr = Grid [i + 1, j + 1];
                MSVertex br = Grid [i + 1, j];

                DisplayCell(bl, tl, tr, br);
            }
        }
    }

    void DisplayCell(MSVertex bl, MSVertex tl, MSVertex tr, MSVertex br)
    {
        byte activationMask = 0;

        activationMask |= (byte)((bl.active ? 1 : 0) << 0);
        activationMask |= (byte)((tl.active ? 1 : 0) << 1);
        activationMask |= (byte)((tr.active ? 1 : 0) << 2);
        activationMask |= (byte)((br.active ? 1 : 0) << 3);

        switch (activationMask)
        {
            case 0b_0000:
                break;
            case 0b_0001:
                Gizmos.DrawLine(Mid(bl, tl), Mid(bl, br));
                break;
            case 0b_0010:
                Gizmos.DrawLine(Mid(bl, tl), Mid(tl, tr));
                break;
            case 0b_0011:
                Gizmos.DrawLine(Mid(bl, br), Mid(tl, tr));
                break;
            case 0b_0100:
                Gizmos.DrawLine(Mid(tl, tr), Mid(tr, br));
                break;
            case 0b_0101:
                Gizmos.DrawLine(Mid(bl, tl), Mid(bl, br));
                Gizmos.DrawLine(Mid(tl, tr), Mid(tr, br));
                break;
            case 0b_0110:
                Gizmos.DrawLine(Mid(bl, tl), Mid(tr, br));
                break;
            case 0b_0111:
                Gizmos.DrawLine(Mid(bl, br), Mid(tr, br));
                break;
            case 0b_1000:
                Gizmos.DrawLine(Mid(bl, br), Mid(tr, br));
                break;
            case 0b_1001:
                Gizmos.DrawLine(Mid(bl, tl), Mid(tr, br));
                break;
            case 0b_1010:
                Gizmos.DrawLine(Mid(bl, br), Mid(tr, br));
                Gizmos.DrawLine(Mid(bl, tl), Mid(tl, tr)); 
                break;
            case 0b_1011:
                Gizmos.DrawLine(Mid(tl, tr), Mid(tr, br));
                break;
            case 0b_1100:
                Gizmos.DrawLine(Mid(bl, br), Mid(tl, tr));
                break;
            case 0b_1101:
                Gizmos.DrawLine(Mid(bl, tl), Mid(tl, tr));
                break;
            case 0b_1110:
                Gizmos.DrawLine(Mid(bl, tl), Mid(bl, br));
                break;
            case 0b_1111:
                break;
        }

        Vector3 Mid(MSVertex a, MSVertex b) => (a.position + b.position) / 2;
    }
}

class MSVertex
{
    public readonly Vector2 position;
    public bool active;

    public MSVertex(int i, int j, float resolution)
    {
        position = new Vector2(i, j) * resolution;
    }
}



//public int a;
//public int A
//{
//    get
//    {
//        return a;
//    }
//    set
//    {
//        a = value;
//        if (a < 0) { 
//            a = 0;
//        }
//    }
//}