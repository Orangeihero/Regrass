using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    //Grass Property
    public Texture2D NoiseTexture = default;
    public bool Wiggle = true;
    public bool Wind = true;
    [Range(0f, 1f)]
    public float WindStrenght = .5f;
    [Range(0f, 1f)]
    public float WindSpeed = .5f;
    [Range(0f, 1f)]
    public float WindTurbulence = .5f;
    [Range(0f, 1f)]
    public float GrassWiggle = .5f;

    public GameObject grassPrefab;

    void Update()
    {
        SetWeather();
    }

    private void Start()
    {

    }

    private void SetWeather()
    {
        if (Wiggle)
        {
            Shader.EnableKeyword("_ISWIGGLE_ON");
        }
        else
        {
            Shader.DisableKeyword("_ISWIGGLE_ON");
        }

        if (Wind)
        {
            Shader.EnableKeyword("_ISWINDY_ON");
        }
        else
        {
            Shader.DisableKeyword("_ISWINDY_ON");
        }

        Shader.SetGlobalTexture("NoiseTexture", NoiseTexture);
        Shader.SetGlobalVector("WindDirection", transform.rotation * Vector3.back);
        Shader.SetGlobalFloat("WindStrenght", WindStrenght);
        Shader.SetGlobalFloat("WindSpeed", WindSpeed);
        Shader.SetGlobalFloat("WindTurbulence", WindTurbulence);
        Shader.SetGlobalFloat("GrassWiggle", GrassWiggle);
    }

    public void SpawnGrass(Vector3 spawnPoint)
    {

    }

    public void EraseGrass(Vector3 position)
    {

    }
}
