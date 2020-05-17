using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 这是
public class FirstPerosonShooting : MonoBehaviour
{
    public float waterGunRange = 100f;
    private LevelGrid selectingGrid;
    private Vector3 targetPoint;
    private WaterColor waterColor = WaterColor.RED;
    private int mouseButtonDown = 0;

    public Text debugText;

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
                        if (Input.GetAxis("Mouse ScrollWheel") < 0) //鼠标后滚
                        {
                            //Done:Change Water color
                            //如果此时已经是最后一个颜色，那么就从第一个开始重新后滚（形成循环）
                            if (waterColor == WaterColor.BLUE) waterColor = WaterColor.RED;
                            else waterColor = waterColor + 1;
                            //Debug.Log("Hougun--");
                            //Debug.Log((int)waterColor);

                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") > 0) //鼠标前滚
                        {
                            //Done:Change Water color
                            if (waterColor == WaterColor.RED) waterColor = WaterColor.BLUE;
                            else waterColor = waterColor - 1;
                            //Debug.Log("Qiangun--");
                            //Debug.Log((int)waterColor);
                        }

                    }
                    break;
                }
            case 1:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        mouseButtonDown = 0;

                        //TODO:修改UI
                    }
                    else
                    {
                        if(selectingGrid != null)
                        {
                            selectingGrid.SprayWater(waterColor);
                        }
                        WaterAnimation();

                        //TODO:修改UI
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

        //debugText.text = $"{selectingGrid.position}" + 
        //    $"\nLEFT:{selectingGrid.GetNearGrid(NearGridDirection.LEFT).position} - {selectingGrid.hitPos[1]} - {selectingGrid.GetNearGrid(NearGridDirection.LEFT).groundColor}" +
        //    $"\nRIGHT:{selectingGrid.GetNearGrid(NearGridDirection.RIGHT).position} - {selectingGrid.hitPos[0]} - {selectingGrid.GetNearGrid(NearGridDirection.RIGHT).groundColor}" +
        //    $"\nFORWARD:{selectingGrid.GetNearGrid(NearGridDirection.FORWARD).position} - {selectingGrid.hitPos[2]} - {selectingGrid.GetNearGrid(NearGridDirection.FORWARD).groundColor}" +
        //    $"\nBACK:{selectingGrid.GetNearGrid(NearGridDirection.BACK).position} - {selectingGrid.hitPos[3]} - {selectingGrid.GetNearGrid(NearGridDirection.BACK).groundColor}";

    }

    //Debug用
    private void OnDrawGizmos()
    {
        if(selectingGrid != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(selectingGrid.position, .05f);
            //Gizmos.DrawIcon(selectingGrid.position, $"{selectingGrid.position}");

            //back
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.BACK).position, .05f);
            Gizmos.DrawLine(selectingGrid.raycastPos[3], selectingGrid.hitPos[3]);

            //forward
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.FORWARD).position, .05f);
            Gizmos.DrawLine(selectingGrid.raycastPos[2], selectingGrid.hitPos[2]);

            //left
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.LEFT).position, .05f);
            Gizmos.DrawLine(selectingGrid.raycastPos[1], selectingGrid.hitPos[1]);

            //right
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(selectingGrid.GetNearGrid(NearGridDirection.RIGHT).position, .05f);
            Gizmos.DrawLine(selectingGrid.raycastPos[0], selectingGrid.hitPos[0]);
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
