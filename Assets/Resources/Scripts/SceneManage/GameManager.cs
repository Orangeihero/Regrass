using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StartScan();
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static StartScan startScan;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static void AddScanCube(GridSplitter splitter)
    {
        startScan += splitter.ResetScanGrid;
    }

    public static void ResetScanGrid()
    {
        startScan();
    }

    public static void NextScene()
    {
        
    }

    public static void RestartScene()
    {
        startScan();
    }
}
