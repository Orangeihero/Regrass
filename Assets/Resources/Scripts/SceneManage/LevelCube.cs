using UnityEngine;
using System.Collections;



public class LevelCube : GridSplitter
{
    /*
        这个类附在每个场景物体中，它会在场景加载时分割场景数据，并完成初始化的工作。
        它也负责返回raycast击中位置的grid
    */

    private Vector3 startPoint;
    private Vector3 cubeSize;

    private void Awake()
    {
        SplitGrid();
        gameObject.layer = 9;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNearGrids();
        GameManager.AddScanCube(this);
        LightDetect();
    }

    public void UpdateGridVisual()
    {
        //visual
        foreach(LevelGrid grid in levelGrids)
        {
            if(grid.type == GridType.GROUND)
            {
                if(grid.state == 1)
                {
                    //TODO:无起点连接visual
                }else if(grid.state >= 2)
                {
                    //获得所有type的模型
                    
                    //随机分布这些模型
                }
                
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    //Gizmos.color = Color.white;
    //    //for (int i = 0; i < levelGrids.Length; i++)
    //    //{
    //    //    Gizmos.DrawLine(levelGrids[i].position, levelGrids[i].position + levelGrids[i].direction);
    //    //}
    //}

    //输入世界坐标，返回对应位置的LevelGrid对象
    public override LevelGrid GetGridAtPosition(Vector3 position)
    {
        Vector3 offset = position - startPoint;
        if (Mathf.Abs(offset.x - 0) < 0.01f || Mathf.Abs(offset.x - cubeSize.x) < 0.01f)
        {
            offset.y = Mathf.Floor(offset.y);
            offset.z = Mathf.Floor(offset.z);
            if (Mathf.Abs(offset.x - cubeSize.x) < 0.01f)
            {
                float indexoffset = cubeSize.x * cubeSize.y * 2 + cubeSize.x * cubeSize.z * 2;
                return levelGrids[(int)indexoffset + (int)offset.y * (int)cubeSize.z * 2 + (int)offset.z * 2];
            }
            else
            {
                float indexoffset = cubeSize.x * cubeSize.y * 2 + cubeSize.x * cubeSize.z * 2;
                return levelGrids[(int)indexoffset + (int)offset.y * (int)cubeSize.z * 2 + (int)offset.z * 2 + 1];
            }
        }
        else if (Mathf.Abs(offset.y - 0) < 0.01f || Mathf.Abs(offset.y - cubeSize.y) < 0.01f) 
        {
            offset.x = Mathf.Floor(offset.x);
            offset.z = Mathf.Floor(offset.z);
            if (Mathf.Abs(offset.y - cubeSize.y) < 0.01f)
            {
                float indexoffset = cubeSize.x * cubeSize.y * 2;
                return levelGrids[(int)indexoffset + (int)offset.x * (int)cubeSize.z * 2 + (int)offset.z * 2];
            }
            else
            {
                float indexoffset = cubeSize.x * cubeSize.y * 2;
                return levelGrids[(int)indexoffset + (int)offset.x * (int)cubeSize.z * 2 + (int)offset.z * 2 + 1];
            }
        }
        else
        {
            offset.y = Mathf.Floor(offset.y);
            offset.x = Mathf.Floor(offset.x);
            if (Mathf.Abs(offset.z - cubeSize.z) < 0.01f)
            {
                float indexoffset = 0;
                return levelGrids[(int)indexoffset + (int)offset.x * (int)cubeSize.y * 2 + (int)offset.y * 2];
            }
            else
            {
                float indexoffset = 0;
                return levelGrids[(int)indexoffset + (int)offset.x * (int)cubeSize.y * 2 + (int)offset.y * 2 + 1];
            }
        }
    }

    protected override void SplitGrid()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        cubeSize = vertices[1] - vertices[0] + vertices[2] - vertices[0] + vertices[1] - vertices[5];

        startPoint = transform.TransformPoint(vertices[9]);
        int gridCounts = (int)cubeSize.x * (int)cubeSize.y * 2 + (int)cubeSize.x * (int)cubeSize.z * 2 + (int)cubeSize.y * (int)cubeSize.z * 2;
        levelGrids = new LevelGrid[gridCounts];
        int gridIndex = 0;
        GridType type;

        if (GetComponent<MeshRenderer>().material.name == "GroundMat (Instance)") 
        {
            type = GridType.GROUND;
        }
        else
        {
            type = GridType.GLASS;
        }

        for (int x = 0; x < cubeSize.x; x++)
        {
            for (int y = 0; y < cubeSize.y; y++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(x, y, cubeSize.z) + new Vector3(0.5f, 0.5f, 0);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.forward, type);
                position.z = startPoint.z;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.back, type);
            }
        }

        for (int x = 0; x < cubeSize.x; x++)
        {
            for (int z = 0; z < cubeSize.z; z++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(x, cubeSize.y, z) + new Vector3(0.5f, 0, 0.5f);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.up, type);
                position.y = startPoint.y;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.down, type);
            }
        }

        for (int y = 0; y < cubeSize.y; y++)
        {
            for (int z = 0; z < cubeSize.z; z++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(cubeSize.x, y, z) + new Vector3(0, 0.5f, 0.5f);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.right, type);
                position.x = startPoint.x;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.left, type);
            }
        }
    }
}
