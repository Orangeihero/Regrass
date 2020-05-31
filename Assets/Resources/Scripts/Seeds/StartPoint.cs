using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum SeedType
//{
//    LIGHTGRASS = 1,
//    LIGHTMUSH = 2,
//    BOUNCEMUSH = 3,
//    BLOCKTREE = 4
//}

abstract public class StartPoint : MonoBehaviour
{
    public bool isActivate = false;
    public GameObject deactivate;
    public WaterColor color;
    public Vector3 centerPointOffset;

    public GameObject activatedGrass;
    public GameObject deactivatedGrass;

    private Vector3 hitPoint;

    protected void InitializeGrid()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.TransformVector(centerPointOffset) + transform.TransformDirection(Vector3.up) * 0.5f, transform.TransformDirection(Vector3.down), out hit, 1.2f, layerMask))
        {
            hitPoint = hit.point;
            LevelGrid grid = hit.collider.GetComponent<GridSplitter>().GetGridAtPosition(hit.point);
            grid.type = GridType.SEED;
            grid.groundColor = color;
            grid.state = 2;
        }
    }

    abstract public void Activate();

    abstract public void Deactivate();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position + transform.TransformVector(centerPointOffset) + transform.TransformDirection(Vector3.up) * 0.5f, hitPoint);
        
    }

    public GameObject GetGrowingGrass(int state)
    {
        if(state == 2)
        {
            return activatedGrass;
        }else if(state == 3)
        {
            return deactivatedGrass;
        }
        else
        {
            return null;
        }
    }

    protected void ChangeModel(bool b)
    {
        if(isActivate != b)
        {
            isActivate = b;
            activatedGrass.SetActive(isActivate);
            deactivatedGrass.SetActive(!isActivate);
        }
    }
}
