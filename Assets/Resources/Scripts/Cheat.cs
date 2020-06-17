using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public List<Door> doors;
    public TextController textController;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            textController.showInfo("Press Left Mouse Button to Shoot Water" , 2);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            textController.showInfo("Press Right Mouse Button to Clear the Ground", 2);
        }
    }
}
