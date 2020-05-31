using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
    public ParticleSystem pars1;
    public ParticleSystem pars2;
    private int flag = 1;

    // Start is called before the first frame update
    void Start()
    {
        //pars.Play(); //或者 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //pars.gameObject.SetActive(true);
            if (flag == 1) pars1.Play();
            else if (flag == 2) pars2.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //pars.gameObject.SetActive(false);
            if (flag == 1) pars1.Stop();
            else if (flag == 2) pars2.Stop();
        }
        if (Input.GetMouseButton(1) && (!Input.GetMouseButton(0)))
        {
            if (flag == 1) flag = 2;
            else flag = 1;
            //pars.startColor = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0.1f, 1f));
        }
        Debug.Log(flag);
    }

}
