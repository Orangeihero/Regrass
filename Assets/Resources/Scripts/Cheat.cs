using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public List<Door> doors;
    private void Update()
    {
        for(int i = 0;i < doors.Count; i++)
        {
            if(Input.GetKeyDown((KeyCode)i + 49))
            {
                doors[i].Open();
            }
        }
    }
}
