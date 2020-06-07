using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGrassSeed : StartPoint
{
    public List<Door> doors = new List<Door>();
    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        InitializeDoor();
    }

    override public void Activate()
    {
        if(ChangeModel(true))   SeasameDoor();
    }

    override public void Deactivate()
    {
        if(ChangeModel(false))  SeasameDoor();
    }

    private void SeasameDoor()
    {
        if (isActivate)
        {
            foreach (Door door in doors)
            {
                door.openLock();
            }
        }
        else
        {
            foreach (Door door in doors)
            {
                door.addLock();
            }
        }
    }

    private void InitializeDoor()
    {
        //TODO:加门（或许直接手动）
        foreach (Door door in doors)
        {
            door.addLock();
        }
    }
}
