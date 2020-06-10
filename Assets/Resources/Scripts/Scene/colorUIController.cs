using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorUIController : MonoBehaviour
{
    public FirstPerosonShooting firstPerosonShooting;
    private WaterColor waterColor;
    public GameObject blueUI = new GameObject();
    public GameObject redUI = new GameObject();
    // Start is called before the first frame update
    void Start()
    {
        blueUI.SetActive(false);
        redUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        waterColor = firstPerosonShooting.getWaterColor();
        if(waterColor == WaterColor.BLUE)
        {
            redUI.SetActive(false);
            blueUI.SetActive(true);
        }
        else if (waterColor == WaterColor.RED)
        {
            blueUI.SetActive(false);
            redUI.SetActive(true);
        }
        else
        {
            blueUI.SetActive(false);
            redUI.SetActive(false);
        }
    }
}
