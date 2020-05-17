using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : GridSplitter
{
    public WaterColor color;

    public override LevelGrid GetGridAtPosition(Vector3 position)
    {
        return levelGrids[0];
    }

    protected override void SplitGrid()
    {
        levelGrids = new LevelGrid[1];
        Vector3 gridPosition = transform.position + new Vector3(0.5f, 1, -0.5f);
        levelGrids[0] = new LevelGrid(gridPosition, Vector3.up, GridType.WATER);
        levelGrids[0].groundColor = color;
        levelGrids[0].state = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNearGrids();
        GameManager.AddScanCube(this);
    }

    private void Awake()
    {
        SplitGrid();
        gameObject.layer = 9;
    }
}
