using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GridSplitter : MonoBehaviour
{
    protected LevelGrid[] levelGrids;
    protected void FindNearGrids()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        foreach (LevelGrid grid in levelGrids)
        {
            Vector3 raycastPos = grid.position + 0.5f * grid.direction;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, grid.direction);

            //right
            if (Physics.Raycast(raycastPos, rotation * Vector3.right, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.right - raycastPos, out hit, 2f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.right, rotation * Vector3.left, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.RIGHT);
            }

            //left
            if (Physics.Raycast(raycastPos, rotation * Vector3.left, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.left - raycastPos, out hit, 2f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.left, rotation * Vector3.right, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.LEFT);
            }

            //forward
            if (Physics.Raycast(raycastPos, rotation * Vector3.forward, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.forward - raycastPos, out hit, 2f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.forward, rotation * Vector3.back, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.FORWARD);
            }

            //back
            if (Physics.Raycast(raycastPos, rotation * Vector3.back, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
            else if (Physics.Raycast(raycastPos, grid.position + rotation * Vector3.back - raycastPos, out hit, 2f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
            else if (Physics.Raycast(raycastPos - grid.direction + rotation * Vector3.back, rotation * Vector3.forward, out hit, 1f, layerMask))
            {
                GridSplitter cube = hit.collider.GetComponent<GridSplitter>();
                grid.SetNearGrid(cube.GetGridAtPosition(hit.point), NearGridDirection.BACK);
            }
        }
    }

    abstract protected void SplitGrid();

    abstract public LevelGrid GetGridAtPosition(Vector3 position);
}
