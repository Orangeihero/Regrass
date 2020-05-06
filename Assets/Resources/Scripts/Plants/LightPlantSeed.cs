using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantColor
{
    red = 1,
    blue = 2
}

[RequireComponent(typeof(MeshRenderer))]
public class LightPlantSeed : MonoBehaviour
{
    public PlantColor plantColor = PlantColor.red;

    void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        switch (plantColor)
        {
            case PlantColor.red:
                {
                    meshRenderer.material = Resources.Load<Material>("Material/Plants/RedPlantMat");
                    break;
                }
            case PlantColor.blue:
                {
                    meshRenderer.material = Resources.Load<Material>("Material/Plants/BluePlantMat");
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
