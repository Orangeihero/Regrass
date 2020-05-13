using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 这是
public class FirstPerosonShooting : MonoBehaviour
{
    private LevelGrid debugGrid;

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            debugGrid = hit.collider.gameObject.GetComponent<LevelCube>().GetGridAtPosition(hit.point);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

    private void OnDrawGizmos()
    {
        if(debugGrid != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(debugGrid.position, .05f);
            Gizmos.DrawSphere(debugGrid.GetNearGrid(NearGridDirection.BACK).position, .05f);
            Gizmos.DrawSphere(debugGrid.GetNearGrid(NearGridDirection.FORWARD).position, .05f);
            Gizmos.DrawSphere(debugGrid.GetNearGrid(NearGridDirection.LEFT).position, .05f);
            Gizmos.DrawSphere(debugGrid.GetNearGrid(NearGridDirection.RIGHT).position, .05f);
        }
    }
}
