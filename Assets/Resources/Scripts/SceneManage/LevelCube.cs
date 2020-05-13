using UnityEngine;
using System.Collections;

public class LevelCube : MonoBehaviour
{
    /*
        这个类附在每个场景物体中，它会在场景加载时分割场景数据，并完成初始化的工作。
        它也负责返回raycast击中位置的grid
    */
    private Vector3 startPoint;
    private Vector3 cubeSize;
    private LevelGrid[] levelGrids;

    private void Awake()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        cubeSize = vertices[1] - vertices[0] + vertices[2] - vertices[0] + vertices[1] - vertices[5];
        //Debug.Log($"{name}:{cubeSize}");
        //Debug.Log($"{vertices[5]},{vertices[9]}");

        startPoint = transform.TransformPoint(vertices[9]);
        int gridCounts = (int)cubeSize.x * (int)cubeSize.y * 2 + (int)cubeSize.x * (int)cubeSize.z * 2 + (int)cubeSize.y * (int)cubeSize.z * 2;
        levelGrids = new LevelGrid[gridCounts];
        int gridIndex = 0;

        for (int x = 0; x < cubeSize.x; x++) 
        {
            for (int y = 0; y < cubeSize.y; y++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(x, y, cubeSize.z) + new Vector3(0.5f, 0.5f, 0);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.forward);
                position.z = startPoint.z;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.back);
            }
        }

        for (int x = 0; x < cubeSize.x; x++)
        {
            for (int z = 0; z < cubeSize.z; z++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(x, cubeSize.y, z) + new Vector3(0.5f, 0, 0.5f);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.up);
                position.y = startPoint.y;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.down);
            }
        }

        for (int y = 0; y < cubeSize.y; y++)
        {
            for (int z = 0; z < cubeSize.z; z++, gridIndex += 2)
            {
                Vector3 position = startPoint + new Vector3(cubeSize.x, y, z) + new Vector3(0, 0.5f, 0.5f);
                levelGrids[gridIndex] = new LevelGrid(position, Vector3.right);
                position.x = startPoint.x;
                levelGrids[gridIndex + 1] = new LevelGrid(position, Vector3.left);
            }
        }

        gameObject.layer = 9;
        //Test();

    }

    void Test()
    {
        for(int i = 0; i < levelGrids.Length; i++)
        {
            //GameObject grid = Instantiate(testPrefab, levelGrids[i].position, levelGrids[i].rotation, transform);
            //grid.layer = 9;
        }
        
    }

    private void Start()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        foreach (LevelGrid grid in levelGrids)
        {
            Vector3 raycastPos = grid.position + 0.5f * grid.direction;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);

            //right
            if (Physics.Raycast(raycastPos, rotation * Vector3.right, out hit, 1, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            } else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.right - raycastPos, out hit, 1.3f, layerMask))   {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            }
            else if(Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.right, rotation * Vector3.left, out hit, 1f, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            }

            //left
            if (Physics.Raycast(raycastPos, rotation * Vector3.left, out hit, 1, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.left - raycastPos, out hit, 1.3f, layerMask)) 
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.left, rotation * Vector3.right, out hit, 1f, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }

            //forward
            if (Physics.Raycast(raycastPos, rotation * Vector3.forward, out hit, 1, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.forward - raycastPos, out hit, 1.3f, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.forward, rotation * Vector3.back, out hit, 1f, layerMask)) 
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }

            //back
            if (Physics.Raycast(raycastPos, rotation * Vector3.back, out hit, 1, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.back - raycastPos, out hit, 1.3f, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.back, rotation * Vector3.forward, out hit, 1f, layerMask))
            {
                LevelCube cube = hit.collider.GetComponent<LevelCube>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //for (int i = 0; i < levelGrids.Length; i++)
        //{
        //    Gizmos.DrawLine(levelGrids[i].position, levelGrids[i].position + levelGrids[i].direction);
        //}
    }



    public void HighlightGrid(Vector3 position)
    {
        LevelGrid grid = GetGridAtPosition(position);
        //TODO:显示高亮
    }
    

    //输入世界坐标，返回对应位置的LevelGrid对象
    public LevelGrid GetGridAtPosition(Vector3 position)
    {
        Vector3 offset = position - startPoint;
        if (offset.x == 0 || offset.x == cubeSize.x)
        {
            offset.y = Mathf.Floor(offset.y);
            offset.z = Mathf.Floor(offset.z);
            if (offset.x == cubeSize.x)
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
        else if (offset.y == 0 || offset.y == cubeSize.y)
        {
            offset.x = Mathf.Floor(offset.x);
            offset.z = Mathf.Floor(offset.z);
            if (offset.y == cubeSize.y)
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
            if (offset.z == cubeSize.z)
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
}
