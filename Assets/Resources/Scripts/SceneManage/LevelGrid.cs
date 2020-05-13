using UnityEngine;

public enum NearGridDirection
{
    RIGHT = 1,
    LEFT = 2,
    FORWARD = 3,
    BACK = 4,
}


public class LevelGrid
{
    //这是描述每个地块信息的类，它不参与游戏事件循环，所以这里尽量只存储信息。

    public Vector3 position;
    public Vector3 direction;
    private LevelGrid[] nearGrids = new LevelGrid[4];
    private float[] state;

    public LevelGrid(Vector3 p, Vector3 d)
    {
        this.position = p;
        this.direction = d;
        for(int i = 0; i < nearGrids.Length; i++)
        {
            nearGrids[i] = null;
        }
    }

    public void SetNearGrid(LevelGrid near, NearGridDirection dir)
    {
        nearGrids[(int)dir - 1] = near; 
    }

    //获取对应方向的LevelGrid
    public LevelGrid GetNearGrid(NearGridDirection dir)
    {
        return nearGrids[(int)dir - 1];
    }
}
