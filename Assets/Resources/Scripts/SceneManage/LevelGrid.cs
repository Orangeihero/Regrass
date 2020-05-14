using UnityEngine;

public enum NearGridDirection
{
    RIGHT = 1,
    LEFT = 2,
    FORWARD = 3,
    BACK = 4,
}

public enum WaterColor
{
    RED = 1,
    BLUE = 2
}

public enum GridType
{
    GROUND = 1,
    SEED = 2,
    WATER = 3,
    GLASS = 4
}


public class LevelGrid
{
    //这是描述每个地块信息的类，它不参与游戏事件循环，所以这里尽量只存储信息。
    //位置属性
    public Vector3 position; //位置
    public Vector3 direction; //朝向
    private LevelGrid[] nearGrids = new LevelGrid[4]; //邻接格子
    
    //地块属性
    private int luminance = 0;//亮度
    private int state = 0; // 0是没有被染色，1是被染色，2是染色且与未激活起点连接，3是染色且与激活起点连接
    public GridType type; //地面类型，包括地面、玻璃、起点、终点
    public WaterColor groundColor; //地面颜色，包括蓝色、红色
    
    //其他
    public StartPoint seed;//该地面上的起点

    public LevelGrid(Vector3 p, Vector3 d, GridType t, int l = 1) //构造函数,光照目前还没有用，不要管
    {
        this.position = p;
        this.direction = d;
        this.type = t;
        this.luminance = l;
        for(int i = 0; i < nearGrids.Length; i++)
        {
            nearGrids[i] = null;
        }
    }

    //设置对应方向的LevelGrid
    public void SetNearGrid(LevelGrid near, NearGridDirection dir)
    {
        nearGrids[(int)dir - 1] = near; 
    }

    //获取对应方向的LevelGrid
    public LevelGrid GetNearGrid(NearGridDirection dir)
    {
        return nearGrids[(int)dir - 1];
    }

    //给方块染色
    public void SprayWater(WaterColor waterColor)
    {
        //TODO:先判断这是否会让地面状态发生变化
        if (false/*水的状态没有变化*/)
        {
            //do nothing
        }
        else
        {
            //TODO:改变水的状态
            ScanGrids();
        }
    }

    public void ClearGrid()
    {
        //TODO:先判断这是否会让地面状态发生变化
        if (false/*水的状态没有变化*/)
        {
            //do nothing
        }
        else
        {
            //TODO:把水的状态清除回0
            ScanGrids();
        }
    }

    private void ScanGrids()
    {
        //TODO:遍历场景
    }
}
