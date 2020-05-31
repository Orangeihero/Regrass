using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMushSeed : StartPoint
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO:玩家跳跃功能
    }

    override public void Activate()
    {
        ChangeModel(true);
    }

    override public void Deactivate()
    {
        ChangeModel(false);
    }
}
