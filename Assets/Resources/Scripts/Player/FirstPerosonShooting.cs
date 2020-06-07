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
    public GameObject hl; //用来显示高亮的cube

    public Text debugText;

    private LevelCube selectingCube;

    //public ParticleSystem waterParticle;
    //public ParticleSystem[] particles = new ParticleSystem[2];

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

                        //waterParticle = particles[(int)waterColor - 1];

                    }
                    break;
                }
            case 1:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        mouseButtonDown = 0;
                        //waterParticle.Stop();
                        //TODO:修改UI
                    }
                    else
                    {
                        if(selectingGrid != null)
                        {
                            selectingGrid.SprayWater(waterColor);
                            selectingCube.SprayWaterAtPosition(targetPoint, waterColor);
                        }
                        //WaterAnimation();

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
        int layerMask = 1 << 9 | 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, waterGunRange, layerMask))
        {
            targetPoint = hit.point;
            selectingCube = hit.collider.GetComponent<LevelCube>();
            if(selectingCube != null)
            {
                selectingGrid = selectingCube.GetGridAtPosition(hit.point);
            }
            else
            {
                selectingGrid = null;
            }
            //selectingGrid = hit.collider.GetComponent<GridSplitter>().GetGridAtPosition(hit.point);
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

        if (selectingGrid != null)
        {
            debugText.text = $"Position:{selectingGrid.position}   State:{selectingGrid.state}   Luminance:{selectingGrid.luminance}   Type:{selectingGrid.type}";
            for(int i = 0;i < selectingGrid.grassStates.Length; i++)
            {
                debugText.text += $"\ngrassState[{i}] = {selectingGrid.grassStates[i]}";
            }
            //LevelGrid grid = selectingGrid.GetNearGrid(NearGridDirection.LEFT);
            //if (grid != null)
            //{
            //    debugText.text += $"\nLEFT:{grid.position} - {grid.groundColor}";
            //}
            //else
            //{
            //    debugText.text += "\nLEFT:NULL";
            //}
            //grid = selectingGrid.GetNearGrid(NearGridDirection.RIGHT);
            //if (grid != null)
            //{
            //    debugText.text += $"\nRIGHT:{grid.position} - {grid.groundColor}";
            //}
            //else
            //{
            //    debugText.text += "\nRIGHT:NULL";
            //}
            //grid = selectingGrid.GetNearGrid(NearGridDirection.FORWARD);
            //if (grid != null)
            //{
            //    debugText.text += $"\nFORWARD:{grid.position} - {grid.groundColor}";
            //}
            //else
            //{
            //    debugText.text += "\nFORWARD:NULL";
            //}
            //grid = selectingGrid.GetNearGrid(NearGridDirection.BACK);
            //if (grid != null)
            //{
            //    debugText.text += $"\nBACK:{grid.position} - {grid.groundColor}";
            //}
            //else
            //{
            //    debugText.text += "\nBACK:NULL";
            //}

        }
        else
        {
            debugText.text = "NULL";
        }


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
        //waterParticle.Play();
    }

    private void HighlightGrid()
    {
        if (selectingGrid != null)
        {
            hl.SetActive(true);
            if (selectingGrid.direction == Vector3.up)
            {
                hl.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (selectingGrid.direction == Vector3.down)
            {
                hl.transform.eulerAngles = new Vector3(180, 0, 0);
            }
            else if (selectingGrid.direction == Vector3.forward)
            {
                hl.transform.eulerAngles = new Vector3(90, 0, 0);
            }
            else if (selectingGrid.direction == Vector3.back)
            {
                hl.transform.eulerAngles = new Vector3(-90, 0, 0);
            }
            else if (selectingGrid.direction == Vector3.left)
            {
                hl.transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else if (selectingGrid.direction == Vector3.right)
            {
                hl.transform.eulerAngles = new Vector3(90, 0, 90);
            }
            hl.transform.position = selectingGrid.position;
            if (selectingGrid.type == GridType.SOIL || selectingGrid.type == GridType.GROUND)
            {
                if (waterColor == WaterColor.BLUE)
                    hl.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/SceneMats/Water/BlueWaterMat");
                else
                    hl.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/SceneMats/Water/RedWaterMat");
            }
            else if (selectingGrid.type == GridType.GLASS)
            {
                hl.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/SceneMats/Water/GrayMat");
            }
        }
        else hl.SetActive(false);

    }
}
