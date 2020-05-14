using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public bool isActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f, layerMask))
        {
            LevelGrid grid = hit.collider.GetComponent<GridSplitter>().GetGridAtPosition(hit.point);
            grid.type = GridType.SEED;
            grid.seed = this;

        }
    }

    public void Activate()
    {
        //TODO:激活
    }

    public void Deactivate()
    {
        //TODO:不激活
    }
}
