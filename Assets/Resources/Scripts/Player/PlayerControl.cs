using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool playerHasGun;
    FirstPerosonShooting shootingComponent;
    GameObject gun;

    private void Awake()
    {
        playerHasGun = false;

    }
    private void Start()
    {
        gun = GameObject.Find("Gun").gameObject;
        shootingComponent = gun.GetComponent<FirstPerosonShooting>();
        SetGun();
    }

    public void AddGun()
    {
        playerHasGun = true;
        SetGun();
    }

    public void AddColor(WaterColor color)
    {
        shootingComponent.AddColor(color);
    }

    private void SetGun()
    {
        shootingComponent.SetGun(playerHasGun);
    }
}
