using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToolPlant : MonoBehaviour
{
    public bool isActivate = false;
    private GameObject plantObject;

    // Update is called once per frame

    private void Awake()
    {
        plantObject = transform.GetChild(0).gameObject;
        plantObject.SetActive(isActivate);
    }

    void Update()
    {
        plantObject.SetActive(isActivate);
    }
}
