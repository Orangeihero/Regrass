using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGrassSeed : StartPoint
{
    public override void Activate()
    {
        if (!isActivate)
        {
            isActivate = true;
            //TODO:更换模型
            //TODO:告知activate？
        }
    }

    public override void Deactivate()
    {
        isActivate = false;
        //TODO:更换模型
        //TODO：告知？
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        //type = SeedType.LIGHTGRASS;
    }

}
