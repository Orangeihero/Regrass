using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 这是
public class FirstPerosonShooting : MonoBehaviour
{
    public float waterGunRange = 100f;
    private LevelGrid selectingGrid;
    private Vector3 targetPoint;
    private WaterColor waterColor;
    private int mouseButtonDown = 0;



    // Update is called once per frame
    void Update()
    {
        //输入判断
        switch (mouseButtonDown)
        {
            case 0:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseButtonDown = 1;
                    }

                    if(Input.GetMouseButtonDown(1))
                    {
                        if(mouseButtonDown == 0)
                        {
                            mouseButtonDown = 2;
                        }
                        else
                        {
                            mouseButtonDown = 0;
                        }
                    }

                    if(mouseButtonDown == 0)
                    {
                        if(Input.GetAxis("Mouse ScrollWheel") < 0)
                        {
                            //TODO:Change Water color
                        }else if(Input.GetAxis("Mouse ScrollWheel") > 0)
                        {
                            //TODO:Change Water color
                        }
                    }
                    break;
                }
            case 1:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        mouseButtonDown = 0;
                    }
                    else
                    {
                        if(selectingGrid != null)
                        {
                            selectingGrid.SprayWater(waterColor);
                        }
                        WaterAnimation();
                    }
                    break;
                }
            case 2:
                {
                    if (Input.GetMouseButtonUp(1))
                    {
                        mouseButtonDown = 0;
                    }
                    else
                    {
                        if(selectingGrid != null)
                        {
                            selectingGrid.ClearGrid();
                        }
                    }
                    break;
                }
        }

        //Raycast
        int layerMask = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, waterGunRange, layerMask))
        {
            targetPoint = hit.point;
            selectingGrid = hit.collider.GetComponent<GridSplitter>().GetGridAtPosition(hit.point);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        { 
            targetPoint = transform.position + transform.TransformDirection(Vector3.forward) * waterGunRange; //这里可能有问题
            selectingGrid = null;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        //高亮
        HighlightGrid();

    }

    //Debug用
    private void OnDrawGizmos()
    {
        if(selectingGrid != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(selectingGrid.position, .05f);
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.BACK).position, .05f);
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.FORWARD).position, .05f);
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.LEFT).position, .05f);
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.RIGHT).position, .05f);
        }
    }

    private void WaterAnimation()
    {
        //TODO:用targetPoint和枪的位置计算一个水的动画轨迹
    }

    private void HighlightGrid()
    {
        //TODO:高亮SelectingGrid
    }
}
