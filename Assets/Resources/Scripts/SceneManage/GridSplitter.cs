using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GridSplitter : MonoBehaviour
{
    protected LevelGrid[] levelGrids;
    protected void FindNearGrids()
    {
        foreach (LevelGrid grid in levelGrids)
        {
            FindNearGridOnDirection(grid, NearGridDirection.RIGHT);
            FindNearGridOnDirection(grid, NearGridDirection.LEFT);
            FindNearGridOnDirection(grid, NearGridDirection.FORWARD);
            FindNearGridOnDirection(grid, NearGridDirection.BACK);
        }
    }

    abstract protected void SplitGrid();

    abstract public LevelGrid GetGridAtPosition(Vector3 position);

    private void OnDrawGizmos()
    {
        foreach(LevelGrid grid in levelGrids)
        {
            if (grid.state != 0)
            {
                if (grid.groundColor == WaterColor.BLUE)
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
            }
            if (grid.state == 1)
            {
                Gizmos.color = Color.Lerp(Gizmos.color, Color.white, 0.8f);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);
                Gizmos.DrawCube(grid.position + grid.direction * 0.03f, rotation * new Vector3(0.3f, 0.05f, 0.3f));
            }
            else if (grid.state == 2)
            {
                Gizmos.color = Color.Lerp(Gizmos.color, Color.white, 0.8f);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);
                Gizmos.DrawCube(grid.position + grid.direction * 0.06f, rotation * new Vector3(1, 0.1f, 1));
            }
            else if (grid.state == 3)
            {

                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);
                Gizmos.DrawCube(grid.position + grid.direction * 0.22f, rotation * new Vector3(1, 0.4f, 1));
            }
        }
    }


    private void FindNearGridOnDirection(LevelGrid grid, NearGridDirection dir)
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        Vector3 raycastPos = grid.position + 0.5f * grid.direction;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);
        Vector3 direction = Vector3.back;
        switch (dir)
        {
            case NearGridDirection.BACK:
                {
                    direction = Vector3.back;
                    break;
                }
            case NearGridDirection.FORWARD:
                {
                    direction = Vector3.forward;
                    break;
                }
            case NearGridDirection.LEFT:
                {
                    direction = Vector3.left;
                    break;
                }
            case NearGridDirection.RIGHT:
                {
                    direction = Vector3.right;
                    break;
                }
        }

        if (Physics.Raycast(raycastPos, rotation * direction, out hit, 1f, layerMask))
        {
            GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
            grid.SetNearGrid(cube.GetGridAtPosition(hit.point), dir);
            //grid.setDebugInfo(raycastPos, hit.point, dir);
        }
        else if (Physics.Raycast(raycastPos, grid.position + rotation * direction - raycastPos, out hit, 2f, layerMask))
        {
            GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
            grid.SetNearGrid(cube.GetGridAtPosition(hit.point), dir);
            //grid.setDebugInfo(raycastPos, hit.point, dir);
        }
        else if (Physics.Raycast(raycastPos - grid.direction + rotation * direction, rotation * ((-1) * direction), out hit, 1f, layerMask))
        {
            GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
            grid.SetNearGrid(cube.GetGridAtPosition(hit.point), dir);
            //grid.setDebugInfo(raycastPos - grid.direction + rotation * direction, hit.point, dir);
        }
    }

    public void ResetScanGrid()
    {
        foreach(LevelGrid grid in levelGrids)
        {
            grid.haveScanned = false;
        }
    }

}
